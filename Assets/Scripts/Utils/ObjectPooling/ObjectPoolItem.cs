using System;

using UnityEngine;

namespace Utils.ObjectPooling
{
	/// <summary>
	/// Pool Item
	///
	/// Configuration of an item used in unity
	/// </summary>
	[Serializable]
	public struct ObjectPoolItem
	{
		public string Id;
		public GameObject Prefab;
		public int StartAmount;
		public bool Prewarm;
	}

}
