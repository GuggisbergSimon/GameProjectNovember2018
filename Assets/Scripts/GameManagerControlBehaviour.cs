using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManagerControlBehaviour : PlayableBehaviour
{
	private GameManager gameManager;
	private GameObject[] patternToShoot;

	public GameManager GameManager
	{
		get { return gameManager; }
		set { gameManager = value; }
	}

	public GameObject[] PatternToShoot
	{
		get { return patternToShoot; }
		set { patternToShoot = value; }
	}

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		for (int i = 0; i < PatternToShoot.Length; i++)
		{
			gameManager.Fire(PatternToShoot[i]);
		}
	}
}