using System.Collections.Generic;

using UnityEngine;

namespace Utils.ObjectPooling
{
	/// <summary>
	/// Pool for one type of Item
	/// </summary>
	public class ObjectPool : MonoBehaviour
	{
		public ObjectPoolItem Item { get; private set; }
		public List<GameObject> Pool { get; private set; }

		/// <summary>
		/// Initialization of the pool
		/// 
		/// Already spawn a certain amount of prefab
		/// </summary>
		/// <param name="item"></param>
		public void Init(ObjectPoolItem item)
		{
			Item = item;
			Pool = new List<GameObject>();
			for (var i = 0; i < Item.StartAmount; i++)
			{
				Pool.Add(Instantiate());
			}

			if (Item.Prewarm)
			{
				GameObject go = Instantiate();
				go.transform.position = new Vector3(100000, 100000, 100000);
				go.SetActive(true);
				GameObject.Destroy(go, 5);
			}
		}

		/// <summary>
		/// Method used to instantiate a prefab
		/// </summary>
		/// <param name="active"></param>
		/// <returns></returns>
		private GameObject Instantiate(bool active = false)
		{
			GameObject go = GameObject.Instantiate(Item.Prefab);
			go.transform.SetParent(transform, false);
			go.SetActive(active);
			return go;
		}

		/// <summary>
		/// Method used to get a instance of that item
		/// </summary>
		/// <returns></returns>
		public GameObject Pop()
		{
			if (Pool.Count == 0)
			{
				return Instantiate(true);
			}

			GameObject go = Pool[0];
			Pool.RemoveAt(0);
			go.SetActive(true);

			return go;
		}

		/// <summary>
		/// Method used to push back an instance in the pool
		/// </summary>
		/// <param name="go"></param>
		public void Push(GameObject go)
		{
			go.SetActive(false);
			Pool.Add(go);
		}
	}
}
