using UnityEngine;
using Utils.EventManager.Extensions;

public class CameraFollow : MonoBehaviour
{
    private CharacterComponent target;
    private Vector3 OriginalPosition;

    void Start()
    {
        OriginalPosition = transform.position;
        this.Subscribe<CharacterComponent>(EventTopics.GameCharacterSpawn, (character) =>
        {
            if (!character.IsPlayer) return;
            target = character;
        });
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        transform.position = target.transform.position + OriginalPosition;
    }
}
