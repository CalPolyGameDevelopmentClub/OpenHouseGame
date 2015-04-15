using UnityEngine;
using System.Collections;

public class GameMonitorScript : MonoBehaviour {

	public ArrayList activePlayers;

	// Use this for initialization
	void Start () {
		activePlayers = new ArrayList();
		for (int i = 1; i <= 4; i++)
		{
			PlayerInfo pi = new PlayerInfo();
			pi.playerNum = "P" + i.ToString();
			pi.alive = true;

			activePlayers.Add(pi);
		}

		foreach(PlayerInfo pi in activePlayers)
		{
			pi.alive = true;
		}

	}
	
	// Update is called once per frame
	void Update () {
		checkWin();
	}

	void checkWin()
	{
		PlayerInfo winner = null;
		int numAlivePlayers = 0;

		foreach (PlayerInfo pi in activePlayers)
		{
			if (pi.alive)
			{
				winner = pi;
				numAlivePlayers++;
			}
		}
		if (numAlivePlayers == 1)
		{
			winner.wins++;
			Debug.Log(winner.playerNum + " WINS!!");
			// Start new round!
		}
	}
}
