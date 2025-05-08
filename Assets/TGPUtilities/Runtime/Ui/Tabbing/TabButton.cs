using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;



namespace TGP.Utilities.Ui {
	[RequireComponent(typeof(Image))]
	public class TabButton : BaseMonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler {

		internal TabGroup TabGroup;
		internal Image Backgroud;
		[SerializeField]
		internal UnityEvent OnClick;
		protected override void Awake() {
			base.Awake();
			Backgroud = GetComponent<Image>();
			TabGroup = GetComponentInParent<TabGroup>();
		}

		private void OnEnable() {
			TabGroup.Subscribe(this);
		}
		private void OnDisable() {
			TabGroup.UnSubscribe(this);
		}
		public void OnPointerClick(PointerEventData eventData) {
			TabGroup.OnTabSelected(this);
			OnClick?.Invoke();
		}
		public void SetActive() {
			if (debug)
				Debug.Log($"SetActive {this.gameObject.name}");
			TabGroup.OnTabSelected(this);
		}

		public void OnPointerEnter(PointerEventData eventData) {
			TabGroup.OnTabEnter(this);
		}

		public void OnPointerExit(PointerEventData eventData) {
			TabGroup.OnTabExit(this);
		}



	}
}
