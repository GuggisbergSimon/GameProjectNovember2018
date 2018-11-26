using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManagerPlayableAsset : PlayableAsset
{
	[SerializeField] private ExposedReference<GameManager> gameManager;
	[SerializeField] private GameObject[] pattern;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		var playable = ScriptPlayable<GameManagerControlBehaviour>.Create(graph);
		var gameManagerControlBehaviour = playable.GetBehaviour();
		gameManagerControlBehaviour.GameManager = gameManager.Resolve(graph.GetResolver());

		gameManagerControlBehaviour.PatternToShoot = pattern;

		return playable;
	}
}
