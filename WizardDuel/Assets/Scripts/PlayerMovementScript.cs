using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour {

	public float moveForce;
	public float jumpForce;
	public float fallForce;
	public float maxVelocity;
	public float slow;
	public float jumpMoveForce;
	public float damageRatio;

	private bool inAir;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {

		inAir = false;
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		// Jumping
		if (Input.GetKey(KeyCode.W) && !inAir) {
			Debug.Log("W");
			rb.AddForce(new Vector2(0.0f, jumpForce) / Time.fixedDeltaTime);
			inAir = true;
		}
	}

	void FixedUpdate() {
		// Ground Movement
		if (Input.GetKey(KeyCode.A)) {
			Debug.Log("A");
			if (inAir) {
				rb.AddForce(new Vector2(-1.0f * (moveForce - jumpMoveForce), 0.0f) / Time.fixedDeltaTime);
			}
			else {
				rb.AddForce(new Vector2(-1.0f * moveForce, 0.0f) / Time.fixedDeltaTime);
			}
		}
		else if (Input.GetKey(KeyCode.D)) {
			Debug.Log("D");
			if (inAir) {
				rb.AddForce(new Vector2((moveForce - jumpMoveForce), 0.0f) / Time.fixedDeltaTime);
			}
			else {
				rb.AddForce(new Vector2(moveForce, 0.0f) / Time.fixedDeltaTime);
			}

		}

		// Fastfall

		if (Input.GetKeyDown(KeyCode.S)) {
			Debug.Log("S");
			rb.AddForce(new Vector2(0.0f, -1.00f * fallForce) / Time.fixedDeltaTime);
		}

		// Limiting Velocity
		if (rb.velocity.x > maxVelocity) {
			rb.velocity = new Vector2(maxVelocity, rb.velocity.y);
		}
		else if (rb.velocity.x < -1 * maxVelocity) {
			rb.velocity = new Vector2(-1 * maxVelocity, rb.velocity.y);
		}

		// Self slowdown
		if (!Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)
		    && !inAir) {
			if (rb.velocity.x > 0) {
				if (rb.velocity.x - slow > 0) {
					rb.velocity = new Vector2(rb.velocity.x - slow, rb.velocity.y);
				}
				else {
					rb.velocity = new Vector2(0, rb.velocity.y);
				}
			}
			else if (rb.velocity.x < 0){
				if (rb.velocity.x + slow < 0) {
					rb.velocity = new Vector2(rb.velocity.x + slow, rb.velocity.y);
				}
				else {
					rb.velocity = new Vector2(0, rb.velocity.y);
				}
			}
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Platform") {
			Debug.Log("Landed");
			inAir = false;
		}
	}
}
