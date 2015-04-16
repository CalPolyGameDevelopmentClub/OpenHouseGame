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
	}

	public string getPlayer()
	{
		return this.player;
	}
}
