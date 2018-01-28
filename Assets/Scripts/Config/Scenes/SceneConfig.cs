using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Scene/Add Scene")]
public class SceneConfig : ScriptableObject
{
    public string Name = "Scene";
    public bool Async = true;
    public SceneNext[] LinkTo;
    public SceneAdditiveConfig[] Additives;
}

[Serializable]
public class SceneNext
{
    public string TriggerName;
    public SceneConfig SceneName;
}