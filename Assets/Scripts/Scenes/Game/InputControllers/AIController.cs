using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public CharacterComponent Character { get; private set; }
    public NavMeshAgent NavAgent;
    public CharacterComponent Target;
    private NavMeshHit hit;
    public float lastmove;
    public float lastshoot;

    // Use this for initialization
    void Start()
    {
        Character = GetComponent<CharacterComponent>();
        NavAgent = GetComponentInChildren<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            SearchForTarget();
            return;
        }

        // move
        if (Time.time - lastmove > 0.2f)
        {
            lastmove = Time.time;
            NavAgent.destination = Target.transform.position;
        }

        // rotate
        transform.LookAt(Target.transform.position);

        // shoot
        if (Time.time - lastshoot > 0.15f)
        {
            lastshoot = Time.time;
            var blocked = NavMesh.Raycast(transform.position, Target.transform.position, out hit, NavMesh.AllAreas);
            if (!blocked)
            {
                Character.Shoot();
            }
        }
    }

    private void SearchForTarget()
    {
        Target = Character.Level.Characters.Where(x => x != Character).OrderBy(x => Random.value).First();
    }
}
