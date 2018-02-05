using Scenes.Game.Components.Characters;

using UnityEngine;
using UnityEngine.AI;

namespace Scenes.Game.Components.InputControllers {
	public class InputController : MonoBehaviour
	{
		public CharacterComponent Character { get; private set; }
		private RaycastHit raycastHit;

		private void Start()
		{
			Character = GetComponent<CharacterComponent>();
		}

		private void Update()
		{
			if (Time.timeScale <= 0)
			{
				return;
			}
			var direction = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
			Quaternion vector = Quaternion.AngleAxis(-45, Vector3.up);

			// move
			Vector3 destination = direction * Time.deltaTime * 15f;
			//transform.Translate(vector * destination, Space.World);
			GetComponent<NavMeshAgent>().Move(vector * destination);

			// rotation
			Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out raycastHit))
			{
				var dest = new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z);
				if ((transform.position - dest).magnitude > 1.5f)
				{
					transform.LookAt(dest);
				}
			}

			// shoot
			if (Input.GetButtonDown("Fire1"))
			{
				Character.Shoot();
			}
		}
	}
}
