using System;
using System.Linq;

using UnityEngine;

namespace Config.Characters {
	[CreateAssetMenu(menuName = "Config/Character/Factory")]
	public class CharacterPrefabFactory : ScriptableObject
	{
		public CharacterPrefab[] Prefabs;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="characterName"></param>
		/// <param name="container"></param>
		/// <returns></returns>
		public GameObject InstantiatePrefab(string characterName, Transform container = null)
		{
			CharacterPrefab character = Prefabs.FirstOrDefault(x => String.Equals(x.Name, characterName, StringComparison.InvariantCultureIgnoreCase));
			if (character == null)
			{
				throw new Exception($"{this} Cant find any prefab with name {name}");
			}

			GameObject go = Instantiate(character.Prefab);
			if (container != null)
			{
				go.transform.SetParent(container, false);
			}

			return go;
		}
	}

	[Serializable]
	public class CharacterPrefab
	{
		public string Name;
		public GameObject Prefab;
	}
}