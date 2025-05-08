using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;
namespace TGP.Utilities {

	public static class ClassExtensions {

		public static byte[] GetBytesFromStream(Stream input) {
			using (MemoryStream ms = new MemoryStream()) {
				input.CopyTo(ms);
				return ms.ToArray();
			}
		}
		public static bool IsNullOrEmpy(this Array input) {
			if (input == null || input.Length == 0) {
				return true;
			}
			return false;
		}
		public static string ToStringZeroEmpty(this float input, CultureInfo cultureInfo) {
			if (input == 0) {
				return "";
			} else {
				return input.ToString(cultureInfo);
			}
		}
		public static int IndexOf<T>(this IList<T> list, T item) {
			for (int i = 0; i < list.Count; i++) {
				if (object.Equals(list[i], item)) {
					return i;
				}
			}

			return -1;
		}
		/// <summary>
		/// The following implementation uses the Fisher-Yates algorithm AKA the Knuth Shuffle. 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="rng"></param>
		/// <param name="array"></param>
		/// <returns>new shuffle array</returns>

		public static T[] Shuffle<T>(this System.Random rng, T[] array) {
			T[] newArray = array.Clone() as T[];
			for (int i = newArray.Length; i > 1; i--) {
				// Pick random element to swap.
				int j = rng.Next(i); // 0 <= j <= i-1
									 // Swap.
				T tmp = newArray[j];
				newArray[j] = newArray[i - 1];
				newArray[i - 1] = tmp;
			}
			return newArray;
		}
	}
}
