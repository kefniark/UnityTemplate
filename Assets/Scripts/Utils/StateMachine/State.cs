using System;

namespace Utils.StateMachine
{
	/// <summary>
	/// Generic State Class
	/// Can be inherited to create a custom state
	/// </summary>
	/// <typeparam name="K"></typeparam>
	/// <typeparam name="T"></typeparam>
	public class State<K, T> where K : IConvertible
	{
		// properties
		public K Id { get; private set; }

		public bool IsActive { get; private set; }
		public StateMachine<K, T> StateMachine { get; private set; }

		public T Entity { get; private set; }

		// transition
		public bool SkipEnterTransition { get; protected set; } = false;
		public bool SkipExitTransition { get; protected set; } = false;

		// events
		public event EventHandler StateEnter;
		public event EventHandler StateEntered;
		public event EventHandler StateExit;
		public event EventHandler StateExited;

		/// <summary>
		/// Used by the stateMachine to setup this state
		/// </summary>
		/// <param name="id"></param>
		/// <param name="statemachine"></param>
		/// <param name="entity"></param>
		public void Init(K id, StateMachine<K, T> statemachine, T entity)
		{
			Id = id;
			StateMachine = statemachine;
			Entity = entity;
		}

		/// <summary>
		/// Enter state
		/// </summary>
		public virtual void Enter()
		{
			IsActive = true;
			StateEnter?.Invoke(this, EventArgs.Empty);
			if (!SkipEnterTransition)
			{
				EnterFinish();
			}
		}

		/// <summary>
		/// Enter state
		/// </summary>
		public void EnterFinish()
		{
			StateEntered?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Exit state
		/// </summary>
		public void Exit()
		{
			IsActive = false;
			StateExit?.Invoke(this, EventArgs.Empty);
			if (!SkipExitTransition)
			{
				ExitFinish();
			}
		}

		/// <summary>
		/// Exit state
		/// </summary>
		public void ExitFinish()
		{
			StateExited?.Invoke(this, EventArgs.Empty);
		}

		public override string ToString() => StateMachine == null ? "[State]" : $"[State of {StateMachine.Parent} : {Id} - {IsActive}]";
	}
}
