﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Player : MonoBehaviour
{
	private Rigidbody2D myRigidbody2D;
	private bool isInvincible = false;
	private SpriteRenderer image;
	private float speed;
	private int cargo;
	private CinemachineVirtualCamera vcam;
	private CinemachineBasicMultiChannelPerlin noise;
	private Animator myAnimator;

	[SerializeField] private int maxCargo = 1000;
	[SerializeField] private Image cargoGauge;
	[SerializeField] private float maxSpeed = 10.0f;
	[SerializeField] private float slowSpeed = 5.0f;
	[SerializeField] private float maxInvincibilityTime = 2.0f;
	[SerializeField] private float InvincibilityBlinkInterval = 0.2f;
	[SerializeField] private float timeShaking = 0.5f;
	[SerializeField] private GameManager gameManager;
	[SerializeField] private AnimationClip explosionAnimation;

	private void Start()
	{
		myRigidbody2D = GetComponent<Rigidbody2D>();
		image = GetComponentInChildren<SpriteRenderer>();
		vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
		noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		myAnimator = GetComponentInChildren<Animator>();

		cargo = maxCargo;
		speed = maxSpeed;
		if (InvincibilityBlinkInterval > maxInvincibilityTime)
		{
			InvincibilityBlinkInterval = maxInvincibilityTime;
		}
	}

	// Update is called once per frame
	private void Update()
	{
		Vector2 v = Vector2.up * Input.GetAxis("Vertical");
		Vector2 h = Vector2.right * Input.GetAxis("Horizontal");
		
		//set speed to slow or max speed wether the related button has been pressed or released
		if (Input.GetButton("Fire1"))
		{
			speed = slowSpeed;
		}
		else if (Input.GetButtonUp("Fire1"))
		{
			speed = maxSpeed;
		}

		Vector2 nextPos = (v + h) * speed * Time.deltaTime;
		myRigidbody2D.MovePosition(transform.position + (Vector3) nextPos);
	}

	// Handle the "collision" between the player and enemy with the tag enemy
	//there are no real collisions in this game, only triggers
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Enemy") && !isInvincible)
		{
			Enemy enemy = other.gameObject.GetComponent<Enemy>();
			int damageTaken = enemy.Damage;
			ReleaseCargo(damageTaken);
			StartCoroutine(SetShaking(damageTaken, damageTaken, timeShaking));
			StartCoroutine(SetInvincibility(maxInvincibilityTime));

			if (enemy.IsDestructibleByPlayer)
			{
				Destroy(other.gameObject);
			}
		}
	}

	//handle the death of the player
	private IEnumerator DeathPlayer()
	{
		myAnimator.SetTrigger("Death");
		yield return new WaitForSeconds(explosionAnimation.length);
		image.color = Color.clear;

		Destroy(gameObject);
		gameManager.GameOver();
	}
	
	//handle when the player release some cargo
	private void ReleaseCargo(int lest)
	{
		cargo -= lest;
		if (cargo <= 0)
		{
			StartCoroutine(DeathPlayer());
		}
		
		cargoGauge.fillAmount = (float) cargo / maxCargo;
	}

	//Handle the invincibility process and its blinking
	private IEnumerator SetInvincibility(float time)
	{
		isInvincible = true;

		for (float i = 0; i <= time; i += InvincibilityBlinkInterval)
		{
			//make the sprite of the player blink via changing the value of alpha channel
			Color tempColor = image.color;
			if (tempColor.a.CompareTo(1.0f) == 0)
			{
				tempColor.a = 0.0f;
			}
			else
			{
				tempColor.a = 1.0f;
			}

			image.color = tempColor;
			yield return new WaitForSeconds(InvincibilityBlinkInterval);
		}
		image.color = Color.white;

		isInvincible = false;
	}

	private IEnumerator SetShaking(float amplitudeGain, float frequencyGain, float time)
	{
		ShakeCamera(amplitudeGain,frequencyGain);
		yield return new WaitForSeconds(time);
		ShakeCamera(0,0);
	}

	private void ShakeCamera(float amplitudeGain, float frequencyGain)
	{
		noise.m_AmplitudeGain = amplitudeGain;
		noise.m_FrequencyGain = frequencyGain;
	}
}