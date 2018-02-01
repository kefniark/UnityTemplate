using UnityEngine;

namespace Scenes.Game.Components.Characters {
	public class BulletComponent : MonoBehaviour
	{
		public CharacterComponent Source;
		public Vector3 Origin;
		public Vector3 Direction;
		public float Speed = 10f;
		private int life = 4;

		public void Setup(CharacterComponent source, Vector3 direction)
		{
			Source = source;
			Direction = direction;
			Origin = transform.position;
			Destroy(gameObject, 3);
		}

		private void Update()
		{
			transform.Translate(Direction * Time.deltaTime * Speed);
		}

		private void OnTriggerEnter(Collider other)
		{
			var character = other.gameObject.GetComponent<CharacterComponent>();
			if (character != null)
			{
				character.Hit();
				if (character.Health <= 0 && Source != null)
				{
					Source.Player.IncrementScore((Source == character) ? -1 : 1);
				}

				Destroy(gameObject);
				return;
			}

			life -= 1;

			RaycastHit hit;
			if (Physics.Raycast(Origin, Direction, out hit, 50, LayerMask.GetMask("Wall")))
			{
				Direction = Vector3.Reflect(Direction, hit.normal);
				Origin = transform.position;
			}
			else
			{
				life = 0;
			}

			if (life <= 0)
			{
				GameObject.Destroy(gameObject);
			}
		}
	}
}
