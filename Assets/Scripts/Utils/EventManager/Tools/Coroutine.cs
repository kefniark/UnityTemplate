using System.Collections;
using UnityEngine;

namespace Utils.EventManager.Tools
{
    public static class Coroutine {

        public static IEnumerator WaitForMessage(this Subscriber subscriber, string[] topics, float timeout = 0.0f)
        {
            var called = false;
            var startTime = Time.time;
            var se = subscriber.Subscribe(topics, () => {
                called = true;
            });

            while (!called && (timeout <= 0.0f || Time.time < startTime + timeout))
            {
                yield return null;
            }

            subscriber.Unsubscribe(se);
        }
    }
}