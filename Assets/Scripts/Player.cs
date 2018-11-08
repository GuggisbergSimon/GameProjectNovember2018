using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	private Rigidbody2D myRigidbody2D;
	private bool isInvincible = false;
	private SpriteRenderer image;
	private float speed;
	private int cargo;

	[SerializeField] private int maxCargo = 1000;
	[SerializeField] private Image cargoGauge;
	[SerializeField] private float maxSpeed = 10;
	[SerializeField] private float slowSpeed = 5;
	[SerializeField] private float maxInvincibilityTime = 2;
	[SerializeField] private float InvincibilityBlinkInterval = 0.2f;
	[SerializeField] private GameManager gameManager;

	private void Start()
	{
		myRigidbody2D = GetComponent<Rigidbody2D>();
		image = GetComponentInChildren<SpriteRenderer>();
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
		
		//set capspeed to slow or max speed wether the related button has been pressed or released
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

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Enemy") && !isInvincible)
		{
			Enemy enemy = other.gameObject.GetComponent<Enemy>();
			ReleaseCargo(enemy.GetDamage());
			StartCoroutine(SetInvincibility(maxInvincibilityTime));

			if (enemy.IsDestructibleByPlayer())
			{
				Destroy(other.gameObject);
			}
		}
	}

	//handle the death of the player
	private void DeathPlayer()
	{
		Destroy(gameObject);
		gameManager.GameOver();
	}

	//handle when the player release some cargo
	private void ReleaseCargo(int lest)
	{
		cargo -= lest;
		if (cargo <= 0)
		{
			DeathPlayer();
		}

		cargoGauge.fillAmount = (float) cargo / (float) maxCargo;
	}

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
}