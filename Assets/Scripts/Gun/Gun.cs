using UnityEngine;

public class Gun : MonoBehaviour
{
	public void AimAtPoint(Vector3 point)
	{
		transform.LookAt(point);
	}
}