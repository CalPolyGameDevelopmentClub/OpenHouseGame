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
				Debug.Log(player + " online!");
				active = true;
			}
		}
		
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
