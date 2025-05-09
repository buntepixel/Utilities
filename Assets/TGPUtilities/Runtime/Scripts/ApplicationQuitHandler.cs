using UnityEngine;

namespace TGP.Utilities {
	[DefaultExecutionOrder(100)]
	/// fixes bug of unity 6 not closing background processes
	/// https://discussions.unity.com/t/bug-unity-6-build-game-process-continues-running-in-background-after-closing-window/1573387/8
	public class ApplicationQuitHandler : MonoBehaviour {
		
		private void OnDestroy() {
			if (!Application.isEditor) {
				System.Diagnostics.Process.GetCurrentProcess().Kill();
			}
		}
	}
}
