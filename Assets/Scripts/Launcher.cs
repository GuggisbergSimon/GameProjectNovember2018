using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : Enemy
{
	[SerializeField] private float rotationAngle = 10;
	[SerializeField] private float maxAngle = 45;
	[SerializeField] private GameObject bullet;
	[SerializeField] private float fireRatePerSeconds = 1;

	private Vector3 initialAngle;
	
	//Vector3 initialPos;

	void Start()
	{
		//initialPos = transform.up * 10;
		initialAngle = transform.rotation.eulerAngles;
		StartCoroutine(Fire(1 / fireRatePerSeconds));
	}

	private void Update()
	{
		/*Vector3 line = transform.up * 10;
		Vector3 rotated = Quaternion.AngleAxis(maxAngle, Vector3.forward) * initialPos;
		Vector3 rotated2 = Quaternion.AngleAxis(-maxAngle, Vector3.forward) * initialPos;
		Debug.DrawLine(transform.position, transform.position + line, Color.red);
		Debug.DrawLine(transform.position, transform.position + rotated);
		Debug.DrawLine(transform.position, transform.position + rotated2);*/
	}

	private IEnumerator Fire(float time)
	{
		for (;;)
		{
			Instantiate(bullet, transform.position, transform.rotation);
			//TODO correct following line for when then euler angle goes from 350 to 0.
			if (Mathf.Abs(Mathf.Abs(transform.rotation.eulerAngles.z + rotationAngle) - initialAngle.z) > maxAngle)
			{
				rotationAngle = -rotationAngle;
			}

			transform.Rotate(Vector3.forward * rotationAngle);
			yield return new WaitForSeconds(time);
		}
	}
}