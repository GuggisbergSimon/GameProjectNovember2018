using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private bool isPause = false;
	[SerializeField] private GameObject[] patterns;
	[SerializeField] private GameObject panelPause;
	[SerializeField] private float timeMax = 10;

	private void Start()
	{
		StartCoroutine(Fire());
	}

	private void Update()
	{
		CheckPause();

		if (Time.time > timeMax)
		{
			EndLevel();
		}
	}

	//checks wether the player has pressed the pause button
	private void CheckPause()
	{
		if (Input.GetButtonDown("Jump") && !isPause)
		{
			Time.timeScale = 0;
			panelPause.SetActive(true);
			isPause = true;
		}
		else if (Input.GetButtonDown("Jump") && isPause)
		{
			Time.timeScale = 1;
			panelPause.SetActive(false);
			isPause = false;
		}
	}

	IEnumerator Fire()
	{
		for (int i = 0; i < patterns.Length; i++)
		{
			Instantiate(patterns[i]);
			yield return new WaitForSeconds(timeMax / patterns.Length);
		}
	}

	//triggered when the balloon arrives to the top
	public void EndLevel()
	{
		SceneManager.LoadScene("EndMissionMenu");
	}

	//triggered when the player's Cargo is equal to 0
	public void GameOver()
	{
		SceneManager.LoadScene("GameOverMenu");
	}
}