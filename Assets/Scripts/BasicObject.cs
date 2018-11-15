using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicObject : MonoBehaviour
{
	private CheckRenderer myCheckRenderer;

	protected void Start()
	{
		myCheckRenderer = GetComponentInChildren<CheckRenderer>();
	}

	protected bool CheckInvisibility()
	{
		return myCheckRenderer.CheckInvisibility();
	}

	protected void Update()
	{
		if (myCheckRenderer.IsInvisible)
		{
			Destroy(gameObject);
		}
	}
}