using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    public Vector3 Direction;
    public float Speed = 10f;

    public void Setup(Vector3 direction)
    {
        Direction = direction;
        Destroy(gameObject, 3);
    }
    
    void Update()
    {
        transform.Translate(Direction * Time.deltaTime * Speed);
    }

    void OnTriggerEnter(Collider other)
    {
        var character = other.gameObject.GetComponent<CharacterComponent>();
        if (character != null)
        {
            character.Hit();
        }
        Destroy(gameObject);
    }
}
