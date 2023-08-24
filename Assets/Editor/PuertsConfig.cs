/// <summary>
/// 官方说明 https://github.com/Tencent/puerts/blob/master/doc/unity/manual.md
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using Puerts;

/// <summary>
/// 配置类
/// ! 配置类必须打 [Configure] 标签
/// ! 必须放在Editor目录
/// </summary>
[Configure]
public class PuertsConfig {

	/// <summary>
	/// 在 Js/Ts 调用时可以找到该类
	/// * 会生成一个静态类（wrap），在 Js 调用时将直接静态调用加快速度，否则通过反射调用
	/// * 会生成到 Assets/Gen/Typing/csharp/index.d.ts ，以在 Ts 中引用
	/// ! 须放在 [Configure] 标记过的类里
	/// </summary>
	/// <value></value>
	[Binding]
	internal static IEnumerable<Type> Bindings {
		get {
			var types = new List<Type> {
				//typeof(UnityEngine.Debug),
				//typeof(UnityEngine.Mathf),
				typeof(UnityEngine.Vector2),
				typeof(UnityEngine.Vector2Int),
				typeof(UnityEngine.Vector3),
				typeof(UnityEngine.Quaternion),
				typeof(Anotode.Utils.JSLoad.JSObjectEntry),
			};

			var namespaces = new HashSet<string> {
				"Anotode",
			};
			var namespacePrefixes = new List<string> {
				"Anotode.Models",
				"Anotode.Simul",
				"Quadnuc",
				"System.Collections.Generic",
			};

			var ignoredNamespaces = new HashSet<string> {
				"Quadnuc.Editor",
			};
			var ignored = new Dictionary<string, HashSet<string>> {
				{"Quadnuc.Utils", new () { "Pathy", "Properties" } },
			};

			// TODO：在此处添加要忽略绑定的类型
			Dictionary<string, HashSet<string>> registered = new();
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				var name = assembly.GetName().Name;
				foreach (var type in assembly.GetTypes()) {
					if (!(type.IsPublic || type.IsNestedPublic)) continue;
					if (type.Name.Contains("<") || type.Name.Contains("*")) continue;               // 忽略泛型，指针类型
					if (type.Namespace == null || type.Name == null) continue;                      // 这是啥玩意？

					bool accept = namespaces.Contains(type.Namespace) ||
						namespacePrefixes.Any(s => type.Namespace.StartsWith(s));

					if (!accept) continue;

					if (ignoredNamespaces.Contains(type.Namespace) ||
						ignored.ContainsKey(type.Namespace) && ignored[type.Namespace].Contains(type.Name))
						continue;

					// 嵌套类？

					types.Add(type);
					if (!registered.ContainsKey(type.Namespace)) {
						registered.Add(type.Namespace, new() { type.Name });
					} else {
						registered[type.Namespace].Add(type.Name);
					}
				}
			}

			return types;
		}
	}
	/// <summary>
	/// 对定义的 Blittable 值类型通过内存拷贝传递，可避免值类型传递产生的GC，需要开启unsafe编译选项
	/// ! 只能用在属性上
	/// ! 需要开启 unsafe 编译选项 
	/// ! 须放在 [Configure] 标记过的类里
	/// </summary>
	/// <value></value>
	[BlittableCopy]
	internal static IEnumerable<Type> Blittables {
		get {
			return new List<Type>() {
				typeof(UnityEngine.Vector2),
				typeof(UnityEngine.Vector3),
				typeof(UnityEngine.Vector4),
				typeof(UnityEngine.Quaternion),
				typeof(UnityEngine.Color),
				typeof(UnityEngine.Rect),
				typeof(UnityEngine.Bounds),
				typeof(UnityEngine.Ray),
				typeof(UnityEngine.RaycastHit),
				typeof(UnityEngine.Matrix4x4)
			};
		}
	}

	/// <summary>
	/// 过滤函数
	/// ! 只能用在函数上
	/// ! 须放在 [Configure] 标记过的类里
	/// </summary>
	/// <param name="memberInfo"></param>
	/// <returns></returns>
	[Filter]
	internal static bool FilterMethods(System.Reflection.MemberInfo memberInfo) {
		string sig = memberInfo.ToString();
		string fullName = memberInfo.DeclaringType.FullName;

		if (fullName == "UnityEngine.MonoBehaviour" && memberInfo.Name == "runInEditMode") return true;
		if (fullName == "UnityEngine.Input" && memberInfo.Name == "IsJoystickPreconfigured") return true;
		if (fullName == "UnityEngine.Texture" && memberInfo.Name == "imageContentsHash") return true;
		if (fullName == "UnityEngine.MeshRenderer" && memberInfo.Name == "stitchLightmapSeams") return true;
		if (fullName == "UnityEngine.MeshRenderer" && memberInfo.Name == "receiveGI") return true;
		if (fullName == "UnityEngine.MeshRenderer" && memberInfo.Name == "scaleInLightmap") return true;
		if (fullName == "UnityEngine.ParticleSystemRenderer" && memberInfo.Name == "supportsMeshInstancing") return true;
		if (fullName == "UnityEngine.UI.Text" && memberInfo.Name == "OnRebuildRequested") return true;
		if (fullName == "UnityEngine.Light" && memberInfo.Name == "SetLightDirty") return true;
		if (fullName == "UnityEngine.Light" && memberInfo.Name == "shadowRadius") return true;
		if (fullName == "UnityEngine.Light" && memberInfo.Name == "shadowAngle") return true;
		if (fullName == "UnityEngine.Light" && memberInfo.Name == "areaSize") return true;
		if (fullName == "UnityEngine.Light" && memberInfo.Name == "lightmapBakeType") return true;
		if (fullName == "UnityEngine.CanvasRenderer" && memberInfo.Name == "OnRequestRebuild") return true;
		if (fullName == "UnityEngine.CanvasRenderer" && memberInfo.Name == "onRequestRebuild") return true;
		// if (memberInfo.Name == "OnRequestRebuild") return true;
		// TODO: 添加要忽略导出的类成员

		return sig.Contains("*");
	}
}