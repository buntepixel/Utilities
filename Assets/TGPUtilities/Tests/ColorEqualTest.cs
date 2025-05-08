using NUnit.Framework;
using UnityEngine;
using TGP.Utilities;

namespace TGP.Utilities.testing.test {
	public class ColorEqualTest {
		// A Test behaves as an ordinary method
		[Test]
		public void Color_Equality_Compare() {
			//Arange
			Color[] color = new[] { new Color(0.11111111f, 0.11111111f, 0.11111111f, 0.11111111f), new(0.1111111f, 0.1111111f, 0.1111111f, 0.1111111f) };
			Color[] colorOffOnePlus = new[] { new Color(0.11111112f, 0.11111110f, 0.11111111f, 0.11111111f), new(0.1111112f, 0.1111111f, 0.1111111f, 0.1111111f) };
			Color[] colorOffTwoPlus = new[] { new Color(0.11111131f, 0.11111111f, 0.11111111f, 0.11111111f), new(0.1111113f, 0.1111111f, 0.1111111f, 0.1111111f) };



			bool[] expectedSame = new bool[2];

			bool[] expected = new bool[2];
			bool[] expectedBad = new bool[2];
			//Act
			for (int i = 0; i < color.Length; i++) {
				expectedSame[i] = color[i].IsEqual(color[i]);
				expected[i] = color[i].IsEqual(colorOffOnePlus[i]);
				expectedBad[i] = color[i].IsEqual(colorOffTwoPlus[i]);
			}
			//Assert
			foreach (var item in expectedSame) {
				Debug.Log(item);
				Assert.IsTrue(item, message: "should all be true");
			}
			foreach (var item in expected) {
				Debug.Log(item);
				Assert.IsTrue(item, message: "should all be true");
			}
			foreach (var item in expectedBad) {
				Debug.Log(item);
				Assert.IsFalse(item, message: "should all be false");
			}
		}
	}
}
