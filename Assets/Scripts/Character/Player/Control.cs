using System;
using UnityEngine;

namespace Character.Player
{
	public class Control : Movement
	{
		[SerializeField]
		private float speed = 3;
		[SerializeField]
		private float jumpForce = 3;
		[SerializeField] 
		private Vector2 cameraBounds;
		[SerializeField] 
		private Gun gun = null;
		[SerializeField]
		private Camera playerCamera;

		public LayerMask layerMask;
		
		private Vector3 rotationX = Vector3.zero;
		private Vector3 rotationY = Vector3.zero;
		private RaycastHit hit;
		private float cameraHeight;
		private float cameraFreedom = 2f;
		private float factorA;

		private void Start()
		{
			factorA = (cameraBounds.y - cameraBounds.x) / 2;
			cameraHeight = playerCamera.transform.position.y - transform.position.y;
		}

		private void Update()
		{
			HandleMouse();

			HandleKeyboard();

			AimGun();
		}

		private void AimGun()
		{
			if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 1000f, layerMask))
			{
				gun.AimAtPoint(hit.point);
			}
		}

		private void HandleKeyboard()
		{
			Move(Input.GetAxis("Vertical") * speed * transform.forward +
			     Input.GetAxis("Horizontal") * speed * transform.right);

			if (Input.GetKeyDown(KeyCode.Space))
			{
				Jump(jumpForce);
			}
		}

		private void HandleMouse()
		{
			rotationX = transform.rotation.eulerAngles;
			rotationX.y += Input.GetAxis("Mouse X");
			gameObject.transform.rotation = Quaternion.Euler(rotationX);
			
//			rotationY = gun.transform.rotation.eulerAngles;
//			rotationY.x += Input.GetAxis("Mouse Y");
//			rotationY.x = (rotationY.x > 180) ? rotationY.x - 360 : rotationY.x;
//			rotationY.x = Mathf.Clamp(rotationY.x, -90f, 90f);
//			gun.transform.rotation = Quaternion.Euler(rotationY);
			
			rotationY = playerCamera.transform.rotation.eulerAngles;
			rotationY.x += Input.GetAxis("Mouse Y");
			rotationY.x = (rotationY.x > 180) ? rotationY.x - 360 : rotationY.x;
			rotationY.x = Mathf.Clamp(rotationY.x, cameraBounds.x, cameraBounds.y);
			playerCamera.transform.rotation = Quaternion.Euler(rotationY);
			//reuse variable
			rotationX = playerCamera.transform.position;
			//orbiting
			rotationX.y = transform.position.y + cameraHeight + cameraFreedom * ((rotationY.x - (factorA + cameraBounds.x)) / factorA);
			playerCamera.transform.position = rotationX;
		}
	}
}