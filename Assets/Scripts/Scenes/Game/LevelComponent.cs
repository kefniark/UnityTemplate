using System.Collections.Generic;

using Config;
using Config.Characters;

using Scenes.Game.Characters;
using Scenes.Game.InputControllers;

using UnityEngine;

using Utils.EventManager;

namespace Scenes.Game {
	public class LevelComponent : MonoBehaviour
	{
		public CharacterPrefabFactory CharacterFactory { get; private set; }
		public List<CharacterComponent> Characters { get; } = new List<CharacterComponent>();
		public Transform[] SpawnPoints;

		public void Setup(CharacterPrefabFactory characterFactory)
		{
			CharacterFactory = characterFactory;
			InitializeCharacters();
		}

		public void InitializeCharacters()
		{
			var colors = new List<string>
			{
				"blue",
				"red",
				"red",
				"yellow"
			};
			colors.Shuffle();

			for (var i = 0; i < colors.Count; i++)
			{
				string color = colors[i];
				Transform spawn = SpawnPoints[i];
				GameObject go = CharacterFactory.InstantiatePrefab(color, transform);
				go.transform.localPosition = new Vector3(spawn.transform.position.x, spawn.transform.position.y, spawn.transform.position.z);

				var character = go.GetComponent<CharacterComponent>();
				character.Setup(this, i == 0);

				Characters.Add(character);
				if (i == 0)
				{
					go.AddComponent<InputController>();
				}
				else
				{
					go.AddComponent<AIController>();
				}

				Publisher.Publish(EventTopics.GameCharacterSpawn, character);
			}
		}
	}
}
