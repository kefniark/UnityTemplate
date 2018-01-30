using System.Collections;

using Config;
using Config.Characters;

using Utils.Scenes;
using Utils.StateMachine;

namespace Scenes.Game {
	public class GameView : SceneComponent
	{
		public CharacterPrefabFactory CharacterFactory;

		public void ClickButton() => LoadNextScene();

		public IEnumerator Start()
		{
			LoadingBegin();
			LoadAdditiveScene("Level1");
			yield return this.WaitForMessage(EventTopics.SceneLoadAdditiveLoaded);
			LoadingEnd();
			StartGame();
		}

		private void StartGame()
		{
			var level = FindObjectOfType<LevelComponent>();
			if (level == null)
			{
				throw new System.Exception($"{this} cant find a level component");
			}

			level.Setup(CharacterFactory);
		}
	}
}
