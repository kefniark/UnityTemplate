using System;

using UnityEngine;

namespace Config.Scenes {
	[CreateAssetMenu(menuName = "Config/Scene/Add Scene")]
	public class SceneConfig : ScriptableObject
	{
		public SceneAdditiveConfig[] Additives;
		public bool Async = true;
		public SceneNext[] LinkTo;
		public string Name = "Scene";
	}

	[Serializable]
	public class SceneNext
	{
		public SceneConfig SceneName;
		public string TriggerName;
	}
}