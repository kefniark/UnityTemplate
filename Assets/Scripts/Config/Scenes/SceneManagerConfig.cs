using UnityEngine;

[CreateAssetMenu(menuName = "Config/Scene/Create Scene Manager")]
public class SceneManagerConfig : ScriptableObject
{
    public GameObject LoadingPrefab;
    public SceneConfig[] Scenes;
}
