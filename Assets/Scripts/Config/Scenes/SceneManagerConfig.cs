using UnityEngine;

namespace Config.Scenes {
	[CreateAssetMenu(menuName = "Config/Scene/Create Scene Manager")]
	public class SceneManagerConfig : ScriptableObject
	{
		public GameObject LoadingPrefab;
		public SceneConfig[] Scenes;
	}
}
