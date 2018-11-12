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

	private void Start()
	{
		player = FindObjectOfType<Player>();

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
		Vector3 posSpawn = beginning.transform.position +
		                   shotNumber * (ending.transform.position - beginning.transform.position) / (numberShots - 1);
		Instantiate(bulletPrefab, posSpawn,
			Quaternion.Euler(0, 0, Vector3.SignedAngle(player.transform.position - posSpawn, Vector3.up,Vector3.back)));
	}
}