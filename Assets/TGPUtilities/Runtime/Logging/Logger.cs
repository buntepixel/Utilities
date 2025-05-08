using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace TGP.Utilities {
	public class Logger {
		int counter;
		public bool debug;

		public string CurrLogFilePath { get; private set; }
		ReaderWriterLockSlim lock_ = new ReaderWriterLockSlim();

		/// <summary>
		/// creates Logger with default paths at Application.productName
		/// </summary>
		/// <param name="filesToKeep"></param>
		public Logger(int filesToKeep) : this(filesToKeep, string.Concat(DateTime.Now.ToString("yyMMdd"), "_Logfile", "_", Application.productName)) { }

		/// <summary>
		/// creates Logger with default paths at Application.persistentDataPath
		/// </summary>
		/// <param name="filesToKeep"></param>
		/// <param name="filename"></param>
		/// <param name="includeDate"></param>
		/// <param name="fileEnding"></param>
		public Logger(int filesToKeep, string filename, bool includeDate = false, string fileEnding = ".txt") : this(filesToKeep, filename, Application.persistentDataPath, includeDate, fileEnding) { }

		/// <summary>
		/// creates Logger with custom path
		/// </summary>
		/// <param name="filesToKeep"></param>
		/// <param name="filename"></param>
		/// <param name="Filepath"></param>
		/// <param name="includeDate"></param>
		/// <param name="fileEnding"></param>
		public Logger(int filesToKeep, string filename, string Filepath, bool includeDate = false, string fileEnding = ".txt") {
			CurrLogFilePath = string.Concat(Filepath, "/_", filename, includeDate == true ? DateTime.Now.ToString("yyMMdd") : "", fileEnding);
			CheckIfDirExists(CurrLogFilePath);
			DeleteOldFiles(filesToKeep);
		}

		void CheckIfDirExists(string dir) {
			if (debug)
				UnityEngine.Debug.LogFormat($"dirIn: {dir}");
			string dirOnly = Path.GetDirectoryName(dir);
			if (!Directory.Exists(dirOnly))
				Directory.CreateDirectory(dirOnly);
		}

		public void CreateAppLogFile(bool overwrite = false) {
			counter = 0;
			if (overwrite && File.Exists(CurrLogFilePath))
				File.Delete(CurrLogFilePath);
			StreamWriter currLogFile;
			if (!File.Exists(CurrLogFilePath)) {
				using (currLogFile = new StreamWriter(CurrLogFilePath, false)) {
					currLogFile.WriteLine(String.Concat("Logfile created:", DateTime.Now.ToString("G", DateTimeFormatInfo.InvariantInfo)));
					currLogFile.Close();
				}
			}
		}

		/// <summary>
		/// Deletes old files
		/// </summary>
		/// <param name="keep">files to keep, -1 is infinite</param>
		void DeleteOldFiles(int keep) {
			if (keep < 0) return;
			foreach (var fi in new DirectoryInfo(Application.persistentDataPath).GetFiles().OrderByDescending(x => x.LastWriteTime).Skip(keep))
				fi.Delete();
		}

		public void WriteLogEntry(string inputString, LogType logType, string stacktracke = "") {
			lock_.EnterWriteLock();
			try {
				using (StreamWriter currLogFile = File.AppendText(CurrLogFilePath)) {
					currLogFile.WriteLine(string.Concat("\nEntry Nr:::", counter, "::: LogType: ", logType.ToString(), "  LogTime: ", DateTime.Now.ToString("HH:mm:ss  ffff")));
					currLogFile.WriteLine(inputString);
					if (!string.IsNullOrEmpty(stacktracke))
						currLogFile.WriteLine(string.Concat(":::Stacktrace::::\n", stacktracke));
					counter++;
				}
			} catch (Exception ex) {
				UnityEngine.Debug.LogWarningFormat("error while writing Log   Message:{0}", ex);
			} finally {
				lock_.ExitWriteLock();
			}
		}

		public void WriteLogEntry(string inputString) {
			WriteLogEntry(inputString, LogType.Log);
		}
	}
}
