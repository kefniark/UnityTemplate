using Config;

using Utils.EventManager;

namespace Scenes.Game.Components.Characters
{
	public class PlayerData
	{
		public string Name { get; }
		public int Index { get; }
		public bool IsPlayer { get; }
		public int Score { get; private set; }

		public PlayerData(string name, int index, bool isPlayer)
		{
			Name = name;
			Index = index;
			IsPlayer = isPlayer;
			Publisher.Publish(EventTopics.GamePlayerCreated, this);
		}

		public void IncrementScore(int val = 1)
		{
			Score += val;
			Publisher.Publish(EventTopics.GamePlayerUpdated, this);
		}

		public override string ToString() => $"[Player Index:{Index} Name:{Name} Score:{Score}]";
	}
}
