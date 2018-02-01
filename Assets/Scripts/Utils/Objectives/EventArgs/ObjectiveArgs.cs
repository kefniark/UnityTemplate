using System;

namespace Scenes.Game
{
	/// <summary>
	/// EventArg used when an objective is changed
	/// </summary>
	public class ObjectiveArgs : EventArgs
	{
		/// <summary>
		/// Objective that has changed
		/// </summary>
		public IObjective Objective { get; }

		/// <summary>
		/// Objective changed args
		/// </summary>
		/// <param name="objective">Objective that is being updated</param>
		public ObjectiveArgs(IObjective objective)
		{
			if (objective == null)
			{
				throw new ArgumentNullException(nameof(objective), $"an objective is required by {this}");
			}

			Objective = objective;
		}

		/// <inheritdoc />
		public override string ToString() => $"[{nameof(ObjectiveArgs)} {nameof(Objective)}: {Objective}]";
	}
}
