using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovable : MonoBehaviour
{
	[SerializeField] private float amplitude = 0;
	[SerializeField] private float period = 1;
	[SerializeField] private float horizontalPhase = 0;
	[SerializeField] private float speed = 0.5f;
	[SerializeField] private float angleForMovement = 90;
	[SerializeField] private float lifeTime = 30;
	[SerializeField] private bool destroyIfInvisible = true;

	private void Start()
	{
		StartCoroutine(DestroyInSeconds(lifeTime));
	}

	void Update()
	{
		//float y = amplitude * Mathf.Sin((2 * Mathf.PI / period) * (Time.time - horizontalPhase));
		float y = amplitude * Mathf.Cos(Time.time*period);
		if (gameObject.name.Contains("launcher"))
		{
			Debug.Log(y);
		}
		Vector2 nextPos = Vector2.up * y + Vector2.right * speed;
		nextPos = Quaternion.Euler(0, 0, angleForMovement) * nextPos;
		transform.Translate(nextPos);
	}

	IEnumerator DestroyInSeconds(float time)
	{
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}

	private void OnBecameInvisible()
	{
		if (destroyIfInvisible)
		{
			Destroy(gameObject);
		}
	}
}