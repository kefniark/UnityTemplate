using UnityEngine;
using Utils.EventManager;

/// <summary>
/// Base class for scene object
/// 
/// Used to publish proper events
/// </summary>
public abstract class SceneComponent : MonoBehaviour
{
    public virtual void Awake()
    {
        SceneManager.Instance.Initialize(this);
        Publisher.Publish(EventTopics.SceneAwake);
    }

    // Next Scene
    protected void LoadNextScene(string name = "next") => Publisher.Publish(EventTopics.SceneChange, name);
    protected void LoadAdditiveScene(string name) => Publisher.Publish(EventTopics.SceneLoadAdditive, name);

    // Loading
    protected void LoadingBegin() => Publisher.Publish(EventTopics.SceneLoading);
    protected void LoadingEnd() => Publisher.Publish(EventTopics.SceneLoaded);

    // ToString
    public override string ToString() => $"[Scene: {name}]";
}
