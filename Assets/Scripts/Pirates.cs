using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirates : MonoBehaviour
{
	[SerializeField] private int damage = 10;
	[SerializeField] private float speed = 1;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		Vector2 move = (Vector2) transform.position+Vector2.up*speed*Time.deltaTime;
		transform.position = move;
	}

	public int GetDamage()
	{
		return damage;
	}
}