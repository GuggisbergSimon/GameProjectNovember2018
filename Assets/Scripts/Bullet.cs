using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Enemy
{
	[SerializeField] private float horizontalSpeed = 5.0f;
	[SerializeField] private float frequency = 1.0f;
	[SerializeField] private float amplitude = 0.0f;

	private Vector3 axis;
	private Vector3 pos;
	private float timer = 0.0f;

	private new void Start()
	{
		base.Start();
		pos = transform.position;
		axis = transform.right;
	}

	private void Update()
	{
		if (canMove)
		{
			SinMove();
			timer += Time.deltaTime;
		}
	}

	private void SinMove()
	{
		pos += transform.up * Time.deltaTime * horizontalSpeed;
		transform.position = pos + axis * Mathf.Sin(timer * frequency) * amplitude;
	}
}