using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManagerControlBehaviour : PlayableBehaviour
{
	public GameManager GameManager { get; set; }

	public GameObject[] PatternToShoot { get; set; }

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		for (int i = 0; i < PatternToShoot.Length; i++)
		{
			GameManager.Fire(PatternToShoot[i]);
		}
	}
}