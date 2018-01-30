using Config;

using UnityEngine;

using Utils.EventManager;

namespace Utils.Scenes {
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
		protected void LoadNextScene(string sceneName = "next") => Publisher.Publish(EventTopics.SceneChange, sceneName);
		protected void LoadAdditiveScene(string sceneName) => Publisher.Publish(EventTopics.SceneLoadAdditive, sceneName);

		// Loading
		protected void LoadingBegin() => Publisher.Publish(EventTopics.SceneLoading);
		protected void LoadingEnd() => Publisher.Publish(EventTopics.SceneLoaded);

		// ToString
		public override string ToString() => $"[Scene: {name}]";
	}
}
