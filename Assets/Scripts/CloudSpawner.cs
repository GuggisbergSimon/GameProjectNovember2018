using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
	[SerializeField] private GameObject[] clouds;
	[SerializeField] private float delayStart = 0;
	[SerializeField] private float minInterval = 1;
	[SerializeField] private float maxInterval = 4;
	[SerializeField] private GameObject leftMaxPos;
	[SerializeField] private GameObject rightMaxPos;

	private GameObject currentCloud;

	private void Start()
	{
		if (minInterval > maxInterval)
		{
			minInterval = maxInterval;
		}

		currentCloud = clouds[0];

		StartCoroutine(Spawner());
	}

	private IEnumerator Spawner()
	{
		yield return new WaitForSeconds(delayStart);

		for (;;)
		{
			var x = Random.Range(leftMaxPos.transform.position.x, rightMaxPos.transform.position.x);
			var y = Random.Range(leftMaxPos.transform.position.y, rightMaxPos.transform.position.y);
			Instantiate(currentCloud, new Vector2(x, y), transform.rotation);
			yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
		}
	}

	public void ChangeColor(int index)
	{
		currentCloud = clouds[index];
	}
}