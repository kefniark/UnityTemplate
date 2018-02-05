using System.Collections.Generic;
using System.Linq;

using Config;

using Scenes.Game.Components.Characters;

using UnityEngine;

using Utils.EventManager;

namespace Scenes.Game.Components.Objectives {
	public class ObjectiveRoundFinish : ObjectiveBase
	{
		public readonly List<CharacterComponent> Characters = new List<CharacterComponent>();

		public ObjectiveRoundFinish()
		{
			var subscriber = new Subscriber();
			subscriber.Subscribe<CharacterComponent>(EventTopics.GameCharacterSpawn, OnCharacterSpawn);
			subscriber.Subscribe<CharacterComponent>(EventTopics.GameCharacterDeath, OnCharacterDeath);
		}

		private void OnCharacterSpawn(CharacterComponent character)
		{
			Characters.Add(character);
		}

		private void OnCharacterDeath(CharacterComponent character)
		{
			Characters.Remove(character);
			ObjectiveUpdate();
			if (Characters.Count <= 1)
			{
				CharacterComponent last = Characters.FirstOrDefault();
				if (last != null)
				{
					Debug.Log($"Last Character Get Point {last.Player}");
					last.Player.IncrementScore(2);
				}

				ObjectiveComplete();
			}
		}
	}
}
