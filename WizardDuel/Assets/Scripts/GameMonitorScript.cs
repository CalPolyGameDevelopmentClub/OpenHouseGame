using UnityEngine;
using System.Collections;

public class GameMonitorScript : MonoBehaviour {

	public ArrayList activePlayers;
	public int numWins = 3;

	private Sprite[] startSprites;
	private bool gameOver;
	private bool gameOverOver;
	private float gameOverTimer;
	private bool gameStart;
	private float gameStartTimer;
	private PlayerInfo winner;

	private float gameOverTime = 5.0f;
	//private float gameStartTime = 3.0f;

	// Use this for initialization
	void Start () {
		activePlayers = new ArrayList();
		gameOverTimer = 0.0f;
		gameStartTimer = 3.0f;
		gameOver = false;
		gameOverOver = false;
		gameStart = false;
		loadSprites();
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameOver && !gameStart)
		{
			checkWin();
		}
		else if (gameStart)
		{

			if (gameStartTimer <= 3.0f && gameStartTimer > 2.0f)
			{
				foreach (Transform child in transform)
				{
					if (child.gameObject.name == "StartText")
					{
						child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
						child.gameObject.GetComponent<Transform>().localScale = new Vector3(6f, 6f, 6f);
						child.gameObject.GetComponent<SpriteRenderer>().sprite = startSprites[2];
					}
				}
			}

			else if (gameStartTimer <= 2.0f && gameStartTimer > 1.0f)
			{
				foreach (Transform child in transform)
				{
					if (child.gameObject.name == "StartText")
					{
						child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
						child.gameObject.GetComponent<Transform>().localScale = new Vector3(7f, 7f, 7f);
						child.gameObject.GetComponent<SpriteRenderer>().sprite = startSprites[1];
					}
				}
			}

			else if (gameStartTimer <= 1.0f && gameStartTimer > 0.0f)
			{
				foreach (Transform child in transform)
				{
					if (child.gameObject.name == "StartText")
					{
						child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
						child.gameObject.GetComponent<Transform>().localScale = new Vector3(8f, 8f, 8f);
						child.gameObject.GetComponent<SpriteRenderer>().sprite = startSprites[0];
					}
				}
			}

			else if (gameStartTimer <= 0.0f && gameStartTimer > -1.0f)
			{
				foreach (Transform child in transform)
				{
					if (child.gameObject.name == "StartText")
					{
						child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
						child.gameObject.GetComponent<Transform>().localScale = new Vector3(9f, 9f, 9f);
						child.gameObject.GetComponent<SpriteRenderer>().sprite = startSprites[3];
					}
				}
			}
			
			else if (gameStartTimer <= -1.0f)
			{
				foreach (Transform child in transform)
				{
					child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
				}
			}


			if (gameStartTimer <= -1.0f)
			{
				gameStart = false;
			}
			gameStartTimer -= Time.fixedDeltaTime;
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

	void loadSprites()
	{
		startSprites = Resources.LoadAll<Sprite>(string.Format("StartSprites"));
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

	public bool isGameStart()
	{
		return gameStart;
	}

	public void startGame()
	{
		gameStart = true;
		gameStartTimer = 3.0f;
	}
}
