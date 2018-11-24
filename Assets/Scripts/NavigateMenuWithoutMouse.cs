using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavigateMenuWithoutMouse : MonoBehaviour
{
	[SerializeField] private EventSystem eventSystem;
	[SerializeField] private GameObject firstSelectedObject;

	private bool isButtonSelected;

	private void Update()
	{
		if (!isButtonSelected)
		{
			eventSystem.SetSelectedGameObject(firstSelectedObject);
			isButtonSelected = true;
		}
	}

	private void OnDisable()
	{
		isButtonSelected = false;
	}
}