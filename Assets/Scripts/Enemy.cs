using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private int damage = 10;
	[SerializeField] private float delayTime = 0.0f;
	[SerializeField] private bool isDestructibleByPlayer = false;
	[SerializeField] private bool destroyIfInvisible = true;
	[SerializeField] private float lifeTime = -1.0f;

	protected bool canMove = false;

	protected void Start()
	{
		if (lifeTime > 0)
		{
			Destroy(gameObject, lifeTime);
		}
		StartCoroutine(DelayMove(delayTime));
	}

	public int Damage
	{
		get { return damage; }
	}

	public bool IsDestructibleByPlayer
	{
		get { return isDestructibleByPlayer; }
	}

	private IEnumerator DelayMove(float delay)
	{
		yield return new WaitForSeconds(delay);
		canMove = true;
	}

	//TODO To Test properly
	private void OnBecameInvisible()
	{
		if (destroyIfInvisible)
		{
			Destroy(gameObject);
		}
	}
}