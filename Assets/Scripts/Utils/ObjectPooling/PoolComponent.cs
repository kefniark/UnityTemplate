using System;
using System.Collections.Generic;

using UnityEngine;

namespace Utils.ObjectPooling
{
	/// <summary>
	/// Component used to instantiate/store/recycle prefabs
	///
	/// Used to avoid memory allocation / garbage collection
	/// </summary>
	public class PoolComponent : MonoBehaviour
	{
		public ObjectPoolItem[] Items;
		public Dictionary<string, ObjectPool> Pools { get; private set; }

		public void Start()
		{
			Pools = new Dictionary<string, ObjectPool>();
			foreach (ObjectPoolItem item in Items)
			{
				var go = new GameObject();
				go.transform.SetParent(transform, false);
				go.name = item.Id;
				var pool = go.AddComponent<ObjectPool>();
				pool.Init(item);
				Pools.Add(item.Id.ToLowerInvariant(), pool);
			}
		}

		/// <summary>
		/// Method used to get an instance of a specific item from the pool
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public GameObject Pop(string id)
		{
			if (!Pools.ContainsKey(id.ToLowerInvariant()))
			{
				throw new Exception($"{this} this pool Id is unknown : {id}");
			}

			ObjectPool pool = Pools[id.ToLowerInvariant()];
			return pool.Pop();
		}

		/// <summary>
		/// Method used to push back in the pool an instance of a specific item
		/// </summary>
		/// <param name="id"></param>
		/// <param name="go"></param>
		public void Push(string id, GameObject go)
		{
			if (!Pools.ContainsKey(id.ToLowerInvariant()))
			{
				throw new Exception($"{this} this pool Id is unknown : {id}");
			}

			ObjectPool pool = Pools[id.ToLowerInvariant()];
			pool.Push(go);
		}

		public override string ToString() => "[PoolComponent]";
	}
}
