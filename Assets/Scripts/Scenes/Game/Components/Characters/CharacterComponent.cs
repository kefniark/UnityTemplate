using Config;

using UnityEngine;
using UnityEngine.AI;

namespace Scenes.Game.Components.Characters {
	public class CharacterComponent : MonoBehaviour
	{
		public LevelComponent Level { get; private set; }
		public bool IsPlayer => Player.IsPlayer;
		public GameObject BulletPrefab;
		public Transform BulletSpawn;
		public PlayerData Player { get; private set; }
		public int Health = 5;

		public void Setup(LevelComponent level, PlayerData player)
		{
			Level = level;
			Player = player;
			var agent = GetComponent<NavMeshAgent>();
			agent.enabled = true;
		}

		public void Shoot()
		{
			GameObject go = Instantiate(BulletPrefab);
			go.transform.position = BulletSpawn.position;
			go.GetComponent<BulletComponent>().Setup(this, BulletSpawn.position - transform.position);
		}

		public void Hit()
		{
			Health -= 1;
			if (Health == 0)
			{
				this.Publish(EventTopics.GameCharacterDeath, this);
				Destroy(gameObject);
			}
		}

		public override string ToString()
		{
			return $"[Character {Player} {IsPlayer}]";
		}
	}
}
