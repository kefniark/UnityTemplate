using UnityEngine;
using UnityEngine.AI;

namespace Scenes.Game.Characters {
	public class CharacterComponent : MonoBehaviour
	{
		public LevelComponent Level { get; private set; }
		public bool IsPlayer { get; private set; }
		public GameObject BulletPrefab;
		public Transform BulletSpawn;
		public int Health = 10;

		public void Setup(LevelComponent level, bool isPlayer)
		{
			Level = level;
			IsPlayer = isPlayer;
			var agent = GetComponent<NavMeshAgent>();
			agent.enabled = true;
		}

		public void Shoot()
		{
			GameObject go = Instantiate(BulletPrefab);
			go.transform.position = BulletSpawn.position;
			go.GetComponent<BulletComponent>().Setup(BulletSpawn.position - transform.position);
		}

		public void Hit()
		{
			Health -= 1;
			if (Health == 0)
			{
				Destroy(gameObject);
			}
		}
	}
}
