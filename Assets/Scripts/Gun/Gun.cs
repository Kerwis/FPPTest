using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
	public class Gun : MonoBehaviour
	{
		[SerializeField] 
		private Bullet bulletPrefab;
		[SerializeField]
		private Transform muzzle;
		[SerializeField]
		private int maxBullet = 200;
		[SerializeField]
		private AudioSource audioSource;
		
		private readonly Queue<Bullet> bulletFired = new Queue<Bullet>();
		
		public void AimAtPoint(Vector3 point)
		{
			transform.LookAt(point);
		}

		public void Fire()
		{
			audioSource.pitch = Random.Range(0.85f, 1.15f);
			audioSource.Play();
			var bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
			bullet.Fire(muzzle.forward);
			bulletFired.Enqueue(bullet);
			if (bulletFired.Count > maxBullet)
			{
				Destroy(bulletFired.Dequeue().gameObject);
			}
		}
	}
}