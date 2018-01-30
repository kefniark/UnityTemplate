namespace Utils.EventManager.SubscribeEvents
{
	public static class SubscribeEventOnce
	{
		public static SubscribeEventChain Once(this SubscribeEventChain sec)
		{
			sec.Event.PostHook += () =>
			{
				sec.Subscriber.Unsubscribe(sec);
			};
			return sec;
		}
	}
}
