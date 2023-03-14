using System;
using Puerts;

namespace Anotode.Utils.JSLoad {
	internal class JSDataLoader : IDisposable {

		private readonly string developerTools = "DeveloperTools.js";    // SourceMap 处理文件
		private readonly int debugPort = 43990;                          // 调试端口号
		private readonly bool debug = false;                             // 是否开启调试

		public static JsEnv vm { get; private set; }

		public JSDataLoader(string jsDir) {
			//Application.runInBackground = true;
			var jsLoader = new JSLoader(jsDir); ;
			if (vm == null) {
#if UNITY_WEBGL && !UNITY_EDITOR
				vm = Puerts.WebGL.GetBrowserEnv();
#else
				vm = new JsEnv(jsLoader, debugPort, IntPtr.Zero, IntPtr.Zero);
				if (developerTools != string.Empty) {      // 启用 SourceMap 映射
					vm.ExecuteModule(developerTools);
				};
#endif
				// 处理事件要用到（有多少类型添加多少个）
				vm.UsingAction<bool>();
				vm.UsingAction<float>();
				vm.UsingAction<float, float>();
				vm.UsingFunc<bool>();
				vm.UsingFunc<float>();
				vm.UsingFunc<float?>();

			};
			if (debug) {                                           // 启用调试
				vm.WaitDebugger();
			};

			JSObjectConverter.Init(vm);
		}

		public T Load<T>(string filename, string exportee = "default") {
			return JSObjectConverter.Convert<T>(vm.ExecuteModule<JSObject>(CheckedName(filename), exportee));
		}

		public T LoadTo<T>(T obj, string filename, string exportee = "default") {
			return JSObjectConverter.ConvertInplace(obj, vm.ExecuteModule<JSObject>(CheckedName(filename), exportee));
		}

		private string CheckedName(string filename) {
			if (!filename.EndsWith(".js")) {
				filename += ".js";
			}
			return filename;
		}

		public void Dispose() {
			vm.Dispose();
		}
	}
}
