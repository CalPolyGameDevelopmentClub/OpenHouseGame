using UnityEngine;
using System.Collections;

public class PlayerMenuHandler : MonoBehaviour {
	bool player1In;
	bool player2In;
	bool player3In;
	bool player4In;
	bool gamePlaying;
	public GameObject player1Sprite;
	public GameObject player2Sprite;
	public GameObject player3Sprite;
	public GameObject player4Sprite;
	float offset = 40;
	float speed = 5;
	public LevelCreator creator;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!gamePlaying)
		{
			if(Input.GetButtonDown("AP1"))
			{
				player1In = true;
				updatePlayer(1);
			}
			if(Input.GetButtonDown("BP1"))
			{
				player1In = false;
				updatePlayer(1);
			}
			if(Input.GetButtonDown("AP2"))
			{
				player2In = true;
				updatePlayer(2);
			}
			if(Input.GetButtonDown("BP2"))
			{
				player2In = false;
				updatePlayer(2);
			}
			if(Input.GetButtonDown("AP3"))
			{
				player3In = true;
				updatePlayer(3);
			}
			if(Input.GetButtonDown("BP3"))
			{
				player3In = false;
				updatePlayer(3);
			}
			if(Input.GetButtonDown("AP4"))
			{
				player4In = true;
				updatePlayer(4);
			}
			if(Input.GetButtonDown("BP4"))
			{
				player4In = false;
				updatePlayer(4);
			}
			if(Input.GetKeyDown(KeyCode.Alpha1))
			{
				player1In = !player1In;
				updatePlayer(1);
			}
			if(Input.GetKeyDown(KeyCode.Alpha2))
			{
				player2In = !player2In;
				updatePlayer(2);
			}
			if(Input.GetKeyDown(KeyCode.Alpha3))
			{
				player3In = !player3In;
				updatePlayer(3);
			}
			if(Input.GetKeyDown(KeyCode.Alpha4))
			{
				player4In = !player4In;
				updatePlayer(4);
			}
			if(Input.GetKeyDown(KeyCode.Space))
			{
				gamePlaying = !gamePlaying;
				creator.loadLevel(creator.testLevel,new bool[]{player1In,player2In,player3In,player4In});
			}
		}
		if(gamePlaying && transform.position.y > -offset)
		{
			transform.Translate(new Vector3(0f,-speed,0f));
		}
		else if(!gamePlaying && transform.position.y < 0)
		{
			transform.Translate(new Vector3(0f,speed,0f));
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
