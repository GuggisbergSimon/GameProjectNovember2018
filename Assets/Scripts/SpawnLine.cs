using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLine : MonoBehaviour
{
	[SerializeField] private Enemy bulletPrefab;
	[SerializeField] private int numberShots;
	[SerializeField] private float interval;
	[SerializeField] private GameObject beginning;
	[SerializeField] private GameObject ending;

	private Player player;
	private float angle;
	private Vector2 initialPos;

	private void Start()
	{
		player = FindObjectOfType<Player>();
		initialPos = beginning.transform.position;

		//TODO define angle so that setup is correct
		Vector3 axis = player.transform.position - (Vector3) initialPos;

		if (interval > 0)
		{
			StartCoroutine(FireAndWait(interval));
		}
		else
		{
			for (int i = 0; i < numberShots; i++)
			{
				FireSingle(i);
			}
		}
	}

	//Fire and wait interval time
	private IEnumerator FireAndWait(float time)
	{
		for (int i = 0; i < numberShots; i++)
		{
			FireSingle(i);
			yield return new WaitForSeconds(time);
		}
	}


	//Fire and then move on to the next object
	private void FireSingle(int shotNumber)
	{
		Instantiate(bulletPrefab,
			(Vector3) initialPos + shotNumber * (ending.transform.position - beginning.transform.position) / numberShots,
			Quaternion.Euler(0, 0, angle));
	}
}