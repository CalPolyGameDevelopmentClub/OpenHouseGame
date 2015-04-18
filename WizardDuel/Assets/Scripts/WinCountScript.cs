using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class WinCountScript : MonoBehaviour {

	private string player;

	// Use this for initialization
	void Start () {
		player = gameObject.GetComponentInParent<UIPlayerInfo>().player;
	}
	
	// Update is called once per frame
	void Update () {
		GameMonitorScript gm = GameObject.FindGameObjectWithTag("GameMonitor").GetComponent<GameMonitorScript>();
		foreach (PlayerInfo pi in gm.activePlayers)
		{
			/*if (pi.playerNum == player)
			{
				gameObject.GetComponent<Text>().text = pi.wins.ToString();
			}*/
		}

	}
}
