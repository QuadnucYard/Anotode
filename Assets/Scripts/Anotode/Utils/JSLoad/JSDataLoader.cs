using System;
using System.IO;
using Puerts;
using UnityEngine;

namespace Anotode.Utils.JSLoad {
	internal class JSDataLoader : IDisposable {

		private readonly string developerTools = "DeveloperTools.js";    // SourceMap 处理文件
		private readonly int debugPort = 43990;                          // 调试端口号
		private readonly bool debug = false;                             // 是否开启调试

		private readonly JSLoader jsLoader;                             // JS加载器
		private readonly string jsDir;                                  // JS脚本目录
		public static JsEnv vm { get; private set; }

		public JSDataLoader() {
			//Application.runInBackground = true;
			jsDir ??= Path.Combine(Application.streamingAssetsPath, "data");
			jsLoader ??= new JSLoader(jsDir); ;
			if (vm == null) {
#if UNITY_WEBGL && !UNITY_EDITOR
				vm = Puerts.WebGL.GetBrowserEnv();
#else
				vm = new JsEnv(jsLoader, debugPort, IntPtr.Zero, IntPtr.Zero);
				if (false && developerTools != string.Empty) {                                             // 启用 SourceMap 映射
					vm.ExecuteModule(developerTools);
				};
#endif
				vm.UsingAction<bool>();                                                             // 处理事件要用到（有多少类型添加多少个）
				vm.UsingAction<float>();                                                            // 处理事件要用到（有多少类型添加多少个）
				vm.UsingAction<float, float>();                                                      // 处理事件要用到（有多少类型添加多少个）
			};
			if (debug) {                                                                              // 启用调试
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
