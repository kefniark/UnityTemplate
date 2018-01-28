using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Character/Factory")]
public class CharacterPrefabFactory : ScriptableObject
{
    public CharacterPrefab[] Prefabs;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="container"></param>
    /// <returns></returns>
    public GameObject InstantiatePrefab(string name, Transform container = null)
    {
        var character = Prefabs.FirstOrDefault(x => x.Name.ToLowerInvariant() == name.ToLowerInvariant());
        if (character == null)
        {
            throw new Exception($"{this} Cant find any prefab with name {name}");
        }

        var go = GameObject.Instantiate(character.Prefab);
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