using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.StateMachine
{
	/// <summary>
	/// Generic StateMachine class - Manage a set of states
	/// Can use almost any type as index (int, string, enum, struct)
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="K"></typeparam>
	public class StateMachine<K, T> where K : IConvertible
	{
		// properties
		public Dictionary<K, State<K, T>> States { get; } = new Dictionary<K, State<K, T>>();

		// accessors
		public State<K, T> Current { get; private set; }

		public State<K, T> Previous { get; private set; }
		public readonly T Parent;

		private State<K, T> next;

		public StateMachine(T parent)
		{
			Parent = parent;
		}

		// event
		public event EventHandler<StateChangeEventArgs<K, T>> StateChanged;

		/// <summary>
		///  Add a new state to the statemachine
		/// </summary>
		/// <param name="id"></param>
		/// <param name="state"></param>
		public void Add(K id, State<K, T> state)
		{
			States.Add(id, state);
			state.Init(id, this, Parent);
		}

		/// <summary>
		///  Remove a state from the statemachine
		/// </summary>
		/// <param name="id"></param>
		public void Remove(K id) => States.Remove(id);

		/// <summary>
		/// Change the current state of the statemachine
		/// </summary>
		/// <param name="id"></param>
		public void ChangeState(K id)
		{
			if (Current == null)
			{
				throw new Exception($"{this} This stateMachine was not started");
			}

			if (!States.ContainsKey(id))
			{
				throw new Exception($"{this} This state doesn\'t exist {id}");
			}

			ChangeState(States[id]);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="state"></param>
		private void ChangeState(State<K, T> state)
		{
			if (state == Current)
			{
				UnityEngine.Debug.LogWarning($"{this} Can\'t change to the same state");
				return;
			}

			// exit previous state
			if (Current != null)
			{
				next = state;
				Current.StateExited += Current_StateExited;
				Current.Exit();
				return;
			}

			// switch to new state
			Current = state;

			// event
			StateChanged?.Invoke(this, new StateChangeEventArgs<K, T>(Current, Previous));

			Current.Enter();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Current_StateExited(object sender, EventArgs e)
		{
			Current.StateExited -= Current_StateExited;
			Previous = Current;
			Current = next;
			StateChanged?.Invoke(this, new StateChangeEventArgs<K, T>(Current, Previous));
			Current.Enter();
		}

		/// <summary>
		/// Start the statemachine (enter the default state)
		/// </summary>
		public void Start()
		{
			if (Current != null)
			{
				UnityEngine.Debug.LogWarning($"{this} Can\'t start, seems already started");
				return;
			}

			KeyValuePair<K, State<K, T>> state = States.FirstOrDefault();
			if (state.Value == null)
			{
				throw new Exception($"{this} No default state, cannot start");
			}

			// TODO: Check if these need to be switched over (CC: Almir bug)?
			state.Value.Enter();
			Current = state.Value;
		}

		/// <summary>
		/// Go back to the previous state
		/// </summary>
		public void Back()
		{
			if (Previous == null)
			{
				return;
			}
			ChangeState(Previous);
			Previous = null;
		}

		/// <summary>
		/// Use to dispose the stop the statemachine
		/// </summary>
		public void Exit()
		{
			if (Current == null)
			{
				UnityEngine.Debug.LogWarning($"{this} Can\'t exit, seems already exited");
				return;
			}
			Current.Exit();
			Current = null;
			Previous = null;
		}

		public override string ToString() => $"[StateMachine of {Parent}]";
	}
}
