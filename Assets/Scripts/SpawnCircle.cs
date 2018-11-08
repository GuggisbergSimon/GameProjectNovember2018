using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class SpawnCircle : MonoBehaviour
{
	[SerializeField] private Enemy bullet;
	[SerializeField] private int numberShots;
	[SerializeField] private float radius;
	[SerializeField] private float interval;
	[SerializeField] private Player player;

	// Use this for initialization
	private void Start()
	{
		transform.position = player.transform.position;
		if (interval > 0)
		{
			StartCoroutine(FireAndWait());
		}
		else
		{
			for (int i = 0; i < numberShots; i++)
			{
				FireSingle();
			}
		}
	}

	private IEnumerator FireAndWait()
	{
		for (int i = 0; i < numberShots; i++)
		{
			FireSingle();
			yield return new WaitForSeconds(interval);
		}
	}

	private void FireSingle()
	{

		Instantiate(bullet, transform.up * -radius, transform.rotation);
		transform.Rotate(transform.forward, (float)360 / (float)numberShots);
	}
}