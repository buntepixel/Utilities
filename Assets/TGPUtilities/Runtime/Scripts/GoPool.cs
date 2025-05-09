using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities {
	public class GoPool : BasePool<GameObject> {
	
		List<GameObject> activeObj;

		public GoPool(Transform poolParent, GameObject instanceObj, string poolName = "pool") : base(poolParent, instanceObj, poolName) {
			activeObj = new List<GameObject>();
		}

		/// <summary>
		/// Adds an Item to the pool and deactivates it.
		/// </summary>
		/// <param name="item">the item to add to the pool</param>
		public override void ReturnItem(GameObject item) {
			base.ReturnItem(item);
			item.transform.SetParent(poolObj.transform);
			item.gameObject.SetActive(false);
			}

		public override GameObject GetItem() {
			return GetItem(GetPoolRoot());
			}

		public override GameObject GetItem(Transform parent) {
			GameObject item = base.GetItem(parent);
			if (item == null) {
				item = Object.Instantiate(instanceObj, parent, false);
				}
			else
				item.transform.SetParent(parent);
			activeObj.Add(item);
			item.gameObject.SetActive(true);
			return item;
			}
		public void CleanupActivesToPool() {
			if (activeObj.Count == 0)
				return;
			foreach(GameObject item in activeObj) {
				ReturnItem(item);
				}
			activeObj.Clear();
			}
		public List<GameObject> GetActiveObj() {
			return activeObj;
			}
	

		public override void Dispose() {
			Object.Destroy(poolObj);
		}
	}
	}
