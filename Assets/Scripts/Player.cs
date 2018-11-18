using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Experimental.PlayerLoop;

public class Player : MonoBehaviour
{
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
	[SerializeField] private float slowSpeed = 5.0f;
	[SerializeField] private float maxInvincibilityTime = 2.0f;
	[SerializeField] private float InvincibilityBlinkInterval = 0.2f;
	[SerializeField] private float timeShaking = 0.5f;
	[SerializeField] private GameManager gameManager;
	[SerializeField] private AnimationClip explosionAnimation;
	[SerializeField] private GameObject balloonModel;
	[SerializeField] private AudioClip explosionSound;

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

	//handle when the player release some cargo
	private void ReleaseCargo(int lest)
	{
		cargo -= lest;
		if (cargo <= 0 && isAlive)
		{
			StartCoroutine(DeathPlayer());
		}

		cargoGauge.fillAmount = (float) cargo / maxCargo;
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