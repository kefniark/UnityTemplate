using System;
using System.Collections;

using UnityEngine;

using Utils.EventManager;
using Utils.EventManager.SubscribeEvents;

public static class GameObjectExtension
{
	public static Subscriber GetSubscriber(this MonoBehaviour behaviour) => GetOrAddComponent<SubscriberGameObject>(behaviour.gameObject).Subscriber;

	public static SubscribeEventChain Subscribe(this MonoBehaviour behaviour, string topic, Action callback) => GetSubscriber(behaviour).Subscribe(topic, callback);

	public static SubscribeEventChain SubscribeWithTopic(this MonoBehaviour behaviour, string topic, Action<string> callback) => GetSubscriber(behaviour).SubscribeWithTopic(topic, callback);

	public static SubscribeEventChain Subscribe<T1>(this MonoBehaviour behaviour, string topic, Action<T1> callback) => GetSubscriber(behaviour).Subscribe(topic, callback);

	public static SubscribeEventChain SubscribeWithTopic<T1>(this MonoBehaviour behaviour, string topic, Action<string, T1> callback) => GetSubscriber(behaviour).SubscribeWithTopic(topic, callback);

	public static SubscribeEventChain Subscribe<T1, T2>(this MonoBehaviour behaviour, string topic, Action<T1, T2> callback) => GetSubscriber(behaviour).Subscribe(topic, callback);

	public static SubscribeEventChain SubscribeWithTopic<T1, T2>(this MonoBehaviour behaviour, string topic, Action<string, T1, T2> callback) => GetSubscriber(behaviour).SubscribeWithTopic(topic, callback);

	public static SubscribeEventChain Subscribe<T1, T2, T3>(this MonoBehaviour behaviour, string topic, Action<T1, T2, T3> callback) => GetSubscriber(behaviour).Subscribe(topic, callback);

	public static SubscribeEventChain SubscribeWithTopic<T1, T2, T3>(this MonoBehaviour behaviour, string topic, Action<string, T1, T2, T3> callback) => GetSubscriber(behaviour).SubscribeWithTopic(topic, callback);

	public static SubscribeEventChain Subscribe(this MonoBehaviour behaviour, string[] topics, Action callback) => GetSubscriber(behaviour).Subscribe(topics, callback);

	public static SubscribeEventChain SubscribeWithTopic(this MonoBehaviour behaviour, string[] topics, Action<string> callback) => GetSubscriber(behaviour).SubscribeWithTopic(topics, callback);

	public static SubscribeEventChain Subscribe<T1>(this MonoBehaviour behaviour, string[] topics, Action<T1> callback) => GetSubscriber(behaviour).Subscribe(topics, callback);

	public static SubscribeEventChain SubscribeWithTopic<T1>(this MonoBehaviour behaviour, string[] topics, Action<string, T1> callback) => GetSubscriber(behaviour).SubscribeWithTopic(topics, callback);

	public static SubscribeEventChain Subscribe<T1, T2>(this MonoBehaviour behaviour, string[] topics, Action<T1, T2> callback) => GetSubscriber(behaviour).Subscribe(topics, callback);

	public static SubscribeEventChain SubscribeWithTopic<T1, T2>(this MonoBehaviour behaviour, string[] topics, Action<string, T1, T2> callback) =>
		GetSubscriber(behaviour).SubscribeWithTopic(topics, callback);

	public static SubscribeEventChain Subscribe<T1, T2, T3>(this MonoBehaviour behaviour, string[] topics, Action<T1, T2, T3> callback) => GetSubscriber(behaviour).Subscribe(topics, callback);

	public static SubscribeEventChain SubscribeWithTopic<T1, T2, T3>(this MonoBehaviour behaviour, string[] topics, Action<string, T1, T2, T3> callback) =>
		GetSubscriber(behaviour).SubscribeWithTopic(topics, callback);

	public static SubscribeEventChain SubscribeAndStartCoroutine(this MonoBehaviour behaviour, string topic, Func<IEnumerator> callback)
	{
		return GetSubscriber(behaviour).Subscribe(
			topic,
			() =>
			{
				behaviour.StartCoroutine(callback());
			});
	}

	public static SubscribeEventChain SubscribeWithTopicAndStartCoroutine(this MonoBehaviour behaviour, string topic, Func<string, IEnumerator> callback)
	{
		return GetSubscriber(behaviour).SubscribeWithTopic(
			topic,
			(t) =>
			{
				behaviour.StartCoroutine(callback(t));
			});
	}

	public static SubscribeEventChain SubscribeAndStartCoroutine<T1>(this MonoBehaviour behaviour, string topic, Func<T1, IEnumerator> callback)
	{
		return GetSubscriber(behaviour).Subscribe<T1>(
			topic,
			(a1) =>
			{
				behaviour.StartCoroutine(callback(a1));
			});
	}

	public static SubscribeEventChain SubscribeWithTopicAndStartCoroutine<T1>(this MonoBehaviour behaviour, string topic, Func<string, T1, IEnumerator> callback)
	{
		return GetSubscriber(behaviour).SubscribeWithTopic<T1>(
			topic,
			(t, a1) =>
			{
				behaviour.StartCoroutine(callback(t, a1));
			});
	}

	public static SubscribeEventChain SubscribeAndStartCoroutine<T1, T2>(this MonoBehaviour behaviour, string topic, Func<T1, T2, IEnumerator> callback)
	{
		return GetSubscriber(behaviour).Subscribe<T1, T2>(
			topic,
			(a1, a2) =>
			{
				behaviour.StartCoroutine(callback(a1, a2));
			});
	}

	public static SubscribeEventChain SubscribeWithTopicAndStartCoroutine<T1, T2>(this MonoBehaviour behaviour, string topic, Func<string, T1, T2, IEnumerator> callback)
	{
		return GetSubscriber(behaviour).SubscribeWithTopic<T1, T2>(
			topic,
			(t, a1, a2) =>
			{
				behaviour.StartCoroutine(callback(t, a1, a2));
			});
	}

	public static SubscribeEventChain SubscribeAndStartCoroutine<T1, T2, T3>(this MonoBehaviour behaviour, string topic, Func<T1, T2, T3, IEnumerator> callback)
	{
		return GetSubscriber(behaviour).Subscribe<T1, T2, T3>(
			topic,
			(a1, a2, a3) =>
			{
				behaviour.StartCoroutine(callback(a1, a2, a3));
			});
	}

	public static SubscribeEventChain SubscribeWithTopicAndStartCoroutine<T1, T2, T3>(this MonoBehaviour behaviour, string topic, Func<string, T1, T2, T3, IEnumerator> callback)
	{
		return GetSubscriber(behaviour).SubscribeWithTopic<T1, T2, T3>(
			topic,
			(t, a1, a2, a3) =>
			{
				behaviour.StartCoroutine(callback(t, a1, a2, a3));
			});
	}

	public static void Unsubscribe(this MonoBehaviour behaviour) => GetSubscriber(behaviour).Unsubscribe();

	public static void Unsubscribe(this MonoBehaviour behaviour, string topic) => GetSubscriber(behaviour).Unsubscribe(topic);

	public static void Unsubscribe(this MonoBehaviour behaviour, ISubscribeEvent se) => GetSubscriber(behaviour).Unsubscribe(se);

	public static void Unsubscribe(this MonoBehaviour behaviour, SubscribeEventChain sec) => GetSubscriber(behaviour).Unsubscribe(sec);

	public static int Publish(this MonoBehaviour behaviour, string topic) => Publisher.Publish(topic);

	public static int Publish<T1>(this MonoBehaviour behaviour, string topic, T1 arg1) => Publisher.Publish(topic, arg1);

	public static int Publish<T1, T2>(this MonoBehaviour behaviour, string topic, T1 arg1, T2 arg2) => Publisher.Publish(topic, arg1, arg2);

	public static int Publish<T1, T2, T3>(this MonoBehaviour behaviour, string topic, T1 arg1, T2 arg2, T3 arg3) => Publisher.Publish(topic, arg1, arg2, arg3);

	public static void Mute(this MonoBehaviour behaviour, string topic) => GetSubscriber(behaviour).Mute(topic);

	public static void Unmute(this MonoBehaviour behaviour, string topic) => GetSubscriber(behaviour).Unmute(topic);

	public static Coroutine WaitForMessage(this MonoBehaviour behaviour, string topic, float timeout = 0.0f) => behaviour.WaitForMessage(new string[] {topic}, timeout);

	public static Coroutine WaitForMessage(this MonoBehaviour behaviour, string[] topics, float timeout = 0.0f)
	{
		var subscriberObject = GetOrAddComponent<SubscriberGameObject>(behaviour.gameObject);
		return behaviour.StartCoroutine(Utils.EventManager.Tools.Coroutine.WaitForMessage(subscriberObject.Subscriber, topics, timeout));
	}

	private static T GetOrAddComponent<T>(GameObject gameObject)
		where T : Component
	{
		var component = gameObject.GetComponent<T>();
		if (component == null)
		{
			component = gameObject.AddComponent<T>();
		}

		return component;
	}
}
