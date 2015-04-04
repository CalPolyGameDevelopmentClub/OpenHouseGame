using UnityEngine;
using System.Collections;

public class TestProjectileExplostion : MonoBehaviour {
	
	public Vector2 force;
	public float radius;
	
	
	// Use this for initialization
	void Start () {
		GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
		foreach ( GameObject player in playerList ) {
			float dis = Vector2.Distance(this.rigidbody2D.position, player.rigidbody2D.position);
			float forcePower = dis/this.radius;
			if (0 < forcePower && forcePower < 1) {
				float xForce = (Mathf.Pow((1 - dis), 4))*315;
				float yForce = (Mathf.Pow((1 - dis), 4))*315;
				
				player.rigidbody2D.AddForce(new Vector2(xForce, yForce));
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

// 1-(x^4)