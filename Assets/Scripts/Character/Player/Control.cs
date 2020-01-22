using System;
using UnityEngine;

namespace Character.Player
{
	public class Control : Movement
	{
		[SerializeField] 
		private Vector2 cameraBounds;
		[SerializeField] 
		private Gun.Gun gun = null;
		[SerializeField]
		private Camera playerCamera;

		public LayerMask layerMask;
		public float MouseSensivityX = 5;
		public float MouseSensivityY = 5;
		
		private Vector3 rotationX = Vector3.zero;
		private Vector3 rotationY = Vector3.zero;
		private Vector3 direction;
		private RaycastHit hit;
		private float cameraHeight;
		private float cameraFreedom = 2f;
		private float factorA;
		private int jumpCount;
		private bool onGround;
		private bool haveDoubleJump;
		private bool haveSprint;
		private float sprintPower = 2.5f;

		private void Start()
		{
			factorA = (cameraBounds.y - cameraBounds.x) / 2;
			cameraHeight = playerCamera.transform.position.y - transform.position.y;
		}

		private void Update()
		{
			HandleMouse();

			HandleMouseClick();

			HandleKeyboard();

			AimGun();
		}

		private void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Ground") ||
			    other.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
			{
				onGround = true;
				ResetJump();
			}
		}

		private void ResetJump()
		{
			jumpCount = haveDoubleJump ? 2 : 1;
		}

		float time = 0;
		private float reloadTime = 0.5f;
		private void HandleMouseClick()
		{
			time -= Time.deltaTime;
			if (Input.GetMouseButton(0))
			{
				//gun.Fire();
				if (time < 0)
				{
					gun.Fire();
					time = reloadTime;
				}
			}
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
			direction = Input.GetAxis("Vertical") * transform.forward +
			            Input.GetAxis("Horizontal") * transform.right;
			if (haveSprint && Input.GetKey(KeyCode.LeftShift))
			{
				direction *= sprintPower;
			}
			Move(direction);

			if (Input.GetKeyDown(KeyCode.Space) && jumpCount-- > 0)
			{
				Jump();
			}
		}

		private void HandleMouse()
		{
			rotationX = transform.rotation.eulerAngles;
			rotationX.y += Input.GetAxis("Mouse X") * Time.deltaTime * MouseSensivityX;
			gameObject.transform.rotation = Quaternion.Euler(rotationX);
			
//			rotationY = gun.transform.rotation.eulerAngles;
//			rotationY.x += Input.GetAxis("Mouse Y");
//			rotationY.x = (rotationY.x > 180) ? rotationY.x - 360 : rotationY.x;
//			rotationY.x = Mathf.Clamp(rotationY.x, -90f, 90f);
//			gun.transform.rotation = Quaternion.Euler(rotationY);
			
			rotationY = playerCamera.transform.rotation.eulerAngles;
			rotationY.x -= Input.GetAxis("Mouse Y") * Time.deltaTime * MouseSensivityY;
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