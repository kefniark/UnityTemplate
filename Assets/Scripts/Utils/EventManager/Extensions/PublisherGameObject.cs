using UnityEngine;

using Utils.EventManager;

[DisallowMultipleComponent]
public class PublisherGameObject : MonoBehaviour
{
	public void Publish(string topic) => Publisher.Publish(topic);
}
