using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Player : MonoBehaviour
{
	private Rigidbody2D myRigidbody2D;
	private int cargo;
	private float invincibilityTime = 0;
	private bool isInvincible = false;

	[SerializeField] private float sideSpeed = 0.1f;
	[SerializeField] private float upSpeed = 0.1f;
	[SerializeField] private float jumpSpeed = 1;
	[SerializeField] private float maxSpeed = 10;
	[SerializeField] private int cargoUpCost = 1;
	[SerializeField] private int maxCargo = 1000;
	[SerializeField] private GameManager gameManager;

	private void Start()
	{
		myRigidbody2D = GetComponent<Rigidbody2D>();
		cargo = maxCargo;
	}

	// Update is called once per frame
	private void Update()
	{
		Vector2 myVelocity = myRigidbody2D.velocity;
		Vector2 v = Vector2.zero;
		if (Input.GetAxis("Vertical") > 0)
		{
			v = Vector2.up * Input.GetAxis("Vertical") * upSpeed * Time.deltaTime;
		}

		Vector2 h = Vector2.right * Input.GetAxis("Horizontal") * sideSpeed * Time.deltaTime;
		myVelocity += v + h;

		//release cargo
		if (Input.GetButton("Jump"))
		{
			ReleaseCargo(cargoUpCost);
			myVelocity += Vector2.up * jumpSpeed * Time.deltaTime;
		}

		//checks for maxSpeed
		if (Mathf.Abs(myVelocity.x) > maxSpeed)
		{
			myVelocity.x = Mathf.Sign(myVelocity.x) * maxSpeed;
		}

		if (myVelocity.y > maxSpeed)
		{
			myVelocity.y = Mathf.Sign(myVelocity.y) * maxSpeed;
		}

		if (isInvincible)
		{
			invincibilityTime -= Time.deltaTime;
			if (invincibilityTime < 0)
			{
				isInvincible = false;
			}
		}

		myRigidbody2D.velocity = myVelocity;
		Debug.DrawLine(transform.position, transform.position + (Vector3) myVelocity, Color.red, 120);
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
			ReleaseCargo(other.gameObject.GetComponent<Pirates>().GetDamage());
		}
	}

	private void DeathPlayer()
	{
		Destroy(this);
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

	private void SetInvincibility(float time)
	{
		isInvincible = true;
		invincibilityTime = time;
	}
}