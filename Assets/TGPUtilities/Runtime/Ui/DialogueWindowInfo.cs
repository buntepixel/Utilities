using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TGP.Utilities.Ui {
	public class DialogueWindowInfo : DialogueWindowBase {
		public delegate void DialogueWindow_delegate(System.Object sender, DialogueWindowInfoEventArgs context);
		public static DialogueWindow_delegate OnOpenDialogueWindow;
		public static DialogueWindow_delegate OnCloseDialogueWindow;
		public static Action<UnityAction> OnAddButtonListener;
		[SerializeField]
		TMP_Text headline;
		[SerializeField]
		TMP_Text message;
		[SerializeField]
		Button confirm;
		TMP_Text confirmText;
		UnityAction ButtonCallback;

		protected override void Awake() {
			base.Awake();
			if (headline == null || message == null)
				Debug.LogError($" Serialized Fields are empty on Obj: {this.gameObject.name}");
			if (confirm == null)
				Debug.LogError($" no Button Assigned on Obj: {this.gameObject.name}");
			else
				confirmText = confirm.GetComponentInChildren<TMP_Text>();
			confirm.onClick.AddListener(() => CloseWindow(this, new DialogueWindowBaseEventArgs()));
		}
		private void OnEnable() {
			OnOpenDialogueWindow = OpenWindow;
			OnCloseDialogueWindow = CloseWindow;
			OnAddButtonListener = AddButtonListener;
		}
		 void AddButtonListener(UnityAction callback) {
			ButtonCallback = callback;
			confirm.onClick.AddListener(callback);
		}
		protected override void CloseWindow(object sender, DialogueWindowBaseEventArgs args) {
			base.CloseWindow(sender, args);
			if (ButtonCallback != null) {
				confirm.onClick.RemoveListener(ButtonCallback);
				ButtonCallback = null;
			}
		}
		protected override async void OpenWindow(object sender, DialogueWindowBaseEventArgs args) {
			if (args == null)
				return;
			DialogueWindowInfoEventArgs myArgs = new DialogueWindowInfoEventArgs();
			try {
				myArgs = args as DialogueWindowInfoEventArgs;
			} catch (Exception ex) {
				Debug.LogError(ex.Message);
			}
			if (isOpen && debug) {
				Debug.LogWarning($"Window already open, change text from:\n{message.text}\nto\n{myArgs.Message}");
			}
			base.OpenWindow(sender, myArgs);
			confirmText.text = myArgs.ConfirmBtnText;
			confirm.gameObject.SetActive(myArgs.Confirm && myArgs.ConfirmBtnText != string.Empty);
			headline.text = myArgs.Headline;
			message.text = myArgs.Message;
			if (!myArgs.Confirm) {
				if (CancellationTokenSource != null)//cancelling the old timer and setting a new one.
					CancellationTokenSource.Cancel();
				Debug.Log($"{TAG}Setting new token");
				CancellationTokenSource = new System.Threading.CancellationTokenSource();
				await AutoHideAsync(myArgs, CancellationTokenSource.Token);
				CancellationTokenSource.Dispose();
				CancellationTokenSource = null;
			}
		}
	}
	public class DialogueWindowInfoEventArgs : DialogueWindowBaseEventArgs {
		public DialogueWindowInfoEventArgs() {

		}
		public DialogueWindowInfoEventArgs(string headline, string message, bool confirm = false, int displayDuration = 5, string confirmBtnMessage = "Ok") : base(displayDuration) {
			Confirm = confirm;
			ConfirmBtnText = confirmBtnMessage;
			if (!Confirm)
				DisplayDuration = displayDuration;
			Headline = headline;
			Message = message;
		}
		public string ConfirmBtnText { get; set; }
		public bool Confirm { get; set; }
		public string Headline { get; set; }
		public string Message { get; set; }

	}
}