using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavigateMenuWithoutMouse : MonoBehaviour
{
	[SerializeField] private EventSystem eventSystem;
	[SerializeField] private GameObject firstSelectedObject;

	private bool buttonSelected;

	private void Start()
	{
		eventSystem.SetSelectedGameObject(firstSelectedObject);
		buttonSelected = true;
	}

	private void Update()
	{
		if ((Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) && buttonSelected == false)
		{
			eventSystem.SetSelectedGameObject(firstSelectedObject);
			buttonSelected = true;
		}
	}

	private void OnDisable()
	{
		buttonSelected = false;
	}
}