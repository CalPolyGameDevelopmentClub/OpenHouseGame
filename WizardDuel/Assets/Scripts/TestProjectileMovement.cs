using UnityEngine;
using System.Collections;

public class TestProjectileMovement : MonoBehaviour {

	public Vector2 force;
	public Vector2 vel;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
		//gameObject.GetComponent<Rigidbody2D>().AddForce(force);
		rb.velocity = vel;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			Debug.Log("HERE");

			float xForce;
			float yForce;
			float playerRatio = coll.gameObject.GetComponent<PlayerMovementScript>().damageRatio;

			if (rb.velocity.x > 0.0f) {
				xForce = force.x;
			}
			else if (rb.velocity.x < 0.0f) {
				xForce = -force.x;
			}
			else {
				xForce = 0.0f;
			}

			if (rb.velocity.y > 0.0f) {
				yForce = force.y;
			}
			else if (rb.velocity.y < 0.0f) {
				yForce = -force.y;
			}
			else {
				yForce = 0.0f;
			}

			xForce *= playerRatio;
			yForce *= playerRatio;

			coll.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, yForce));
		}
	}

	/*
	void OnCollisionStay2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			Debug.Log("HITING!");
			coll.gameObject.GetComponent<Rigidbody2D>().AddForce(rb.velocity + (new Vector2(100, 100)));
		}
	}*/
}
