using UnityEngine;

namespace Utils.EventManager.Extensions
{
	[DisallowMultipleComponent]
	public class PublisherGameObject : MonoBehaviour
	{
		public void Publish(string topic)
		{
			Publisher.Publish(topic);
		}
	}
}