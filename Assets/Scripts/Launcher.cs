using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
	[SerializeField] private float periodRotation = 1;
	[SerializeField] private float maxAngleRotation = 0;
	[SerializeField] private GameObject bullet;
	[SerializeField] private float delayTime=0;
	[SerializeField] private float fireRatePerSeconds = 1.8f;

	private float initAngle;

	private void Start()
	{
		initAngle=transform.rotation.eulerAngles.z;
		StartCoroutine(Fire(1 / fireRatePerSeconds, 1, 0));
	}

	private void Update()
	{
		float phase = Mathf.Sin((Time.time / periodRotation));
		transform.localRotation = Quaternion.Euler(new Vector3(0, 0, initAngle+(phase * maxAngleRotation)));

		//TODO remove that line
		//Debug.DrawLine(transform.position, transform.position + transform.up * 10, Color.red);
	}

	private IEnumerator Fire(float time, int shots, float angle)
	{
		yield return new WaitForSeconds(delayTime);
		for (;;)
		{
			Instantiate(bullet, transform.position, transform.rotation);
			yield return new WaitForSeconds(time);
		}
	}
}