using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float amplitude;
	[SerializeField] private float phase;
	[SerializeField] private float speed;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = transform.position +
		                     (Vector3) (Vector2.up * Mathf.Cos(Time.deltaTime + phase * amplitude) +
		                                Vector2.right * speed * Time.deltaTime);
	}
}