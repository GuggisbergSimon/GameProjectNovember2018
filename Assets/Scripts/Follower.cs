using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : Enemy
{
	[SerializeField] private Player player;
	[SerializeField] private float speed = 5;
	[SerializeField] private float distanceMinimum = 5;
	[SerializeField] private bool noTurn = false;

	private void Start()
	{
		player = FindObjectOfType<Player>();
		Rotate();
	}

	private void Update()
	{
		CheckClose();
		Rotate();

		transform.Translate(Vector3.up * speed * Time.deltaTime);
	}

	private void CheckClose()
	{
		if (speed > 0 && (player.transform.position - transform.position).magnitude < distanceMinimum)
		{
			noTurn = true;
		}
	}

	private void Rotate()
	{
		if (!noTurn)
		{
			transform.up = player.transform.position - transform.position;
		}
	}
}