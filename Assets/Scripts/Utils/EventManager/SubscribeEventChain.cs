namespace Utils.EventManager
{
	public class SubscribeEventChain
	{
		public SubscribeEventChain(Subscriber subscriber, ISubscribeEvent subscribeEvent)
		{
			Subscriber = subscriber;
			Event = subscribeEvent;
		}
		
		public Subscriber Subscriber;
		public ISubscribeEvent Event;
	}
}