using System.Linq;

using Scenes.Game.Components.Characters;

using UnityEngine;
using UnityEngine.AI;

namespace Scenes.Game.Components.InputControllers {
	public class AIController : MonoBehaviour
	{
		public CharacterComponent Character { get; private set; }
		private NavMeshHit hit;
		private float lastmove;
		private float lastshoot;
		public NavMeshAgent NavAgent;
		public CharacterComponent Target;

		// Use this for initialization
		private void Start()
		{
			Character = GetComponent<CharacterComponent>();
			NavAgent = GetComponentInChildren<NavMeshAgent>();
		}

		// Update is called once per frame
		private void Update()
		{
			if (Target == null)
			{
				SearchForTarget();
				return;
			}

			// move
			if (Time.time - lastmove > 0.2f)
			{
				lastmove = Time.time;
				NavAgent.destination = Target.transform.position;
			}

			// rotate
			transform.LookAt(Target.transform.position);

			// shoot
			if (Time.time - lastshoot > 0.15f)
			{
				lastshoot = Time.time;
				bool blocked = NavMesh.Raycast(transform.position, Target.transform.position, out hit, NavMesh.AllAreas);
				if (!blocked)
				{
					Character.Shoot();
				}
			}
		}

		private void SearchForTarget()
		{
			Target = Character.Level.Characters.Where(x => x != Character).OrderBy(x => Random.value).First();
		}
	}
}
