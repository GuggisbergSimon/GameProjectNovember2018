using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
	[SerializeField] private float period = 1;
	[SerializeField] private float maxAngleRotation = 0;
	[SerializeField] private GameObject bullet;
	[SerializeField] private float fireRatePerSeconds = 1;

	void Start()
	{
		StartCoroutine(Fire(1 / fireRatePerSeconds,1,0));
	}

	private void Update()
	{
		float phase = Mathf.Sin(Time.time / period);
		transform.localRotation = Quaternion.Euler(new Vector3(0, 0, phase * maxAngleRotation));

		//Debug.DrawLine(transform.position, transform.position + transform.up * 10, Color.red);
	}

	private IEnumerator Fire(float time, int shots,float angle)
	{
		for (;;)
		{
			Instantiate(bullet, transform.position, transform.rotation);
			yield return new WaitForSeconds(time);
		}
	}
}