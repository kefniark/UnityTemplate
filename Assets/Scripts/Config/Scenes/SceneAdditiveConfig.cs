using UnityEngine;

namespace Config.Scenes {
	[CreateAssetMenu(menuName = "Config/Scene/Add Additive Scene")]
	public class SceneAdditiveConfig : ScriptableObject
	{
		public bool ActiveOnLoad = false;
		public bool AutoLoad = false;
		public string Name = "Scene";
		public SceneConfig SceneParent;
	}
}
