using System;
using UnityEngine;

namespace Character
{
	[RequireComponent(typeof(Rigidbody))]
	public class Movement : MonoBehaviour
	{
		[SerializeField]
		private float speed = 3;
		[SerializeField]
		private float jumpForce = 3;
		
		private Rigidbody rg;
		private Vector3 jumpVelocity = Vector3.zero;
	
		private void Awake()
		{
			rg = GetComponent<Rigidbody>();
		}

		protected void Move(Vector3 direction)
		{
			jumpVelocity.y = rg.velocity.y;
			rg.velocity = direction * speed + jumpVelocity;
		}

		protected void Jump()
		{
			Jump(jumpForce);
		}

		private void Jump(float force)
		{
			rg.AddForce(0, force, 0, ForceMode.VelocityChange);
		}

		protected void Ragdoll()
		{
			rg.constraints = RigidbodyConstraints.None;
		}
		
		protected void Ragdoll(Vector3 direction)
		{
			rg.AddForce(direction);
			Ragdoll();
		}
	}
}