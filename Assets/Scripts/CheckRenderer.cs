using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRenderer : MonoBehaviour
{
	private Renderer myRenderer;
	private bool isInvisible = false;

	private void Start()
	{
		myRenderer = GetComponent<Renderer>();
	}

	public bool IsInvisible
	{
		get { return isInvisible; }
	}

	public bool CheckInvisibility()
	{
		isInvisible = !myRenderer.isVisible;
		return isInvisible;
	}

	private void OnBecameVisible()
	{
		isInvisible = false;
	}

	private void OnBecameInvisible()
	{
		isInvisible = true;
	}
}