using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private bool isPause = false;

	[SerializeField] private GameObject panelPause;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		CheckPause();
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

	//checks for escape button, the alt-f4 alternative
	private void CheckEsc()
	{
		if (Input.GetButtonDown("Cancel"))
		{
			Application.Quit();
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