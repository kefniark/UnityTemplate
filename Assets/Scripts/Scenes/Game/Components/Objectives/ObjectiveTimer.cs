using UnityEngine;

namespace Scenes.Game.Components.Objectives {
	public class ObjectiveTimer : ObjectiveBase
	{
		public float Duration { get; }
		public float Elapsed { get; private set; }

		public ObjectiveTimer(float duration)
		{
			Elapsed = 0;
			Duration = duration;
			ObjectiveComplete();
		}

		/// <inheritdoc />
		public override void Update(float delta)
		{
			if (!Enabled)
			{
				return;
			}

			Elapsed += delta;
			ObjectiveUpdate();

			if (Elapsed < Duration)
			{
				return;
			}

			ObjectiveFail();
		}
	}
}
