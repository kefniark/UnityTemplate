using UnityEngine;

[CreateAssetMenu(menuName = "Config/Scene/Add Additive Scene")]
public class SceneAdditiveConfig : ScriptableObject
{
    public string Name = "Scene";
    public SceneConfig SceneParent;
    public bool AutoLoad = false;
    public bool ActiveOnLoad = false;
}