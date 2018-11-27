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
			Destroy(gameObject);
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
		Destroy(gameObject);
	}


	//Calculate the position for the shot and then proceeds to fire, rotating the object based on the position of the player.
	private void FireSingle(int shotNumber)
	{
		Vector3 posSpawn = beginning.transform.position +
		                   shotNumber * (ending.transform.position - beginning.transform.position) / (numberShots - 1);
		Instantiate(bulletPrefab, posSpawn,
			Quaternion.Euler(0, 0, Vector3.SignedAngle(player.transform.position - posSpawn, Vector3.up,Vector3.back)));
	}
}