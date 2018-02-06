using System;
using System.Collections;
using System.Linq;

using Config;
using Config.Scenes;

using UnityEngine;

using Utils.EventManager;

using Object = UnityEngine.Object;
using SceneManagement = UnityEngine.SceneManagement.SceneManager;

namespace Utils.Scenes {

	/// <summary>
	/// This scene manager use Unity API to load/switch scenes
	/// 
	/// This also provide:
	/// * loading screen (through a simple customizable prefab)
	/// * async scene loading
	/// * additive scene
	/// </summary>
	public class SceneManager
	{
		public static SceneManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new SceneManager();
				}

				return instance;
			}
		}

		private static SceneManager instance = null;
		private SceneManagerConfig config;

		private SceneComponent currentScene;

		public SceneManager()
		{
			var subscriber = new Subscriber();
			var currentSceneName = "";
			GameObject loading = null;

			// Scene Loaded
			subscriber.Subscribe(EventTopics.SceneAwake, () => currentSceneName = SceneManagement.GetActiveScene().name);

			// Scene Change
			subscriber.Subscribe<string>(EventTopics.SceneChange, (triggerName) =>
			{
				SceneConfig scene = config.Scenes.FirstOrDefault(x => String.Equals(x.Name, currentSceneName, StringComparison.InvariantCultureIgnoreCase));
				if (scene == null)
				{
					throw new Exception($"{this} no configuration for the current scene: {currentSceneName}");
				}

				SceneNext next = scene.LinkTo.FirstOrDefault(x => String.Equals(x.TriggerName, triggerName, StringComparison.InvariantCultureIgnoreCase));
				if (next == null)
				{
					throw new Exception($"{this} no configuration for this trigger scene: {currentSceneName}, trigger: {triggerName}");
				}

				SceneConfig nextConfig = config.Scenes.FirstOrDefault(x => String.Equals(x.Name, next.SceneName.Name, StringComparison.InvariantCultureIgnoreCase));
				if (nextConfig == null)
				{
					throw new Exception($"{this} no configuration for this scene: {next.SceneName.Name}");
				}

				if (nextConfig.Async)
				{
					SceneManagement.LoadSceneAsync(next.SceneName.Name, UnityEngine.SceneManagement.LoadSceneMode.Single);
				}
				else
				{
					SceneManagement.LoadScene(next.SceneName.Name);
				}
			});

			// Scene Loading
			subscriber.Subscribe(EventTopics.SceneLoading, () =>
			{
				loading = Object.Instantiate(config.LoadingPrefab);
				loading.GetComponent<CanvasGroup>().alpha = 1;
			});

			subscriber.Subscribe(EventTopics.SceneLoaded, () =>
			{
				loading.GetComponent<CanvasGroup>().alpha = 0;
				Object.Destroy(loading);
				loading = null;
			});

			// Additive Scene
			subscriber.Subscribe<string>(EventTopics.SceneLoadAdditive, (name) =>
			{
				SceneConfig scene = config.Scenes.FirstOrDefault(x => x.Name.ToLowerInvariant() == currentSceneName.ToLowerInvariant());
				if (scene == null)
				{
					throw new Exception($"{this} no configuration for the current scene: {currentSceneName}");
				}

				SceneAdditiveConfig additive = scene.Additives.FirstOrDefault(x => x.Name.ToLowerInvariant() == name.ToLowerInvariant());
				if (additive == null)
				{
					throw new Exception($"{this} no configuration for this additive scene: {currentSceneName}, additive: {name}");
				}

				currentScene.StartCoroutine(LoadAdditiveScene(additive.Name));
			});

			subscriber.Subscribe<string>(EventTopics.SceneLoadAdditiveAwake, (name) =>
			{
				// if the additive scene is opened directly
				if (currentScene == null)
				{
					return;
				}

				SceneConfig scene = config.Scenes.FirstOrDefault(x => x.Name.ToLowerInvariant() == currentSceneName.ToLowerInvariant());
				if (scene == null)
				{
					throw new Exception($"{this} no configuration for the current scene: {currentSceneName}");
				}

				SceneAdditiveConfig additive = scene.Additives.FirstOrDefault(x => x.Name.ToLowerInvariant() == name.ToLowerInvariant());
				if (additive == null)
				{
					throw new Exception($"{this} no configuration for this additive scene: {currentSceneName}, additive: {name}");
				}

				if (additive.ActiveOnLoad)
				{
					SceneManagement.SetActiveScene(SceneManagement.GetSceneByName(additive.Name));
				}
			});
		}

		public void Initialize(SceneComponent component)
		{
			currentScene = component;
			if (config != null)
			{
				return;
			}

			config = Resources.Load<SceneManagerConfig>("SceneManagerConfig");
			if (config == null)
			{
				throw new Exception($"{this} Cant load scene manager config from Resources/SceneManagerConfig");
			}
		}

		public void Initialize(AdditiveSceneComponent component)
		{
			if (config != null)
			{
				return;
			}

			config = Resources.Load<SceneManagerConfig>("SceneManagerConfig");
			if (config == null)
			{
				throw new Exception($"{this} Cant load scene manager config from Resources/SceneManagerConfig");
			}

			string parentSceneName = component.Config.SceneParent.Name;

			SceneConfig scene = config.Scenes.FirstOrDefault(x => x.Name.ToLowerInvariant() == parentSceneName.ToLowerInvariant());
			if (scene == null)
			{
				throw new Exception($"{this} no configuration for the current scene: {parentSceneName}");
			}

			if (scene.Async)
			{
				SceneManagement.LoadSceneAsync(scene.Name, UnityEngine.SceneManagement.LoadSceneMode.Single);
			}
			else
			{
				SceneManagement.LoadScene(scene.Name);
			}
		}

		private IEnumerator LoadAdditiveScene(string scene)
		{
			AsyncOperation async = SceneManagement.LoadSceneAsync(scene, UnityEngine.SceneManagement.LoadSceneMode.Additive);

			while (!async.isDone)
			{
				yield return null;
			}

			Publisher.Publish(EventTopics.SceneLoadAdditiveLoaded, scene);
		}

		public override string ToString()
		{
			return "[SceneManager]";
		}
	}
}
