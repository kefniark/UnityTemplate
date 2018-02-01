using System.Collections.Generic;

using Config;

using Scenes.Game.Components.Characters;

using UnityEngine;

using Utils.EventManager;

namespace Scenes.Game.Components.Objectives {
	public class ObjectiveGameFinish : ObjectiveBase
	{
		public float ScoreMax { get; }
		public readonly List<CharacterComponent> Characters = new List<CharacterComponent>();

		public ObjectiveGameFinish(int scoreTarget)
		{
			ScoreMax = scoreTarget;
			var subscriber = new Subscriber();
			subscriber.Subscribe<PlayerData>(EventTopics.GamePlayerUpdated, OnPlayerScoreUpdated);
		}

		private void OnPlayerScoreUpdated(PlayerData obj)
		{
			Debug.LogWarning($"OnPlayerScoreUpdated {obj}");
			if (obj.Score >= ScoreMax)
			{
				ObjectiveComplete();
			}
		}
	}
}
