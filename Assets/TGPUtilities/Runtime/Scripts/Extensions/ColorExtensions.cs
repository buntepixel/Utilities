using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities {
	public static class ColorExtensions {
		/// <summary>
		/// Rounds Color values to 2 digits by default and compares it
		/// </summary>
		/// <param name="color1"></param>
		/// <param name="color2"></param>
		/// <param name="digitsCompare"> digits do round value to</param>
		/// <returns>bool</returns>
		public static bool IsEqual(this Color color1, Color color2, int digitsCompare = 2) {
			int comp = 0;

			int multi = 1;
			for(int i = 0; i < digitsCompare; i++) {
				multi *= 10;
			}

			//Debug.Log($"color1: {color1.r*multi} {color1.g * multi} {color1.b * multi} {color1.a * multi} color2: {color2.r} {color2.g} {color2.b} {color2.a} ");
			comp += Mathf.RoundToInt(color1.r * multi) == Mathf.RoundToInt(color2.r * multi) ? 0 : 1;
			comp += Mathf.RoundToInt(color1.g * multi) == Mathf.RoundToInt(color2.g * multi) ? 0 : 1;
			comp += Mathf.RoundToInt(color1.b * multi) == Mathf.RoundToInt(color2.b * multi) ? 0 : 1;
			comp += Mathf.RoundToInt(color1.a * multi) == Mathf.RoundToInt(color2.a * multi) ? 0 : 1;
			//Debug.Log($"comp: {comp} multi: {multi}");
			return comp == 0;
		}
	}
}
