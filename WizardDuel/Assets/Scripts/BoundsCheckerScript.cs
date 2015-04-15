using UnityEngine;
using System.Collections;

public class BoundsCheckerScript : MonoBehaviour {

	private float camSize;
	private Vector2 camPos;
	private Camera cam;
	private bool dead;
	private float starVel;

	// Use this for initialization
	void Start () {
		cam = gameObject.GetComponentInParent<Camera>();
		camSize = cam.orthographicSize;
		camPos = Camera.main.transform.position;
		starVel = 25.0f;
		dead = false;
	}
	
	// Update is called once per frame
	void Update () {
		Object[] deathXs = GameObject.FindGameObjectsWithTag("DeathX");
		Object[] players = GameObject.FindGameObjectsWithTag("Player");
		Vector2 dir = new Vector2 (0.0f, 0.0f);

		foreach (GameObject player in players)
		{
			// Off screen right
			if (player.transform.position.x > camPos.x + camSize * 2)
			{
				dir = new Vector2 (-1.0f, 0.5f);
				dead = true;
			}

			// Off screen left
			else if (player.transform.position.x < camPos.x - camSize * 2)
			{
				dir = new Vector2 (1.0f, 0.5f);
				dead = true;
			}

			// Off screen up
			else if (player.transform.position.y - player.GetComponent<Collider2D>().bounds.size.y * 2 > camPos.y + camSize)
			{
				dir = new Vector2 (0.5f, -0.5f);
				dead = true;
			}

			// Off screen down
			else if (player.transform.position.y + player.GetComponent<Collider2D>().bounds.size.y * 2 < camPos.y - camSize)
			{
				dir = new Vector2 (0.5f, 1.5f);
				dead = true;
			}

			// A player is dead
			if (dead)
			{
				GameMonitorScript gm = GameObject.FindGameObjectWithTag("GameMonitor").GetComponent<GameMonitorScript>();
				string playerNum = player.GetComponent<PlayerVars>().player;
				Debug.Log(playerNum + " IS DEAD!!");

				foreach (PlayerInfo pi in gm.activePlayers)
				{
					if (playerNum == pi.playerNum)
					{
						pi.alive = false;
					}
				}
				/*if (playerNum == "P1")
				{
					gm.p1Alive = false;
				}
				else if (playerNum == "P2")
				{
					gm.p2Alive = false;
				}
				else if (playerNum == "P3")
				{
					gm.p3Alive = false;
				}
				else if (playerNum == "P4")
				{
					gm.p4Alive = false;
				}*/

				Vector2 pos = player.transform.position;
				GameObject.Destroy(player);
				GetComponentInChildren<DeathStarsParticles>().Shoot(pos, dir, starVel,
				                                                    player.GetComponent<PlayerVars>().damageRatio);
			}

			dead = false;
		}

		Object[] stars = GameObject.FindGameObjectsWithTag("Star");
		foreach (GameObject star in stars)
		{
			if ((star.GetComponent<Rigidbody2D>().velocity.y < 0) &&
				(star.transform.position.y + star.GetComponent<Collider2D>().bounds.size.y * 2 < camPos.y - camSize))
			{
				GameObject.Destroy(star);
			}
		}
	}
}
