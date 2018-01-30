using UnityEngine;

namespace Utils.EventManager.Debug
{
	public class EventDebugger : MonoBehaviour
	{
		public string Arg1;
		public bool SendTopic;
		public string Topic;

		public void Start()
		{
			this.SubscribeWithTopic(
				"*",
				(string topic, object arg1, object arg2, object arg3) =>
				{
					if (arg1 == null)
					{
						UnityEngine.Debug.LogFormat("[EventDebugger] Topic:{0}", topic);
					}
					else if (arg2 == null)
					{
						UnityEngine.Debug.LogFormat("[EventDebugger] Topic:{0} | Arg1:{1}", topic, arg1);
					}
					else if (arg3 == null)
					{
						UnityEngine.Debug.LogFormat("[EventDebugger] Topic:{0} | Arg1:{1} Arg2:{2}", topic, arg1, arg2);
					}
					else
					{
						UnityEngine.Debug.LogFormat("[EventDebugger] Topic:{0} | Arg1:{1} Arg2:{2} Arg3:{3}", topic, arg1, arg2, arg3);
					}
				});
		}

		public void Update()
		{
			if (!SendTopic)
			{
				return;
			}

			if (string.IsNullOrEmpty(Arg1))
			{
				Publisher.Publish(Topic);
			}
			else
			{
				Publisher.Publish(Topic, Arg1);
			}

			SendTopic = false;
			Topic = "";
			Arg1 = "";
		}

		public void OnEnable() => this.Unmute("*");

		public void OnDisable() => this.Mute("*");
	}
}
