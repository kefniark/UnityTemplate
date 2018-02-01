using System;

namespace Scenes.Game
{
	/// <summary>
	/// An objective in the level
	/// </summary>
	public interface IObjective
	{
		/// <summary>
		/// Objective completed
		/// </summary>
		event EventHandler Completed;

		/// <summary>
		/// Objective failed
		/// </summary>
		event EventHandler Failed;

		/// <summary>
		/// Objective status changed
		/// </summary>
		event EventHandler EnableChanged;

		/// <summary>
		/// If objective is complete
		/// </summary>
		bool Complete { get; }

		/// <summary>
		/// If objective has been failed
		/// </summary>
		bool Fail { get; }

		/// <summary>
		/// If objective is Enabled
		/// </summary>
		bool Enabled { get; set; }

		/// <summary>
		/// Update to tick the objective
		/// This allows for timers inside the objectives
		/// </summary>
		/// <param name="delta">Time from last frame</param>
		void Update(float delta);
	}
}
