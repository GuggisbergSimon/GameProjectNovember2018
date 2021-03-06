﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Player : MonoBehaviour
{
	#region player variables

	private AudioSource myAudioSource;
	private Rigidbody2D myRigidbody2D;
	private bool isInvincible = false;
	private float speed;
	private int cargo;
	private CinemachineVirtualCamera vcam;
	private CinemachineBasicMultiChannelPerlin noise;
	private Animator myAnimator;
	private bool isAlive = true;

	[SerializeField] private int maxCargo = 1000;
	[SerializeField] private Image cargoGauge;
	[SerializeField] private float maxSpeed = 10.0f;
	[SerializeField] private float slowSpeed = 7.0f;
	[SerializeField] private float maxInvincibilityTime = 2.0f;
	[SerializeField] private float InvincibilityBlinkInterval = 0.2f;
	[SerializeField] private float timeShaking = 0.5f;
	[SerializeField] private GameManager gameManager;
	[SerializeField] private GameObject balloonModel;
	[SerializeField] private AudioClip explosionSound;
	[SerializeField] private AudioClip hitSound;
	[SerializeField] private float timeScaleSlowDown = 0.7f;
	[SerializeField] private AnimationClip explosionAnimation;

	#endregion

	private void Start()
	{
		myAudioSource = GetComponent<AudioSource>();
		myRigidbody2D = GetComponent<Rigidbody2D>();
		vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
		noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		myAnimator = GetComponent<Animator>();

		cargo = maxCargo;
		speed = maxSpeed;
		if (InvincibilityBlinkInterval > maxInvincibilityTime)
		{
			InvincibilityBlinkInterval = maxInvincibilityTime;
		}
	}

	private void Update()
	{
		if (isAlive)
		{
			CheckSpecialActions();
			Move();
		}
	}

	//set timescale to slow or normal timescale if the related button has been pressed or released
	// also adjusts the speed so that this action won't be abused
	private void CheckSpecialActions()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			speed = slowSpeed;
			gameManager.SetTimeScaleTo(timeScaleSlowDown);
		}
		else if (Input.GetButtonUp("Fire1"))
		{
			speed = maxSpeed;
			gameManager.SetTimeScaleTo(1.0f);
		}
	}

	private void Move()
	{
		Vector2 v = Vector2.up * Input.GetAxis("Vertical");
		Vector2 h = Vector2.right * Input.GetAxis("Horizontal");
		Vector2 nextPos = (v + h) * speed * Time.deltaTime;
		myRigidbody2D.MovePosition(transform.position + (Vector3) nextPos);
	}

	// Handle the "collision" between the player and enemy with the tag enemy
	// there are no real collisions in this game, only triggers
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Enemy") && !isInvincible && isAlive)
		{
			Enemy enemy = other.gameObject.GetComponent<Enemy>();
			TakeDamage(enemy.Damage);

			if (enemy.IsDestructibleByPlayer)
			{
				Destroy(other.gameObject);
			}
		}
	}

	//handle the death of the player
	private IEnumerator DeathPlayer()
	{
		gameManager.SetTimeScaleTo(1.0f);
		isAlive = false;
		myAnimator.SetTrigger("Death");
		myAudioSource.clip = explosionSound;
		myAudioSource.loop = false;
		myAudioSource.volume = 1.0f;
		myAudioSource.Play();
		balloonModel.SetActive(false);
		yield return new WaitForSeconds(explosionAnimation.length);

		Destroy(gameObject);
		gameManager.GameOver();
	}

	public bool IsAlive
	{
		get { return isAlive; }
	}

	//handle when the player release some cargo
	private void TakeDamage(int damage)
	{
		cargo -= damage;
		cargoGauge.fillAmount = (float) cargo / maxCargo;
		StartCoroutine(SetShaking(damage, damage, timeShaking));
		if (cargo <= 0 && isAlive)
		{
			StartCoroutine(DeathPlayer());
		}
		else
		{
			StartCoroutine(SetInvincibility(maxInvincibilityTime));
			StartCoroutine(PlaySound(hitSound));
		}
	}

	// Play a sound then reverts to the sound playing before on the source
	private IEnumerator PlaySound(AudioClip clip)
	{
		AudioClip originalClip = myAudioSource.clip;
		bool originalIsLooping = myAudioSource.loop;
		float originalVolume = myAudioSource.volume;
		myAudioSource.clip = clip;
		myAudioSource.loop = false;
		myAudioSource.volume = 1.0f;
		myAudioSource.Play();
		yield return new WaitForSeconds(clip.length);
		myAudioSource.clip = originalClip;
		myAudioSource.loop = originalIsLooping;
		myAudioSource.volume = originalVolume;
		myAudioSource.Play();
	}

	//Handle the invincibility process and its blinking
	private IEnumerator SetInvincibility(float time)
	{
		if (isAlive)
		{
			isInvincible = true;

			for (float i = 0; i <= time; i += InvincibilityBlinkInterval)
			{
				//make the sprite of the player blink via changing the value of alpha channel
				if (balloonModel.activeInHierarchy)
				{
					balloonModel.SetActive(false);
				}
				else
				{
					balloonModel.SetActive(true);
				}

				yield return new WaitForSeconds(InvincibilityBlinkInterval);
			}

			balloonModel.SetActive(true);
			isInvincible = false;
		}
	}

	private IEnumerator SetShaking(float amplitudeGain, float frequencyGain, float time)
	{
		ShakeCamera(amplitudeGain, frequencyGain);
		yield return new WaitForSeconds(time);
		ShakeCamera(0, 0);
	}

	private void ShakeCamera(float amplitudeGain, float frequencyGain)
	{
		noise.m_AmplitudeGain = amplitudeGain;
		noise.m_FrequencyGain = frequencyGain;
	}
}