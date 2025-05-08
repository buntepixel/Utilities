using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TGP.Utilities {
	public abstract class SelectionExtra :BaseMonoBehaviour, IDeselectHandler {
		protected Selectable selectable;
		protected Image image;
		protected override void Awake() {
			base.Awake();
			selectable = GetComponentInParent<Selectable>();
			image = GetComponent<Image>();
			image.enabled = false;
		}

		public virtual void SetSelected() {
			BaseEventData data = new BaseEventData(EventSystem.current);
			data.selectedObject = selectable.gameObject;
			EventSystem.current.SetSelectedGameObject(gameObject,data);
			image.enabled = true;
		}
		public virtual void OnDeselect(BaseEventData eventData) {
			//Debug.Log($"onDeselct called: {eventData.selectedObject.name}");
			image.enabled = false;
		}
	
	}
}
