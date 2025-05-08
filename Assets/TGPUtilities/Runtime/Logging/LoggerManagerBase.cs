using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using UnityEngine;

namespace TGP.Utilities {
	public class LoggerManagerBase : BaseMonoBehaviour {
		[SerializeField]
		public LogLevel _LogLevel;
		[SerializeField]
		int LogFilesToKeep = 10;
		[SerializeField]
		string Filepath;
		[SerializeField]
		protected bool logStacktrace;
		protected string _errorText;
		protected List<string> _errorSequ;
		[Flags]
		public enum LogLevel {
			none = 0,
			Errors = 1 << 0,
			Warnings = 1 << 1,
			Information = 1 << 2,
			Exeption = 1 << 3
		}
		protected Logger logger;
		protected override void Awake() {
			base.Awake();
			logger = new Logger(LogFilesToKeep);
			_errorSequ = new List<string>();
			logger.CreateAppLogFile();
			if (debug)
				logger.debug = debug;
			Filepath = logger.CurrLogFilePath;
		}

		protected virtual void OnEnable() {
			Application.logMessageReceivedThreaded += LogCallbacksToFile;
		}
		protected virtual void OnDisable() {
			Application.logMessageReceivedThreaded -= LogCallbacksToFile;
		}
		protected virtual void OnDestroy() {
			logger.WriteLogEntry("-----------------App Closed--------------\n\n", LogType.Warning);
		}
		protected virtual void LogCallbacksToFile(string condition, string stacktrace, LogType logtype) {
			switch (logtype) {
				case LogType.Error:
					if (_LogLevel.HasFlag(LogLevel.Errors)) {
						LogErrors(condition, stacktrace, logtype);
					}
					break;
				case LogType.Assert:
					break;
				case LogType.Warning:
					if (_LogLevel.HasFlag(LogLevel.Warnings)) {
						LogWarnings(condition, stacktrace, logtype);
					}
					break;
				case LogType.Log:
					if (_LogLevel.HasFlag(LogLevel.Information)) {
						LogInformation(condition, stacktrace, logtype);
					}
					break;
				case LogType.Exception:
					if (_LogLevel.HasFlag(LogLevel.Exeption)) {
						LogException(condition, stacktrace, logtype);
					}
					break;
				default:
					break;
			}
		}
		protected virtual void Update() {
			if (_errorSequ.Count > 0) {
				foreach (var item in _errorSequ) {
					_errorText = string.Concat(item, "\n", _errorText);
				}
				_errorSequ.Clear();
			}
		}
		protected virtual void LogErrors(string message, string stacktrace, LogType logtype) {
			logger.WriteLogEntry(message, logtype, stacktrace);
		}
		protected virtual void LogWarnings(string message, string stacktrace, LogType logtype) {
			logger.WriteLogEntry(message, logtype, logStacktrace ? stacktrace : string.Empty);
		}
		protected virtual void LogInformation(string message, string stacktrace, LogType logtype) {
			logger.WriteLogEntry(message, logtype, logStacktrace ? stacktrace : string.Empty);
		}
		protected virtual void LogException(string message, string stacktrace, LogType logtype) {
			logger.WriteLogEntry(message, logtype, stacktrace);
		}
		protected void SetBit(int bitNr) {
			byte tmp = (byte)_LogLevel;
			tmp = (byte)(tmp | (1 << bitNr));
			_LogLevel = (LogLevel)tmp;
		}

		protected void UnSetBit(int bitNr) {
			byte tmp = (byte)_LogLevel;
			tmp = (byte)(tmp & ~(1 << bitNr));
			_LogLevel = (LogLevel)tmp;
		}
	}

}
