using UnityEngine;
using System.Collections;

public class UIPlayerInfo : MonoBehaviour {

	public string player;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		GameMonitorScript gm = GameObject.FindGameObjectWithTag("GameMonitor").GetComponent<GameMonitorScript>();
		bool active = false;

		foreach (PlayerInfo pi in gm.activePlayers)
		{
			if (player == pi.playerNum)
			{
				active = true;
			}
		}
		/*if (player == "P1")
		{
			active = gameMonitor.GetComponent<GameMonitorScript>().p1Active;
		}
		else if (player == "P2")
		{
			active = gameMonitor.GetComponent<GameMonitorScript>().p2Active;
		}
		else if (player == "P3")
		{
			active = gameMonitor.GetComponent<GameMonitorScript>().p3Active;
		}
		else if (player == "P4")
		{
			active = gameMonitor.GetComponent<GameMonitorScript>().p4Active;
		}*/
		
		if (active)
		{
			gameObject.GetComponent<CanvasGroup>().alpha = 1.0f;
		}
		else
		{
			gameObject.GetComponent<CanvasGroup>().alpha = 0.0f;
		}
	}
}
