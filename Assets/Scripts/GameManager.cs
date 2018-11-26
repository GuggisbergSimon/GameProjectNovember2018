using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private bool isPause = false;
	private AudioSource myAudioSource;

	[SerializeField] private GameObject panelPause;
	[SerializeField] private CloudSpawner cloudSpawner;
	[SerializeField] private AudioClip enablePause;
	[SerializeField] private AudioClip disablePause;
	[SerializeField] private Player player;

	private void Start()
	{
		player = FindObjectOfType<Player>();
		myAudioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		CheckPause();
	}

	//handles the pause system
	private void CheckPause()
	{
		if (Input.GetButtonDown("Submit"))
		{
			if (isPause)
			{
				Time.timeScale = 1;
				panelPause.SetActive(false);
				isPause = false;
				myAudioSource.clip = disablePause;
				myAudioSource.Play();
			}
			else
			{
				Time.timeScale = 0;
				panelPause.SetActive(true);
				isPause = true;
				myAudioSource.clip = enablePause;
				myAudioSource.Play();
			}
		}
	}

	public void SetTimeScaleTo(float value)
	{
		if (!isPause)
		{
			Time.timeScale = value;
		}
	}

	public void Fire(GameObject pattern)
	{
		Instantiate(pattern);
	}

	public void ChangeColor(int index)
	{
		cloudSpawner.ChangeColor(index);
	}

	public void LoadLevel(string name)
	{
		if (player.IsAlive)
		{
			SceneManager.LoadScene(name);
		}
	}

	public void GameOver()
	{
		SceneManager.LoadScene("GameOverMenu");
	}
}