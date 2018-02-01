using System;
using System.Collections.Generic;

namespace Scenes.Game.Components.Objectives
{
	/// <inheritdoc />
	public class ObjectiveManager : IObjectiveManager
	{
		/// <summary>
		/// Current objective list
		/// </summary>
		public List<IObjective> Objectives { get; } = new List<IObjective>();

		/// <summary>
		/// When the objectives have been completed or failed
		/// </summary>
		private bool finished;

		/// <inheritdoc />
		public event EventHandler<ObjectiveArgs> ObjectiveAdded;

		/// <inheritdoc />
		public event EventHandler<ObjectiveArgs> ObjectiveRemoved;

		/// <inheritdoc />
		public event EventHandler Completed;

		/// <inheritdoc />
		public event EventHandler Failed;

		/// <inheritdoc />
		public void Add(IObjective objective)
		{
			if (objective == null)
			{
				throw new ArgumentNullException(nameof(objective), $"objective is required by {this}");
			}

			Objectives.Add(objective);
			ObjectiveAdded?.Invoke(this, new ObjectiveArgs(objective));
		}

		/// <inheritdoc />
		public void Remove(IObjective objective)
		{
			if (objective == null)
			{
				throw new ArgumentNullException(nameof(objective), $"objective is required by {this}");
			}

			Objectives.Remove(objective);
			ObjectiveRemoved?.Invoke(this, new ObjectiveArgs(objective));
		}

		/// <inheritdoc />
		public void Update(float delta)
		{
			if (finished || Objectives.Count == 0)
			{
				return;
			}

			var win = true;
			var loose = false;

			foreach (IObjective objective in Objectives)
			{
				// Update objective
				objective.Update(delta);

				// Check Objective state
				win = win && objective.Complete;
				loose = loose || objective.Fail;
			}

			// Game Complete
			if (!finished && win)
			{
				Completed?.Invoke(this, EventArgs.Empty);
				finished = true;
			}

			// Game Over
			if (!finished && loose)
			{
				Failed?.Invoke(this, EventArgs.Empty);
				finished = true;
			}
		}

		/// <inheritdoc />
		public override string ToString() => $"[{nameof(ObjectiveManager)}]";
	}
}
