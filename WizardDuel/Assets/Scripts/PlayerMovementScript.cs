using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour {

	public float moveForce;
	public float jumpForce;
	public float fallForce;
	public float maxVelocity;

	private bool inAir;

	// Use this for initialization
	void Start () {
		moveForce = moveForce * 1000;
		jumpForce = jumpForce * 1000;
		inAir = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D> ();

		// Jumping
		if (Input.GetKeyDown(KeyCode.W) && !inAir) {
			Debug.Log("W");
			rb.AddForce(new Vector2(0.0f, jumpForce));
			inAir = true;
		}

		// Ground Movement
		if (Input.GetKey(KeyCode.A)) {
			Debug.Log("A");
			rb.AddForce(new Vector2(-1.0f * moveForce, 0.0f));
		}
		else if (Input.GetKey(KeyCode.D)) {
			Debug.Log("D");
			rb.AddForce(new Vector2(moveForce, 0.0f));
		}

		// Fastfall

		if (Input.GetKeyDown(KeyCode.S)) {
			Debug.Log("S");
			rb.AddForce(new Vector2(0.0f, -1.00f * fallForce));
		}

		if (rb.velocity.x > maxVelocity) {
			rb.velocity = new Vector2(maxVelocity, rb.velocity.y);
		}
		else if (rb.velocity.x < -1 * maxVelocity) {
			rb.velocity = new Vector2(-1 * maxVelocity, rb.velocity.y);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Platform") {
			inAir = false;
		}
	}
}
