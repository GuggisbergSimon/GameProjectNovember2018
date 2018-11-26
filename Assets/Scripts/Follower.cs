using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : Enemy
{
	[SerializeField] private Player player;
	[SerializeField] private float speed = 5.0f;
	[SerializeField] private float distanceMinimum = 5.0f;
	[SerializeField] private bool noTurn = false;

	private new void Start()
	{
		base.Start();
		player = FindObjectOfType<Player>();
		Rotate();
	}

	private new void Update()
	{
		CheckClose();
		Rotate();
		if (canMove)
		{
			transform.Translate(Vector3.up * speed * Time.deltaTime);
		}
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