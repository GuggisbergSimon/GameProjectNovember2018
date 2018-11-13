using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cloud : MonoBehaviour
{
	[SerializeField] private Sprite[] sprites;
	[SerializeField] private float speed = 10;
	[SerializeField] private float margeSpeed = 1;

	private void Start()
	{
		ChangeSprite(Random.Range(0, sprites.Length - 1));
		speed += Random.Range(-margeSpeed, margeSpeed);
		//GetComponentInChildren<SpriteRenderer>().flipY;
	}

	private void Update()
	{
		transform.position = transform.position + Vector3.down * speed * Time.deltaTime;
	}

	public void ChangeSprite(int index)
	{
		GetComponentInChildren<SpriteRenderer>().sprite = sprites[index];
	}
}