using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] private Image cargoGauge;
	[SerializeField] private Player player;
 
	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		cargoGauge.fillAmount = (float) player.GetCargo() / (float) player.GetMaxCargo();
	}
}
