using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovable : MonoBehaviour
{
	[SerializeField] private float speed = 2.0f;
	[SerializeField] private float period = 10;
	[SerializeField] private float amplitude = 1;
	[SerializeField] private float lifeTime = -1;
	[SerializeField] private bool destroyIfInvisible = true;

	private Vector3 axis;
	private Vector3 pos;

	private void Start()
	{
		pos = transform.position;
		axis = transform.right;
		
		if (lifeTime > 0)
		{
			Destroy(gameObject, lifeTime);
		}
	}

	void Update()
	{
		pos += transform.up * Time.deltaTime * speed;
		transform.position = pos + axis * Mathf.Sin(Time.time * period) * amplitude;
	}

	private void OnBecameInvisible()
	{
		if (destroyIfInvisible)
		{
			Destroy(gameObject);
		}
	}
}