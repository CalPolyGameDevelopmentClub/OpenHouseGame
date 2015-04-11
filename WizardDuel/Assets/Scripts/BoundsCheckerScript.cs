using UnityEngine;
using System.Collections;

public class BoundsCheckerScript : MonoBehaviour {

	private float camSize;
	private Vector2 camPos;
	private Camera cam;

	// Use this for initialization
	void Start () {
		cam = gameObject.GetComponentInParent<Camera>();
		camSize = cam.orthographicSize;
		camPos = Camera.main.transform.position; /*gameObject.GetComponentInParent<Transform>().position;*/
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(camPos);

		Object[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players)
		{
			if ((player.transform.position.x > camPos.x + camSize * 2) || 
			    (player.transform.position.x < camPos.x - camSize * 2) ||
			    (player.transform.position.y - player.GetComponent<Collider2D>().bounds.size.y * 2 > camPos.y + camSize) ||
			    (player.transform.position.y + player.GetComponent<Collider2D>().bounds.size.y * 2 < camPos.y - camSize))
			{
				Debug.Log(player.GetComponent<PlayerVars>().player + " IS DEAD!!");
			}
		}
	}
}
