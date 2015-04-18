using UnityEngine;
using System.Collections;

public class GameMonitorScript : MonoBehaviour {

	public ArrayList activePlayers;
	public int numWins = 3;

	private bool gameOver;
	private bool gameOverOver;
	private float gameOverTimer;
	private PlayerInfo winner;

	private float gameOverTime = 5.0f;

	// Use this for initialization
	void Start () {
		activePlayers = new ArrayList();
		gameOverTimer = 0.0f;
		gameOver = false;
		gameOverOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameOver)
		{
			checkWin();
		}
		else 
		{
			foreach (PlayerInfo p in activePlayers)
			{
				if (p.wins >= numWins)
				{
					winner = p;
					gameOverOver = true;
				}
			}
			if (gameOverTimer >= gameOverTime)
			{
				if (gameOverOver)
				{
					Debug.Log(winner.playerNum + " WINS!!");

					foreach (PlayerInfo pl in activePlayers)
					{
						pl.wins = 0;
					}

					// Clear map
					gameOverOver = false;
					GameObject.FindGameObjectWithTag("MenuManager").gameObject.GetComponent<PlayerMenuHandler>().gamePlaying = false;
				}
				else
				{
					newGame();
				}
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
		
			Camera.main.transform.Find("Canvas").transform.Find("UI"+winner.playerNum).GetComponent<UIPlayerInfo>().addCrown();
		

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

	public bool isGameOverOver()
	{
		return gameOverOver;
	}

	public string getWinner()
	{
		return winner.playerNum;
	}
}
