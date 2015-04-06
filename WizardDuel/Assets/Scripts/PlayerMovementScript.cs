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

	private bool canJump;
	private int jumpCount;
	private bool fastFall;
	private Rigidbody2D rb;
	private string player = "P1";
	private string jumpTrigger = "LT";

	// Use this for initialization
	void Start () {
		jumpCount = 0;
		fastFall = false;
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Joystick Movement

		float lJoy = Input.GetAxis("LeftJoystickX" + player);

		if (jumpCount > 0) {
			if (Mathf.Abs(rb.velocity.x + ((moveForce / jumpMoveForce) * lJoy)) < maxVelocity)
			{
				rb.velocity += new Vector2((moveForce / jumpMoveForce) * lJoy, 0.0f);
			}
			else
			{
				rb.velocity = new Vector2(maxVelocity * lJoy, rb.velocity.y);
			}
		}
		else
		{
			if (Mathf.Abs(rb.velocity.x + (moveForce * lJoy)) < maxVelocity)
			{
				rb.velocity += new Vector2(moveForce * lJoy, 0.0f);
			}
			else
			{
				rb.velocity = new Vector2(maxVelocity * lJoy, rb.velocity.y);
			}
		}

		/// Up causes "feather fall", set flag, divide Y Vel by some value 
		/*if (Input.GetAxis("LeftJoystickY" + player) > 0.55 && jumpCount < MAX_JUMP)
		{
			rb.AddForce(new Vector2(0.0f, jumpForce) / Time.fixedDeltaTime);
			jumpCount++;
		}*/
		if (Input.GetAxis("LeftJoystickY" + player) < -0.55 && !fastFall)
		{
			rb.velocity = new Vector2 (rb.velocity.x, 0);
			rb.AddForce(new Vector2(0.0f, -1.00f * fallForce) / Time.fixedDeltaTime);
			fastFall = true;
		}

		// Jumping
		if (Input.GetAxis(jumpTrigger + player) > 0.3  && (jumpCount < MAX_JUMP) && canJump) {
			canJump = false;
			fastFall = false;
			Debug.Log("Jump!");
			// Double jumping resets downward momentum
			if (jumpCount < 1)
			{
				// First jump
				
				rb.velocity = new Vector2 ((rb.velocity.x / 15) * jumpMoveForce, 0);
			}
			else
			{
				// Double jump
				
				rb.velocity = new Vector2 (rb.velocity.x, 0);
			}
			
			rb.AddForce(new Vector2(0.0f, jumpForce) / Time.fixedDeltaTime);
			
			jumpCount++;
		}

		if (!canJump && Input.GetAxis(jumpTrigger + player) < 0.3) {
			canJump = true;
		}

		if (Input.GetKeyDown(KeyCode.W) && (jumpCount < MAX_JUMP)) {
			Debug.Log("W");

			// Double jumping resets downward momentum
			if (jumpCount < 1)
			{
				// First jump

				rb.velocity = new Vector2 ((rb.velocity.x / moveForce) * jumpMoveForce, 0);
			}
			else
			{
				// Double jump

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
