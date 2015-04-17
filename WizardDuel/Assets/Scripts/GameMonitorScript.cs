using UnityEngine;
using System.Collections;

public class GameMonitorScript : MonoBehaviour {

	public ArrayList activePlayers;

	private bool gameOver;
	private float gameOverTimer;
	private PlayerInfo winner;

	private float gameOverTime = 5.0f;

	// Use this for initialization
	void Start () {
		activePlayers = new ArrayList();
		gameOverTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameOver)
		{
			checkWin();
		}
		else 
		{
			if (gameOverTimer >= gameOverTime)
			{
				newGame();
			}
			gameOverTimer += Time.fixedDeltaTime;
		}
	}

	void checkWin()
	{
		winner = null;
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
			gameOver = true;
		}
	}

	void newGame()
	{
		Debug.Log("New Game!");
		foreach(PlayerInfo pi in activePlayers)
		{
			pi.alive = true;
		}

		GameObject lc = GameObject.FindGameObjectWithTag("LevelCreator");
		lc.gameObject.GetComponent<LevelCreator>().reset();

		gameOverTimer = 0.0f;
		gameOver = false;
	}

	public bool isGameOver()
	{
		return gameOver;
	}

	public string getWinner()
	{
		return winner.playerNum;
	}
}
