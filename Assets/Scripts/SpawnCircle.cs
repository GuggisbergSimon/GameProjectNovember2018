using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class SpawnCircle : MonoBehaviour
{
	[SerializeField] private Enemy bulletPrefab;
	[SerializeField] private int numberShots;
	[SerializeField] private float radius;
	[SerializeField] private float interval;
	[SerializeField] private Player player;
	
	private void Start()
	{
		transform.position = player.transform.position;
		if (interval > 0)
		{
			StartCoroutine(FireAndWait(interval));
		}
		else
		{
			for (int i = 0; i < numberShots; i++)
			{
				FireSingle();
			}
		}
	}

	//Fire and wait interval time
	private IEnumerator FireAndWait(float time)
	{
		for (int i = 0; i < numberShots; i++)
		{
			FireSingle();
			yield return new WaitForSeconds(time);
		}
	}

	//Fire and then rotate this object
	private void FireSingle()
	{

		Instantiate(bulletPrefab, transform.up * -radius, transform.rotation);
		transform.Rotate(transform.forward, (float)360 / (float)numberShots);
	}
}