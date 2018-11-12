using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
	private static GameObject checkStatic;

	private void Start()
	{
		//keep only one globalmanager in each scene
		if (checkStatic == null)
		{
			DontDestroyOnLoad(gameObject);
			checkStatic = gameObject;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Update()
	{
		CheckEsc();
	}

	//check for escape button, the alt-f4 alternative
	private void CheckEsc()
	{
		if (Input.GetButtonDown("Cancel"))
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
		}
	}
}