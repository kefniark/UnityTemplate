using UnityEngine;

namespace Utils.EventManager.Extensions
{
	[DisallowMultipleComponent]
	public class SubscriberGameObject : MonoBehaviour
	{
		public Subscriber Subscriber { get; private set; }
		
		public SubscriberGameObject()
		{
			Subscriber = new Subscriber();
		}
		
		public void OnDestroy()
		{
			Subscriber.Dispose();
		}
	}
}