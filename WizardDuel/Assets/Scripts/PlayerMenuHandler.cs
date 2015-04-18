using UnityEngine;
using System.Collections;

public class PlayerMenuHandler : MonoBehaviour {
	bool player1In;
	bool player2In;
	bool player3In;
	bool player4In;
	public bool gamePlaying;
	public GameObject player1Sprite;
	public GameObject player2Sprite;
	public GameObject player3Sprite;
	public GameObject player4Sprite;
	public AudioClip startSound;
	public AudioClip p1In;
	public AudioClip p1Out;
	public AudioClip p2In;
	public AudioClip p2Out;
	public AudioClip p3In;
	public AudioClip p3Out;
	public AudioClip p4In;
	public AudioClip p4Out;
	float offset = 40;
	float speed = 5;
	public LevelCreator creator;

	private int numPlayers;
	private AudioSource audioSource;
	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
		numPlayers = 0;
		player1In = false;
		player2In = false;
		player3In = false;
		player4In = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!gamePlaying)
		{
			if (numPlayers >= 2)
			{
				GameObject.FindGameObjectWithTag("StartPrompt").gameObject.GetComponent<SpriteRenderer>().color= new Color(1.0f, 1.0f, 1.0f, 1.0f);
			}
			else
			{
				GameObject.FindGameObjectWithTag("StartPrompt").gameObject.GetComponent<SpriteRenderer>().color= new Color(1.0f, 1.0f, 1.0f, 0.0f);
			}
			if(Input.GetButtonDown("AP1"))
			{
				if (!player1In)
				{
					audioSource.PlayOneShot(p1In);
					numPlayers++;
				}
				player1In = true;
				updatePlayer(1);
			}
			if(Input.GetButtonDown("BP1"))
			{
				if (player1In)
				{
					audioSource.PlayOneShot(p1Out);
					numPlayers--;
				}
					
				player1In = false;
				updatePlayer(1);
			}
			if(Input.GetButtonDown("AP2"))
			{
				if (!player2In)
				{
					audioSource.PlayOneShot(p2In);
					numPlayers++;
				}
				player2In = true;
				updatePlayer(2);
			}
			if(Input.GetButtonDown("BP2"))
			{
				if (player2In)
				{
					audioSource.PlayOneShot(p1Out);
					numPlayers--;
				}
				player2In = false;
				updatePlayer(2);
			}
			if(Input.GetButtonDown("AP3"))
			{
				if (!player3In)
				{
					audioSource.PlayOneShot(p3In);
					numPlayers++;
				}
				player3In = true;
				updatePlayer(3);
			}
			if(Input.GetButtonDown("BP3"))
			{
				if (player3In)
				{
					audioSource.PlayOneShot(p1Out);
					numPlayers--;
				}
				player3In = false;
				updatePlayer(3);
			}
			if(Input.GetButtonDown("AP4"))
			{
				if (!player4In)
				{
					audioSource.PlayOneShot(p4In);
					numPlayers++;
				}
				player4In = true;
				updatePlayer(4);
			}
			if(Input.GetButtonDown("BP4"))
			{
				if (player4In)
				{
					audioSource.PlayOneShot(p4Out);
					numPlayers--;
				}
				player4In = false;
				updatePlayer(4);
			}
			if(Input.GetKeyDown(KeyCode.Alpha1))
			{
				if (player1In)
				{
					audioSource.PlayOneShot(p1Out);
					numPlayers--;
				}
				else
				{
					audioSource.PlayOneShot(p1In);
					numPlayers++;
				}
					
				player1In = !player1In;
				updatePlayer(1);
			}
			if(Input.GetKeyDown(KeyCode.Alpha2))
			{
				if (player2In)
				{
					audioSource.PlayOneShot(p2Out);
					numPlayers--;
				}
				else
				{
					audioSource.PlayOneShot(p2In);
					numPlayers++;
				}
				player2In = !player2In;
				updatePlayer(2);
			}
			if(Input.GetKeyDown(KeyCode.Alpha3))
			{
				if (player3In)
				{
					audioSource.PlayOneShot(p3Out);
					numPlayers--;
				}
				else
				{
					audioSource.PlayOneShot(p3In);
					numPlayers++;
				}
				player3In = !player3In;
				updatePlayer(3);
			}
			if(Input.GetKeyDown(KeyCode.Alpha4))
			{
				if (player4In)
				{
					audioSource.PlayOneShot(p4Out);
					numPlayers--;
				}
				else
				{
					audioSource.PlayOneShot(p4In);
					numPlayers++;
				}
				player4In = !player4In;
				updatePlayer(4);
			}
			if((Input.GetKeyDown(KeyCode.Space) ||
			   Input.GetButtonDown("StartButton")) &&
			   numPlayers >= 2)
			{
				audioSource.PlayOneShot(startSound);
				PlayerInfo addPlayer;
				gamePlaying = !gamePlaying;
				creator.loadLevel(creator.getLevel(),new bool[]{player1In,player2In,player3In,player4In});
				if (player1In)
				{
					addPlayer = new PlayerInfo();
					addPlayer.alive = true;
					addPlayer.playerNum = "P1";
					GameObject.FindGameObjectWithTag("GameMonitor").gameObject.GetComponent<GameMonitorScript>().activePlayers.Add(addPlayer);
				}
				if (player2In)
				{
					addPlayer = new PlayerInfo();
					addPlayer.alive = true;
					addPlayer.playerNum = "P2";
					GameObject.FindGameObjectWithTag("GameMonitor").gameObject.GetComponent<GameMonitorScript>().activePlayers.Add(addPlayer);
				}
				if (player3In)
				{
					addPlayer = new PlayerInfo();
					addPlayer.alive = true;
					addPlayer.playerNum = "P3";
					GameObject.FindGameObjectWithTag("GameMonitor").gameObject.GetComponent<GameMonitorScript>().activePlayers.Add(addPlayer);
				}
				if (player4In)
				{
					addPlayer = new PlayerInfo();
					addPlayer.alive = true;
					addPlayer.playerNum = "P4";
					GameObject.FindGameObjectWithTag("GameMonitor").gameObject.GetComponent<GameMonitorScript>().activePlayers.Add(addPlayer);
				}
			}
		}
		if(gamePlaying && transform.position.y > -offset)
		{
			transform.Translate(new Vector3(0f,-speed,0f));
		}
		else
		{
			if(!gamePlaying && transform.localPosition.y < 0)
			{
				transform.Translate(new Vector3(0f,speed,0f));
			}

		}

	}
	void updatePlayer(int player)
	{
		if(player == 1)
		{
			player1Sprite.GetComponent<PlayerSlideScript>().playing = player1In;
		}
		else if(player == 2)
		{
			player2Sprite.GetComponent<PlayerSlideScript>().playing = player2In;
		}

		else if(player == 3)
		{
			player3Sprite.GetComponent<PlayerSlideScript>().playing = player3In;
		}

		else if(player == 4)
		{
			player4Sprite.GetComponent<PlayerSlideScript>().playing = player4In;
		}
	}
}
