using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovable : MonoBehaviour
{
	[SerializeField] private float lifeTime = -1;
	[SerializeField] private bool destroyIfInvisible = true;
	[SerializeField] private float horizontalSpeed = 5;
	[SerializeField] private float period = 1;
	[SerializeField] private float amplitude = 0;

	private Vector3 axis;
	private Vector3 pos;

	protected void Start()
	{
		pos = transform.position;
		axis = transform.right;

		if (lifeTime > 0)
		{
			Destroy(gameObject, lifeTime);
		}
	}

	protected void Update()
	{
		SinMove();
	}

	protected void SinMove()
	{
		pos += transform.up * Time.deltaTime * horizontalSpeed;
		transform.position = pos + axis * Mathf.Sin(Time.time * period) * amplitude;
	}
	
	private void OnBecameInvisible()
	{
		if (destroyIfInvisible )
		{
			Destroy(gameObject);
		}
	}
}