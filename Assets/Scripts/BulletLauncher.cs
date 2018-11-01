using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
	[SerializeField] private GameObject bullet;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		//test to remove
		Instantiate(bullet, transform.position, transform.rotation);
		transform.Rotate(Vector3.forward * 10);
	}
}