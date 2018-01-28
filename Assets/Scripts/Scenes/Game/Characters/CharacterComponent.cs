using UnityEngine;
using UnityEngine.AI;

public class CharacterComponent : MonoBehaviour
{
    public Transform BulletSpawn;
    public GameObject BulletPrefab;
    public LevelComponent Level { get; private set; }
    public bool IsPlayer { get; private set; }
    public int Health = 10;

    public void Setup(LevelComponent level,  bool isPlayer)
    {
        Level = level;
        IsPlayer = isPlayer;
        var agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
    }

    public void Shoot()
    {
        var go = GameObject.Instantiate(BulletPrefab);
        go.transform.position = BulletSpawn.position;
        go.GetComponent<BulletComponent>().Setup(BulletSpawn.position - transform.position);
    }

    public void Hit()
    {
        Health -= 1;
        if (Health == 0)
        {
            Destroy(gameObject);
        }
    }
}
