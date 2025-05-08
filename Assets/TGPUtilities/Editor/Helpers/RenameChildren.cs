using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR


namespace TGP.Utilities.Editor
{
	
	using UnityEngine;
	using UnityEditor;

	public class RenameChildren : EditorWindow {

		private static readonly Vector2Int size = new Vector2Int(250, 100);

		private string childrenPrefix;
		private int startIndex;
		private string childrenSuffix;
		private SerializedObject _serializedObject;

		[MenuItem("GameObject/Rename/Rename children")]
		public static void ShowWindow() {
			EditorWindow window = GetWindow<RenameChildren>();
			window.minSize = size;
			window.maxSize = size;
		}
		private void OnEnable() {
			ScriptableObject target = this;
			_serializedObject = new SerializedObject(target);
		}
		private void OnGUI() {
			childrenPrefix = EditorGUILayout.TextField("Children prefix", childrenPrefix);
			childrenSuffix = EditorGUILayout.TextField("Children suffix", childrenSuffix);

			startIndex = EditorGUILayout.IntField("Start index", startIndex);
			_serializedObject.Update();
			if (GUILayout.Button("Rename children")) {
				GameObject[] selectedObjects = Selection.gameObjects;
				for (int objectI = 0; objectI < selectedObjects.Length; objectI++) {
					Transform selectedObjectT = selectedObjects[objectI].transform;
					for (int childI = 0, i = startIndex; childI < selectedObjectT.childCount; childI++) selectedObjectT.GetChild(childI).name = $"{childrenPrefix}{i++}{childrenSuffix}";
				}
			}
			_serializedObject.ApplyModifiedProperties();
		}
	}
	public class RenameSelected : EditorWindow {

		private static readonly Vector2Int size = new Vector2Int(250, 100);

		private string Prefix;
		private int startIndex;
		private string Suffix;
		private SerializedObject _serializedObject;

		[MenuItem("GameObject/Rename/Rename Selected")]
		public static void ShowWindow() {
			EditorWindow window = GetWindow<RenameSelected>();
			window.minSize = size;
			window.maxSize = size;
		}
		private void OnEnable() {
			ScriptableObject target = this;
			_serializedObject = new SerializedObject(target);
		}
		private void OnGUI() {
			Prefix = EditorGUILayout.TextField("prefix", Prefix);
			Suffix = EditorGUILayout.TextField("suffix", Suffix);

			startIndex = EditorGUILayout.IntField("Start index", startIndex);
			_serializedObject.Update();
			if (GUILayout.Button("Rename")) {
				GameObject[] selectedObjects = Selection.gameObjects;
				for (int o = 0; o < selectedObjects.Length; o++) {
					selectedObjects[o].transform.name= $"{Prefix} {o.ToString("D2")} {Suffix}";

					//for (int childI = 0, i = startIndex; childI < selectedObjectT.childCount; childI++)
					//	selectedObjectT.GetChild(childI).name = $"{Prefix}{i++}{Suffix}";
				}
			}
			_serializedObject.ApplyModifiedProperties();
		}
	}
}
#endif