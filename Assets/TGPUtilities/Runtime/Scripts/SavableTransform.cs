using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities {
	[Serializable]
	public class SavableTransform {
		public Vector3 Position;
		public Vector3 LocalPos;
		public Quaternion Rotation;
		public Vector3 Scale;
		public Vector3 LocalScale;
		public SavableTransform(Transform trans) {
			Position = trans.position;
			LocalPos = trans.localPosition;
			Rotation = trans.rotation;
			Scale = trans.lossyScale;
			LocalScale = trans.localScale;
		}
		public Vector3 GetPos(bool local = false) {
			return local ? LocalPos : Position;
		}
		public Vector3 GetScale(bool local = false) {
			return local ? LocalScale : Scale;
		}
		public Quaternion GetRot() {
			return Rotation;
		}
	}
}
