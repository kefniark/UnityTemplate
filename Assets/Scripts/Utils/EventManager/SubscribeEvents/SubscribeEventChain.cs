namespace Utils.EventManager.SubscribeEvents
{
	public class SubscribeEventChain
	{
		public ISubscribeEvent Event;

		public Subscriber Subscriber;

		public SubscribeEventChain(Subscriber subscriber, ISubscribeEvent subscribeEvent)
		{
			Subscriber = subscriber;
			Event = subscribeEvent;
		}
	}
}
