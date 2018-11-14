﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cloud : MonoBehaviour
{
	[SerializeField] private Sprite[] sprites;
	[SerializeField] private float speed = 10;
	[SerializeField] private float margeSpeed = 1;

	private SpriteRenderer mySpriteRender;

	//Select a random mesh between the ones given in the inspector
	private void Start()
	{
		mySpriteRender = GetComponentInChildren<SpriteRenderer>();
		ChangeSprite(Random.Range(0, sprites.Length - 1));
		speed += Random.Range(-margeSpeed, margeSpeed);
	}

	private void Update()
	{
		transform.position = transform.position + Vector3.down * speed * Time.deltaTime;
	}

	//ChangeSprite based on its index
	public void ChangeSprite(int index)
	{
		if (index < sprites.Length && index >= 0)
		{
			mySpriteRender.sprite = sprites[index];
		}
		else
		{
			Debug.Log("ChangeSprite : wrong index entered");
		}
	}
}