using UnityEngine;
using System.Collections;

public class DeathX : MonoBehaviour {

	private string player;

	// Use this for initialization
	void Start () {
		player = GetComponentInParent<UIPlayerInfo>().player;
	}
	
	// Update is called once per frame
	void Update () {
		GameMonitorScript gm = GameObject.FindGameObjectWithTag("GameMonitor").GetComponent<GameMonitorScript>();
		foreach (PlayerInfo pi in gm.activePlayers)
		{
			if (pi.playerNum == player && !pi.alive)
			{
				gameObject.GetComponent<CanvasGroup>().alpha = 1.0f;
			}
		}
		/*if (player == "P1")
		{
			if (!GameObject.FindGameObjectWithTag("GameMonitor").GetComponent<GameMonitorScript>().p1Alive)
			{
				gameObject.GetComponent<CanvasGroup>().alpha = 1.0f;
			}
		}
		else if (player == "P2")
		{
			if (!GameObject.FindGameObjectWithTag("GameMonitor").GetComponent<GameMonitorScript>().p2Alive)
			{
				gameObject.GetComponent<CanvasGroup>().alpha = 1.0f;
			}
		}
		else if (player == "P3")
		{
			if (!GameObject.FindGameObjectWithTag("GameMonitor").GetComponent<GameMonitorScript>().p3Alive)
			{
				gameObject.GetComponent<CanvasGroup>().alpha = 1.0f;
			}
		}
		else if (player == "P4")
		{
			if (!GameObject.FindGameObjectWithTag("GameMonitor").GetComponent<GameMonitorScript>().p4Alive)
			{
				gameObject.GetComponent<CanvasGroup>().alpha = 1.0f;
			}
		}
		else
		{
			gameObject.GetComponent<CanvasGroup>().alpha = 0.0f;
		}*/
	}

	public string getPlayer()
	{
		return this.player;
	}
}
