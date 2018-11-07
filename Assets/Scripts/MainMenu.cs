using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	
	void Update ()
	{
		CheckPause();
	}

	private void CheckPause()
	{
		if (Input.GetButtonDown("Cancel"))
		{
			QuitGame();
		}
	}

	public void LoadLevel(String level)
	{
		SceneManager.LoadScene(level);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
