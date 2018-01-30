using System;

namespace Utils.EventManager.SubscribeEvents
{
	public interface ISubscribeEvent
	{
		bool Muting { get; set; }
		string[] Topics { get; }
		Action PostHook { get; set; }
		void Call(string topic, object[] args);
		string ToString();
		bool RemoveTopic(string topic);
	}
}
