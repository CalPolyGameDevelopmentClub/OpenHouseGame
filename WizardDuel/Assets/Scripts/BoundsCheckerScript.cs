using UnityEngine;
using System.Collections;

public class BoundsCheckerScript : MonoBehaviour {
	public AudioClip deathSound;

	private float camSize;
	private Vector2 camPos;
	private Camera cam;
	private bool dead;
	private float starVel;
	private GameMonitorScript gm;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
		cam = gameObject.GetComponentInParent<Camera>();
		camSize = cam.orthographicSize;
		camPos = Camera.main.transform.position;
		starVel = 25.0f;
		dead = false;
		gm = GameObject.FindGameObjectWithTag("GameMonitor").GetComponent<GameMonitorScript>();
	}
	
	// Update is called once per frame
	void Update () {
		Object[] deathXs = GameObject.FindGameObjectsWithTag("DeathX");
		Object[] players = GameObject.FindGameObjectsWithTag("Player");
		Vector2 dir = new Vector2 (0.0f, 0.0f);

		if (!gm.isGameOver())
		{
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
					
					string playerNum = player.GetComponent<PlayerVars>().player;
					Debug.Log(playerNum + " IS DEAD!!");
					
					foreach (PlayerInfo pi in gm.activePlayers)
					{
						if (playerNum == pi.playerNum && pi.alive)
						{
							pi.alive = false;
							Vector2 pos = player.transform.position;
							audioSource.PlayOneShot(deathSound);
							GetComponentInChildren<DeathStarsParticles>().Shoot(pos, dir, starVel,
							                                                    player.GetComponent<PlayerVars>().damageRatio);
							GameObject.Destroy(player);
						}
					}
					
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
			
			Object[] balls = GameObject.FindGameObjectsWithTag("FireBall");
			foreach (GameObject ball in balls)
			{
				if ((ball.transform.position.x > camPos.x + camSize * 2) ||
				    (ball.transform.position.y + ball.GetComponent<Collider2D>().bounds.size.y * 2 < camPos.y - camSize) ||
				    (ball.transform.position.x < camPos.x - camSize * 2) ||
				    (ball.transform.position.y - ball.GetComponent<Collider2D>().bounds.size.y * 2 > camPos.y + camSize))
				{
					GameObject.Destroy(ball);
				}
			}

			Object[] walls = GameObject.FindGameObjectsWithTag("Platform");
			foreach (GameObject wall in walls)
			{
				if ((wall.transform.position.x > camPos.x + camSize * 2) ||
				    (wall.transform.position.y + wall.GetComponent<Collider2D>().bounds.size.y * 2 < camPos.y - camSize) ||
				    (wall.transform.position.x < camPos.x - camSize * 2) ||
				    (wall.transform.position.y - wall.GetComponent<Collider2D>().bounds.size.y * 2 > camPos.y + camSize))
				{
					GameObject.Destroy(wall);
				}
			}
		}
	}
}
