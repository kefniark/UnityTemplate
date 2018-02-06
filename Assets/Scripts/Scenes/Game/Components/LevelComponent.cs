using System.Collections.Generic;

using Config;
using Config.Characters;

using Scenes.Game.Components.Bullets;
using Scenes.Game.Components.Characters;
using Scenes.Game.Components.InputControllers;
using Scenes.Game.States;

using UnityEngine;

using Utils.EventManager;
using Utils.ObjectPooling;

namespace Scenes.Game.Components {
	public class LevelComponent : MonoBehaviour
	{
		// Prefabs
		public CharacterPrefabFactory CharacterFactory { get; private set; }
		public PoolComponent PrefabFactory { get; private set; }

		public List<CharacterComponent> Characters { get; } = new List<CharacterComponent>();
		public Transform[] SpawnPoints;

		public void Setup(CharacterPrefabFactory characterFactory, PoolComponent prefabFactory)
		{
			CharacterFactory = characterFactory;
			PrefabFactory = prefabFactory;
			InitGameObjectInteractions();
		}

		#region GameObject Interactions

		private void InitGameObjectInteractions()
		{
			this.Subscribe<CharacterComponent, Vector3, Vector3>(EventTopics.GameBulletShoot, OnBulletShoot);
			this.Subscribe<BulletComponent, CharacterComponent>(EventTopics.GameBulletHitCharacter, OnBulletHitCharacter);
			this.Subscribe<BulletComponent>(EventTopics.GameBulletHitWall, OnBulletHitWall);
			this.Subscribe<GameStateTransition>(string.Format(EventTopics.GameStateEnter, GameStates.RoundIntro.ToString()), OnRoundIntro);
		}

		private void OnRoundIntro(GameStateTransition obj)
		{
			GameObject sfx = PrefabFactory.Pop("sfxstartup");
			this.DelayAction(4f, () => PrefabFactory.Push("sfxstartup", sfx));
		}

		private void OnBulletShoot(CharacterComponent owner, Vector3 position, Vector3 direction)
		{
			GameObject go = PrefabFactory.Pop("bullet");
			go.transform.position = position;
			go.GetComponent<BulletComponent>().Setup(owner, direction);

			GameObject sfx = PrefabFactory.Pop("sfxshoot");
			sfx.transform.position = position;
			this.DelayAction(2f, () => PrefabFactory.Push("sfxshoot", sfx));
		}

		private void OnBulletHitCharacter(BulletComponent bullet, CharacterComponent character)
		{
			character.Hit();
			if (character.CurrentHealth <= 0 && bullet.Source != null)
			{
				bullet.Source.Player.IncrementScore((bullet.Source == character) ? -1 : 1);

				//GameObject go = PrefabFactory.Pop("bulletexplode");
				//go.transform.position = bullet.transform.position;
				//this.DelayAction(8, () => PrefabFactory.Push("bulletexplode", go));

				GameObject explode = PrefabFactory.Pop("vfxexplode");
				explode.transform.position = bullet.transform.position + (UnityEngine.Camera.main.transform.position - bullet.transform.position).normalized;
				this.DelayAction(5, () => PrefabFactory.Push("vfxexplode", explode));

				GameObject shock = PrefabFactory.Pop("shockwave");
				shock.transform.position = bullet.transform.position;
				shock.transform.LookAt(UnityEngine.Camera.main.transform);
				this.DelayAction(2.2f, () => PrefabFactory.Push("shockwave", shock));

				string sfxName = Random.Range(0f, 1f) > 0.5 ? "sfxdeath1" : "sfxdeath2";
				GameObject sfx = PrefabFactory.Pop(sfxName);
				sfx.transform.position = bullet.transform.position;
				this.DelayAction(4f, () => PrefabFactory.Push(sfxName, sfx));
			}

			PrefabFactory.Push("bullet", bullet.gameObject);
		}

		private void OnBulletHitWall(BulletComponent bullet)
		{
			PrefabFactory.Push("bullet", bullet.gameObject);
		}

		#endregion

		#region Round Management

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
					go.AddComponent<AiController>();
				}

				Publisher.Publish(EventTopics.GameCharacterSpawn, character);
			}
		}

		#endregion
	}
}
