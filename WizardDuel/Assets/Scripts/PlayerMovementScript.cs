using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour {

	public float moveForce;
	public float jumpForce;
	public float fallForce;
	public float featherForce;
	public float maxVelocity;
	public float slow;
	public float jumpMoveForce;
	public float damageRatio;

	private static int MAX_JUMP = 2;
	private static string player = "P1";
	private static string jumpTrigger = "LT";

	private bool canJump;
	private int jumpCount;
	private bool fastFall;
	private bool featherFall;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		jumpCount = 0;
		fastFall = false;
		featherFall = false;
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		float lJoy = Input.GetAxis("LeftJoystickX" + player);

		// Joystick Movement
		if (jumpCount > 0) {
			// Movement in the air

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
			// Movement on the ground

			if (Mathf.Abs(rb.velocity.x + (moveForce * lJoy)) < maxVelocity)
			{
				rb.velocity += new Vector2(moveForce * lJoy, 0.0f);
			}
			else
			{
				rb.velocity = new Vector2(maxVelocity * lJoy, rb.velocity.y);
			}
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
			
			rb.velocity += new Vector2(0.0f, jumpForce);
			
			jumpCount++;
		}

		// Must release some to jump again
		if (!canJump && Input.GetAxis(jumpTrigger + player) < 0.3) {
			canJump = true;
		}

		// Featherfall
		if (Input.GetAxis("LeftJoystickY" + player) > 0.55
		    && jumpCount >= 1 && !featherFall)
		{
			fastFall = false;
			featherFall = true;
		}
		if (featherFall && rb.velocity.y <= 0.0f)
		{
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / featherForce);
		}
		// Fastfall
		if (Input.GetAxis("LeftJoystickY" + player) < -0.55
		    && jumpCount >= 1 && !fastFall)
		{
			featherFall = false;
			rb.velocity = new Vector2 (rb.velocity.x, 0);
			rb.velocity += new Vector2(0.0f, -1.00f * fallForce);
			fastFall = true;
		}

		// Self slowdown
		if (jumpCount == 0) {
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
			featherFall = false;
			jumpCount = 0;
		}
	}
}
