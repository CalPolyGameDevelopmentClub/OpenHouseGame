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

	private static int MAX_JUMP = 2;
	
	private int jumpCount;
	private bool fastFall;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		jumpCount = 0;
		fastFall = false;
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Jumping
		if (Input.GetKeyDown(KeyCode.W) && (jumpCount < MAX_JUMP)) {
			Debug.Log("W");

			// Double jumping resets downward momentum
			if (jumpCount < 1)
			{
				rb.velocity = new Vector2 ((rb.velocity.x / moveForce) * jumpMoveForce, 0);
			}
			else
			{
				rb.velocity = new Vector2 (rb.velocity.x, 0);
			}
			rb.AddForce(new Vector2(0.0f, jumpForce) / Time.fixedDeltaTime);

			jumpCount++;
		}

		// Fastfall
		if (Input.GetKeyDown(KeyCode.S) && jumpCount > 0 && !fastFall) {
			Debug.Log("S");
			rb.velocity = new Vector2 (rb.velocity.x, 0);
			rb.AddForce(new Vector2(0.0f, -1.00f * fallForce) / Time.fixedDeltaTime);
			fastFall = true;
		}
	}

	void FixedUpdate() {
		// Ground Movement
		if (Input.GetKey(KeyCode.A)) {
			Debug.Log("A");
			if (jumpCount > 0) {
				rb.AddForce(new Vector2(-1.0f * (moveForce - jumpMoveForce), 0.0f) / Time.fixedDeltaTime);
			}
			else {
				rb.AddForce(new Vector2(-1.0f * moveForce, 0.0f) / Time.fixedDeltaTime);
			}
		}
		else if (Input.GetKey(KeyCode.D)) {
			Debug.Log("D");
			if (jumpCount > 0) {
				rb.AddForce(new Vector2((moveForce - jumpMoveForce), 0.0f) / Time.fixedDeltaTime);
			}
			else {
				rb.AddForce(new Vector2(moveForce, 0.0f) / Time.fixedDeltaTime);
			}

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
		    && jumpCount == 0) {
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

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Platform") {
			Debug.Log("Landed");
			fastFall = false;
			jumpCount = 0;
		}
	}
}
