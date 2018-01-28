using UnityEngine;
using Utils.EventManager;

public class AdditiveSceneComponent : MonoBehaviour
{
    public SceneAdditiveConfig Config;

    public virtual void Awake()
    {
        SceneManager.Instance.Initialize(this);
    }

    public virtual void Start()
    {
        Publisher.Publish(EventTopics.SceneLoadAdditiveAwake, Config.Name);
    }

    // ToString
    public override string ToString() => $"[AdditiveScene: {name}]";
}