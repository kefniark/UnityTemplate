using Config;

using Scenes.Game.Characters;

using UnityEngine;

namespace Scenes.Game.Camera {
	public class CameraFollow : MonoBehaviour
	{
		private Vector3 originalPosition;
		private CharacterComponent target;

		private void Start()
		{
			originalPosition = transform.position;
			this.Subscribe<CharacterComponent>(EventTopics.GameCharacterSpawn, (character) =>
			{
				if (!character.IsPlayer)
				{
					return;
				}

				target = character;
			});
		}

		private void Update()
		{
			if (target == null)
			{
				return;
			}

			transform.position = target.transform.position + originalPosition;
		}
	}
}
