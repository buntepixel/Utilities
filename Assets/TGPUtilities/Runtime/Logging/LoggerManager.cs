using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;

namespace TGP.Utilities {
	public class LoggerManager : LoggerManagerBase {
		[SerializeField]
		TMP_Text ErrorTextfield;
		CanvasGroup cg;

		protected override void Awake() {
			base.Awake();
			if (ErrorTextfield != null)
				cg = ErrorTextfield.GetComponentInParent<CanvasGroup>();
		}

		public void ClearErrorTextfield() {
			ErrorTextfield.text = string.Empty;
		}

		void LogToWindow(string condition, string stacktrace) {
			if (ErrorTextfield == null)
				return;
			if (logStacktrace)
				_errorSequ.Add(string.Concat(condition, "\n", stacktrace, "\n--------------"));
			else {
				_errorSequ.Add(string.Concat(condition, "\n--------------"));
			}
		}

		protected override void Update() {
			base.Update();
			if (_errorSequ.Count > 0) {
				foreach (var item in _errorSequ) {
					_errorText = string.Concat(item, "\n", _errorText);
				}
				_errorSequ.Clear();
				ErrorTextfield.text = _errorText;
				if (cg != null) {
					cg.EnableInputVisibility(true);
				}
			}
		}

		protected override void LogCallbacksToFile(string condition, string stacktrace, LogType logtype) {
			base.LogCallbacksToFile(condition, stacktrace, logtype);
			LogToWindow(condition, stacktrace);
		}
	}
}
