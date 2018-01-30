using UnityEngine;

using Utils.EventManager;

[DisallowMultipleComponent]
public class SubscriberGameObject : MonoBehaviour
{
	public Subscriber Subscriber { get; }
		
	public SubscriberGameObject()
	{
		Subscriber = new Subscriber();
	}
		
	public void OnDestroy() => Subscriber.Dispose();
}
