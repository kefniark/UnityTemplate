using System.ComponentModel;

using Config;

using UnityEngine;
using UnityEngine.AI;

namespace Scenes.Game.Components.Characters {
	public class CharacterComponent : MonoBehaviour
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public LevelComponent Level { get; private set; }
		public bool IsPlayer => Player.IsPlayer;
		public Transform BulletSpawn;
		public PlayerData Player { get; private set; }
		public int Health = 5;
		public int CurrentHealth { get; private set; }

		public void Setup(LevelComponent level, PlayerData player)
		{
			Level = level;
			Player = player;
			var agent = GetComponent<NavMeshAgent>();
			agent.enabled = true;

			CurrentHealth = Health;
		}

		public void Shoot()
		{
			this.Publish(EventTopics.GameBulletShoot, this, BulletSpawn.position, BulletSpawn.position - transform.position);
		}

		public void Hit()
		{
			CurrentHealth -= 1;
			OnPropertyChanged("Health");
			if (CurrentHealth == 0)
			{
				this.Publish(EventTopics.GameCharacterDeath, this);
				Destroy(gameObject);
			}
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public override string ToString()
		{
			return $"[Character Health:{CurrentHealth}/{Health} Data:{Player} IsPlayer:{IsPlayer}]";
		}
	}
}
