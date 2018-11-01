using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Player : MonoBehaviour
{
	private Rigidbody2D myRigidbody2D;
	[SerializeField] private float sideSpeed = 0.1f;
	[SerializeField] private float upForce = 0.1f;
	[SerializeField] private float jumpSpeed = 1;
	[SerializeField] private float maxSpeed = 10;
	[SerializeField] private int cargo = 1000;
	[SerializeField] private GameManager gameManager;

	private void Start()
	{
		myRigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		if (Input.GetAxis("Vertical") > 0)
		{
			Vector2 v = Vector2.up * Input.GetAxis("Vertical") * upForce;
			myRigidbody2D.AddForce(v);
		}
		
		myRigidbody2D.mass = cargo;
	}

	// Update is called once per frame
	private void Update()
	{
		Vector2 myVelocity = myRigidbody2D.velocity;
		Vector2 h = Vector2.right * Input.GetAxis("Horizontal") * sideSpeed;
		//Vector2 v = Vector2.up * Input.GetAxis("Vertical") * upSpeed;
		//myVelocity += v + h;
		myVelocity += h;

		//release cargo
		if (Input.GetButton("Jump"))
		{
			cargo -= 1;
			Debug.Log(cargo);
			myVelocity += Vector2.up*jumpSpeed;
		}

		//checks for maxSpeed
		if (Mathf.Abs(myVelocity.x) > maxSpeed)
		{
			myVelocity.x = Mathf.Sign(myVelocity.x) * maxSpeed;
		}

		if (Mathf.Abs(myVelocity.y) > maxSpeed)
		{
			myVelocity.y = Mathf.Sign(myVelocity.y) * maxSpeed;
		}

		myRigidbody2D.velocity = myVelocity;
		Debug.DrawLine(transform.position, transform.position + (Vector3) myVelocity, Color.red, 120);
	}

	public int GetCargo()
	{
		return cargo;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			//cargo -= other.gameObject.GetComponent<Pirates>().GetDamage();
			Destroy(this);
			gameManager.GameOver();
		}
	}
}