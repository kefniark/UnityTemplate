using System.Collections.Generic;

using Config;
using Config.Characters;

using Scenes.Game.Components.Characters;
using Scenes.Game.Components.InputControllers;

using UnityEngine;

using Utils.EventManager;

namespace Scenes.Game.Components {
	public class LevelComponent : MonoBehaviour
	{
		public CharacterPrefabFactory CharacterFactory { get; private set; }
		public List<CharacterComponent> Characters { get; } = new List<CharacterComponent>();
		public Transform[] SpawnPoints;

		public void Setup(CharacterPrefabFactory characterFactory)
		{
			CharacterFactory = characterFactory;
		}

		public void CleanRound()
		{
			for (int i = Characters.Count - 1; i >= 0; i--)
			{
				if (Characters[i] != null)
				{
					GameObject.Destroy(Characters[i].gameObject);
				}
			}
			Characters.Clear();
		}

		public void InitRound(List<PlayerData> players)
		{
			var colors = new List<string>
			{
				"blue",
				"red",
				"red",
				"yellow"
			};
			colors.Shuffle();
			players.Shuffle();

			for (var i = 0; i < colors.Count; i++)
			{
				string color = colors[i];
				Transform spawn = SpawnPoints[i];
				GameObject go = CharacterFactory.InstantiatePrefab(color, transform);
				go.transform.localPosition = new Vector3(spawn.transform.position.x, spawn.transform.position.y, spawn.transform.position.z);

				var character = go.GetComponent<CharacterComponent>();
				character.Setup(this, players[i]);

				Characters.Add(character);

				if (players[i].IsPlayer)
				{
					go.AddComponent<InputController>();
				}
				else
				{
					go.AddComponent<AIController>();
				}

				Debug.Log($"Created Character {character}");

				Publisher.Publish(EventTopics.GameCharacterSpawn, character);
			}
		}
	}
}
