using System;
using System.Collections.Generic;

namespace Utils.EventManager.SubscribeEvents
{
	public partial class SubscribeEventWithTopic
	{
		public static SubscribeEventWithTopic Create(string topic, Action<string> callback) => Create(new[] { topic }, callback);

		public static SubscribeEventWithTopic Create(string[] topics, Action<string> callback) => new SubscribeEventWithTopic(topics, callback);

		public static SubscribeEventWithTopic<T1> Create<T1>(string topic, Action<string, T1> callback) => Create(new[] { topic }, callback);

		public static SubscribeEventWithTopic<T1> Create<T1>(string[] topics, Action<string, T1> callback) => new SubscribeEventWithTopic<T1>(topics, callback);

		public static SubscribeEventWithTopic<T1, T2> Create<T1, T2>(string topic, Action<string, T1, T2> callback) => Create(new[] { topic }, callback);

		public static SubscribeEventWithTopic<T1, T2> Create<T1, T2>(string[] topics, Action<string, T1, T2> callback) => new SubscribeEventWithTopic<T1, T2>(topics, callback);

		public static SubscribeEventWithTopic<T1, T2, T3> Create<T1, T2, T3>(string topic, Action<string, T1, T2, T3> callback) => Create(new[] { topic }, callback);

		public static SubscribeEventWithTopic<T1, T2, T3> Create<T1, T2, T3>(string[] topics, Action<string, T1, T2, T3> callback) => new SubscribeEventWithTopic<T1, T2, T3>(topics, callback);
	}

	public partial class SubscribeEventWithTopic : ISubscribeEvent
	{
		private Action<string> callback;

		public SubscribeEventWithTopic(string[] topics, Action<string> callback)
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
			callback(topic);

			PostHook?.Invoke();
		}

		public override string ToString() => $"SubscribeEvent Topic: {string.Join(",", Topics)}";
	}

	public class SubscribeEventWithTopic<T1> : ISubscribeEvent
	{
		private Action<string, T1> callback;

		public SubscribeEventWithTopic(string[] topics, Action<string, T1> callback)
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
				callback(topic, (T1)args[0]);
			}
			else
			{
				callback(topic, default(T1));
			}

			PostHook?.Invoke();
		}

		public override string ToString() => String.Format("SubscribeEvent<{1}> Topic: {0}", string.Join(",", Topics), typeof(T1));
	}

	public class SubscribeEventWithTopic<T1, T2> : ISubscribeEvent
	{
		private Action<string, T1, T2> callback;

		public SubscribeEventWithTopic(string[] topics, Action<string, T1, T2> callback)
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
				callback(topic, (T1)args[0], (T2)args[1]);
			}
			else if (args.Length >= 1)
			{
				callback(topic, (T1)args[0], default(T2));
			}
			else
			{
				callback(topic, default(T1), default(T2));
			}

			PostHook?.Invoke();
		}

		public override string ToString() => $"SubscribeEvent<{typeof(T1)},{typeof(T2)}> Topic: {string.Join(",", Topics)}";
	}

	public class SubscribeEventWithTopic<T1, T2, T3> : ISubscribeEvent
	{
		private Action<string, T1, T2, T3> callback;

		public SubscribeEventWithTopic(string[] topics, Action<string, T1, T2, T3> callback)
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
			if (args.Length >= 3)
			{
				callback(topic, (T1)args[0], (T2)args[1], (T3)args[2]);
			}
			else if (args.Length >= 2)
			{
				callback(topic, (T1)args[0], (T2)args[1], default(T3));
			}
			else if (args.Length >= 1)
			{
				callback(topic, (T1)args[0], default(T2), default(T3));
			}
			else
			{
				callback(topic, default(T1), default(T2), default(T3));
			}

			PostHook?.Invoke();
		}

		public override string ToString() => $"SubscribeEvent<{typeof(T1)},{typeof(T2)},{typeof(T3)}> Topic: {string.Join(",", Topics)}";
	}
}
