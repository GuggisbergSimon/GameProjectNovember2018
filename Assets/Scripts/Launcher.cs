using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : Enemy
{
	[SerializeField] private float periodRotation = 1;
	[SerializeField] private float maxAngleRotation = 0;
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private float fireRatePerSeconds = 1.8f;

	private float initAngle;
	private bool isLaunching = false;
	private float timer = 0.0f;

	private new void Start()
	{
		base.Start();
		initAngle = transform.rotation.eulerAngles.z;
	}

	private void Update()
	{
		if (canMove)
		{
			timer += Time.deltaTime;
			float phase = Mathf.Sin((timer / periodRotation));
			transform.localRotation = Quaternion.Euler(new Vector3(0, 0, initAngle + (phase * maxAngleRotation)));
			if (!isLaunching)
			{
				StartCoroutine(Fire(1 / fireRatePerSeconds));
			}
		}

		Debug.DrawLine(transform.position, transform.position + transform.up * 10, Color.red);
	}

	private IEnumerator Fire(float time)
	{
		isLaunching = true;
		for (;;)
		{
			Instantiate(bulletPrefab, transform.position, transform.rotation);
			yield return new WaitForSeconds(time);
		}
	}
}