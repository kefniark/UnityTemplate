using System;
using System.Collections.Generic;

namespace Utils.EventManager.SubscribeEvents
{
	public partial class SubscribeEvent
	{
		public static SubscribeEvent Create(string topic, Action callback) => Create(new[] {topic}, callback);

		public static SubscribeEvent Create(string[] topics, Action callback) => new SubscribeEvent(topics, callback);

		public static SubscribeEvent<T1> Create<T1>(string topic, Action<T1> callback) => Create(new[] {topic}, callback);

		public static SubscribeEvent<T1> Create<T1>(string[] topics, Action<T1> callback) => new SubscribeEvent<T1>(topics, callback);

		public static SubscribeEvent<T1, T2> Create<T1, T2>(string topic, Action<T1, T2> callback) => Create(new[] {topic}, callback);

		public static SubscribeEvent<T1, T2> Create<T1, T2>(string[] topics, Action<T1, T2> callback) => new SubscribeEvent<T1, T2>(topics, callback);

		public static SubscribeEvent<T1, T2, T3> Create<T1, T2, T3>(string topic, Action<T1, T2, T3> callback) => Create(new[] {topic}, callback);

		public static SubscribeEvent<T1, T2, T3> Create<T1, T2, T3>(string[] topics, Action<T1, T2, T3> callback) => new SubscribeEvent<T1, T2, T3>(topics, callback);
	}

	public partial class SubscribeEvent : ISubscribeEvent
	{
		private Action callback;

		public SubscribeEvent(string[] topics, Action callback)
		{
			Topics = topics;
			this.callback = callback;
		}

		public string[] Topics { get; private set; }
		public bool Muting { get; set; }
		public Action PostHook { get; set; }

		public bool RemoveTopic(string topic)
		{
			var i = Array.IndexOf(Topics, topic);
			if (i == -1)
			{
				return false;
			}

			var tmp = new List<string>(Topics);
			tmp.RemoveAt(i);
			Topics = tmp.ToArray();
			return Topics.Length == 0;
		}

		public void Call(string topic, object[] args)
		{
			callback();
			PostHook?.Invoke();
		}

		public override string ToString() => $"SubscribeEvent Topic: {string.Join(",", Topics)}";
	}

	public class SubscribeEvent<T1> : ISubscribeEvent
	{
		private Action<T1> callback;

		public SubscribeEvent(string[] topics, Action<T1> callback)
		{
			Topics = topics;
			this.callback = callback;
		}

		public string[] Topics { get; private set; }
		public bool Muting { get; set; }
		public Action PostHook { get; set; }

		public bool RemoveTopic(string topic)
		{
			int i = Array.IndexOf(Topics, topic);
			if (i == -1)
			{
				return false;
			}

			var tmp = new List<string>(Topics);
			tmp.RemoveAt(i);
			Topics = tmp.ToArray();
			return Topics.Length == 0;
		}

		public void Call(string topic, object[] args)
		{
			if (args.Length >= 1)
			{
				callback((T1)args[0]);
			}
			else
			{
				callback(default(T1));
			}

			PostHook?.Invoke();
		}

		public override string ToString() => String.Format("SubscribeEvent<{1}> Topic: {0}", string.Join(",", Topics), typeof(T1));
	}

	public class SubscribeEvent<T1, T2> : ISubscribeEvent
	{
		private Action<T1, T2> callback;

		public SubscribeEvent(string[] topics, Action<T1, T2> callback)
		{
			Topics = topics;
			this.callback = callback;
		}

		public string[] Topics { get; private set; }
		public bool Muting { get; set; }
		public Action PostHook { get; set; }

		public bool RemoveTopic(string topic)
		{
			int i = Array.IndexOf(Topics, topic);
			if (i == -1)
			{
				return false;
			}

			var tmp = new List<string>(Topics);
			tmp.RemoveAt(i);
			Topics = tmp.ToArray();
			return Topics.Length == 0;
		}

		public void Call(string topic, object[] args)
		{
			if (args.Length >= 2)
			{
				callback((T1)args[0], (T2)args[1]);
			}
			else if (args.Length >= 1)
			{
				callback((T1)args[0], default(T2));
			}
			else
			{
				callback(default(T1), default(T2));
			}

			PostHook?.Invoke();
		}

		public override string ToString() => $"SubscribeEvent<{typeof(T1)},{typeof(T2)}> Topic: {string.Join(",", Topics)}";
	}

	public class SubscribeEvent<T1, T2, T3> : ISubscribeEvent
	{
		private Action<T1, T2, T3> callback;

		public SubscribeEvent(string[] topics, Action<T1, T2, T3> callback)
		{
			Topics = topics;
			this.callback = callback;
		}

		public string[] Topics { get; private set; }
		public bool Muting { get; set; }
		public Action PostHook { get; set; }

		public bool RemoveTopic(string topic)
		{
			int i = Array.IndexOf(Topics, topic);
			if (i == -1)
			{
				return false;
			}

			var tmp = new List<string>(Topics);
			tmp.RemoveAt(i);
			Topics = tmp.ToArray();
			return Topics.Length == 0;
		}

		public void Call(string topic, object[] args)
		{
			if (args.Length >= 3)
			{
				callback((T1)args[0], (T2)args[1], (T3)args[2]);
			}
			else if (args.Length >= 2)
			{
				callback((T1)args[0], (T2)args[1], default(T3));
			}
			else if (args.Length >= 1)
			{
				callback((T1)args[0], default(T2), default(T3));
			}
			else
			{
				callback(default(T1), default(T2), default(T3));
			}

			PostHook?.Invoke();
		}

		public override string ToString() => $"SubscribeEvent<{typeof(T1)},{typeof(T2)},{typeof(T3)}> Topic: {string.Join(",", Topics)}";
	}
}
