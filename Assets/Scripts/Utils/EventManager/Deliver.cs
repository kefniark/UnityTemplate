using System;
using System.Collections.Generic;

namespace Utils.EventManager
{
	public class Deliver
	{
		public static Deliver Instance => instance ?? (instance = new Deliver());

		private static Deliver instance;

		private readonly List<Subscriber> subscribers = new List<Subscriber>();
		private List<Action> bookedEvents;

		public int Publish(string topic, object[] args)
		{
			int listener = 0;
			bool rootEvent = false;
			if (bookedEvents == null)
			{
				rootEvent = true;
				bookedEvents = new List<Action>();
			}

			foreach (Subscriber subscriber in subscribers)
			{
				listener += subscriber.Call(topic, args);
			}

			if (rootEvent)
			{
				Action[] copied = bookedEvents.ToArray();
				bookedEvents = null;
				foreach (Action e in copied)
				{
					e();
				}
			}

			return listener;
		}

		public void Subscribe(Subscriber subscriber)
		{
			if (bookedEvents != null)
			{
				bookedEvents.Add(
					() =>
					{
						Subscribe(subscriber);
					});
			}
			else
			{
				subscribers.Add(subscriber);
			}
		}

		public void Unsubscribe(Subscriber subscriber)
		{
			if (bookedEvents != null)
			{
				bookedEvents.Add(
					() =>
					{
						Unsubscribe(subscriber);
					});
			}
			else
			{
				subscribers.Remove(subscriber);
			}
		}
	}
}
