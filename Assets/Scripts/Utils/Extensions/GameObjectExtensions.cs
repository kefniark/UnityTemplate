
using System;
using System.Collections;

using UnityEngine;

public static class GameObjectExtensions
{
	/// <summary>
	/// Use a coroutine to delay an action
	/// </summary>
	/// <param name="behaviour"></param>
	/// <param name="duration"></param>
	/// <param name="callback"></param>
	public static void DelayAction(this MonoBehaviour behaviour, float duration, Action callback)
	{
		behaviour.StartCoroutine(Delay(duration, callback));
	}

	private static IEnumerator Delay(float duration, Action callback)
	{
		yield return new WaitForSeconds(duration);
		callback.Invoke();
	}
}
