using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D myRigidbody2D;
	private int cargo;
	private bool isInvincible = false;
	private SpriteRenderer image;
	private float capSpeed;

	[SerializeField] private float sideSpeed = 0.1f;
	[SerializeField] private float upSpeed = 0.1f;
	[SerializeField] private float maxSpeed = 20;
	[SerializeField] private float maxInvincibilityTime = 2;
	[SerializeField] private float InvincibilityBlinkInterval = 0.2f;
	[SerializeField] private int maxCargo = 1000;
	[SerializeField] private GameManager gameManager;
	[SerializeField] private float slowSpeed = 10;

	private void Start()
	{
		myRigidbody2D = GetComponent<Rigidbody2D>();
		image = GetComponentInChildren<SpriteRenderer>();
		cargo = maxCargo;
		capSpeed = maxSpeed;
		if (InvincibilityBlinkInterval > maxInvincibilityTime)
		{
			InvincibilityBlinkInterval = maxInvincibilityTime;
		}
	}

	// Update is called once per frame
	private void Update()
	{
		Vector2 v = Vector2.up * Input.GetAxis("Vertical") * upSpeed * Time.deltaTime;
		Vector2 h = Vector2.right * Input.GetAxis("Horizontal") * sideSpeed * Time.deltaTime;
		Vector2 myVelocity = v + h;

		//set capspeed to slow or max speed wether the related button has been pressed or released
		if (Input.GetButton("Fire1"))
		{
			capSpeed = slowSpeed;
		}
		else if (Input.GetButtonUp("Fire1"))
		{
			capSpeed = maxSpeed;
		}

		//checks for capspeed
		if (Mathf.Abs(myVelocity.x) > capSpeed)
		{
			myVelocity.x = Mathf.Sign(myVelocity.x) * capSpeed;
		}

		if (Mathf.Abs(myVelocity.y) > capSpeed)
		{
			myVelocity.y = Mathf.Sign(myVelocity.y) * capSpeed;
		}

		myRigidbody2D.velocity = myVelocity;
	}

	public int GetCargo()
	{
		return cargo;
	}

	public int GetMaxCargo()
	{
		return maxCargo;
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

	private void DeathPlayer()
	{
		Destroy(gameObject);
		gameManager.GameOver();
	}

	private void ReleaseCargo(int lest)
	{
		cargo -= lest;
		if (cargo <= 0)
		{
			DeathPlayer();
		}
	}

	private IEnumerator SetInvincibility(float time)
	{
		isInvincible = true;

		for (float i = 0; i <= time; i += InvincibilityBlinkInterval)
		{
			//make the sprite of the player blink through alpha channel
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