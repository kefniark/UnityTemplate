using System;
using System.Collections.Generic;

namespace Scenes.Game
{
	/// <summary>
	/// Objective manager manages the objectives in the level and makes sure
	/// to let everyone know when they are complete or failed.
	/// </summary>
	public interface IObjectiveManager
	{
		List<IObjective> Objectives { get; }

		/// <summary>
		/// When an objective is added
		/// </summary>
		event EventHandler<ObjectiveArgs> ObjectiveAdded;

		/// <summary>
		/// When objective is removed
		/// </summary>
		event EventHandler<ObjectiveArgs> ObjectiveRemoved;

		/// <summary>
		/// When all objectives have been completed
		/// </summary>
		event EventHandler Completed;

		/// <summary>
		/// When the objectives have been failed
		/// </summary>
		event EventHandler Failed;

		/// <summary>
		/// Add an objective for the level
		/// </summary>
		/// <param name="objective">Objective to add</param>
		void Add(IObjective objective);

		/// <summary>
		/// Remove an objective for the level
		/// </summary>
		/// <param name="objective">Objective to remove</param>
		void Remove(IObjective objective);

		/// <summary>
		/// Update objectives ticking each and checking status
		/// </summary>
		/// <param name="delta">Time from last frame</param>
		void Update(float delta);
	}
}
