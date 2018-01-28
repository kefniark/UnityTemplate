using UnityEngine;
using UnityEngine.AI;

public class InputController : MonoBehaviour
{
    public CharacterComponent Character { get; private set; }
    private NavMeshHit hit;
    private RaycastHit raycastHit;

    void Start()
    {
        Character = GetComponent<CharacterComponent>();
    }

    void Update()
    {
        var direction = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));

        var destination = transform.position + direction * Time.deltaTime * 20f;
        var blocked = NavMesh.Raycast(transform.position, destination, out hit, NavMesh.AllAreas);
        GetComponent<NavMeshAgent>().Move(direction * Time.deltaTime * 15f);
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit))
        {
            var dest = new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z);
            if ((transform.position - dest).magnitude > 1.5f)
            {
                transform.LookAt(dest);
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Character.Shoot();
        }
    }
}
