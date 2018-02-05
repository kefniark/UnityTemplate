using Config;

using Scenes.Game.Components.Characters;

using UnityEngine;

namespace Scenes.Game.Components.Bullets {
	public class BulletComponent : MonoBehaviour
	{
		public CharacterComponent Source { get; private set; }
		public Vector3 Origin;
		public Vector3 Direction;
		public float Speed = 10f;
		private int life = 2;

		public void Setup(CharacterComponent source, Vector3 direction)
		{
			Source = source;
			Direction = direction;
			Origin = transform.position;
			life = 2;
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
				this.Publish(EventTopics.GameBulletHitCharacter, this, character);
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
				this.Publish(EventTopics.GameBulletHitWall, this);
			}
		}
	}
}
