using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BasicMovable
{
	[SerializeField] private int damage = 10;
	[SerializeField] private bool isDestructibleByPlayer = false;
	[SerializeField] private float delayBeforeMovement = 0;

	public int GetDamage()
	{
		return damage;
	}

	public bool IsDestructibleByPlayer()
	{
		return isDestructibleByPlayer;
	}
}