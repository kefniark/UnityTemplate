using System;

using Config;

using Utils.EventManager;

namespace Scenes.Game.Components.Objectives
{
	/// <inheritdoc />
	public abstract class ObjectiveBase : IObjective
	{
		/// <summary>
		/// If the objective is enabled
		/// </summary>
		private bool enable = true;

		/// <inheritdoc />
		public event EventHandler Completed;

		/// <inheritdoc />
		public event EventHandler Failed;

		/// <summary>
		/// When the state of the objective changes
		/// </summary>
		public event EventHandler EnableChanged;

		/// <inheritdoc />
		public bool Complete { get; private set; }

		/// <inheritdoc />
		public bool Fail { get; private set; }

		/// <inheritdoc />
		public bool Enabled
		{
			get { return enable; }
			set
			{
				enable = value;
				EnableChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <inheritdoc />
		public virtual void Update(float delta) { }

		/// <summary>
		/// Method to mark the objective as completed
		/// </summary>
		protected void ObjectiveComplete()
		{
			if (Complete)
			{
				return;
			}

			Complete = true;
			Completed?.Invoke(this, EventArgs.Empty);
			Publisher.Publish(EventTopics.GameObjectiveComplete, this);
		}

		/// <summary>
		/// Method to mark the objective as failed
		/// </summary>
		protected void ObjectiveFail()
		{
			if (Fail)
			{
				return;
			}

			Fail = true;
			Failed?.Invoke(this, EventArgs.Empty);
			Publisher.Publish(EventTopics.GameObjectiveFail, this);
		}

		/// <summary>
		/// 
		/// </summary>
		protected void ObjectiveUpdate()
		{
			if (Complete || Fail)
			{
				return;
			}
			Publisher.Publish(EventTopics.GameObjectiveUpdated, this);
		}

		/// <summary>
		/// Method used to format the output description of an objective
		/// </summary>
		/// <param name="description"></param>
		/// <returns></returns>
		protected virtual string FormatIngameDescription(string description) => string.IsNullOrEmpty(description) ? "" : description;

		/// <inheritdoc />
		public override string ToString() => $"[{nameof(ObjectiveBase)} Type:{GetType().Name} {nameof(Enabled)}:{Enabled} {nameof(Complete)}:{Complete} {nameof(Fail)}:{Fail}]";
	}
}
