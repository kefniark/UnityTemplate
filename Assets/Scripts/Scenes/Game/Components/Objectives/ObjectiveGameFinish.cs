using Config;

using Scenes.Game.Components.Characters;

using Utils.EventManager;

namespace Scenes.Game.Components.Objectives {
	public class ObjectiveGameFinish : ObjectiveBase
	{
		public float ScoreMax { get; }

		public ObjectiveGameFinish(int scoreTarget)
		{
			ScoreMax = scoreTarget;
			var subscriber = new Subscriber();
			subscriber.Subscribe<PlayerData>(EventTopics.GamePlayerUpdated, OnPlayerScoreUpdated);
		}

		private void OnPlayerScoreUpdated(PlayerData obj)
		{
			if (obj.Score >= ScoreMax)
			{
				ObjectiveComplete();
			}
		}
	}
}
