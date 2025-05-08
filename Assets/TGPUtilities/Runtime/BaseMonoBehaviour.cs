using UnityEngine;
namespace TGP.Utilities {
	public class BaseMonoBehaviour : MonoBehaviour {
		protected string TAG;
		protected string GON { get { return this.gameObject.name; } }
		protected string CN {
			get {
				string[] s = this.GetType().ToString().Split(".");
				return s[s.Length - 1];
			}
		}
		[SerializeField]
		protected bool debug;
		public void SetDebug(bool value) {
			debug = value;
		}

		protected virtual void Awake() {
			TAG = string.Concat("[", Application.productName, "] ");
		}
	}
}