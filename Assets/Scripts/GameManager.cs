using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private bool isPause = false;
	[SerializeField] private GameObject[] patterns;
	[SerializeField] private GameObject panelPause;
	[SerializeField] private float timeMax = 40;
	[SerializeField] private CloudSpawner cloudSpawner;

	private void Update()
	{
		CheckPause();

		if (Time.timeSinceLevelLoad > timeMax)
		{
			LoadLevel("EndMissionMenu");
		}
	}

	//checks wether the player has pressed the pause button
	private void CheckPause()
	{
		if (Input.GetButtonDown("Jump"))
		{
			if (isPause)
			{
				Time.timeScale = 1;
				panelPause.SetActive(false);
				isPause = false;
			}
			else
			{
				Time.timeScale = 0;
				panelPause.SetActive(true);
				isPause = true;
			}
		}
	}

	public void Fire(int index)
	{
		Instantiate(patterns[index]);
	}

	public void LoadLevel(string name)
	{
		SceneManager.LoadScene(name);
	}

	//triggered when the player's Cargo is equal to 0
	public void GameOver()
	{
		SceneManager.LoadScene("GameOverMenu");
	}

	public void ChangeColor(int index)
	{
		cloudSpawner.ChangeColor(index);
	}
}