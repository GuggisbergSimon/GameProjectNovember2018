using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
	}

	//triggered when the balloon arrives to the top.
	public void EndLevel()
	{
		SceneManager.LoadScene("EndMissionMenu");
	}

	//triggered when the player's Cargo = 0
	public void GameOver()
	{
		SceneManager.LoadScene("GameOverMenu");
	}
}