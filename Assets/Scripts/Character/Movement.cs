using System;
using UnityEngine;

namespace Character
{
	[RequireComponent(typeof(Rigidbody))]
	public class Movement : MonoBehaviour
	{
		private Rigidbody rg;
		private Vector3 jumpVelocity = Vector3.zero;
	
		private void Awake()
		{
			rg = GetComponent<Rigidbody>();
		}

		protected void Move(Vector3 direction)
		{
			jumpVelocity.y = rg.velocity.y;
			rg.velocity = direction + jumpVelocity;
		}

		protected void Jump(float force)
		{
			rg.AddForce(0, force, 0, ForceMode.VelocityChange);
		}
	}
}