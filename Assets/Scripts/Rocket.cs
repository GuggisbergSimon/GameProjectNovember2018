using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	[SerializeField] private Player player;

	private void Start()
	{
		player = FindObjectOfType<Player>();
	}

	void Update()
	{
		transform.up = player.transform.position - transform.position;
	}
}