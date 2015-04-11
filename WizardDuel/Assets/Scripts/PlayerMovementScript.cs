using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour {

	public float moveForce;
	public float jumpMoveForce;
	public float maxVelocity;
	public float maxAirVelocity;
	public float slow;
	public float jumpForce;
	public float fallForce;
	public float featherForce;
	public float damageRatio;
	public float flinchTime;

	private int MAX_JUMP = 2;

	private bool canJump;
	private int jumpCount;
	private bool inAir;
	private bool fastFall;
	private bool featherFall;
	private Rigidbody2D rb;
	private bool flinch;
	private float fTimer;
	private PlayerVars vars;

	// Use this for initialization
	void Start () {
		vars = gameObject.GetComponent<PlayerVars>();

		jumpCount = 0;
		inAir = false;
		fastFall = false;
		featherFall = false;
		rb = gameObject.GetComponent<Rigidbody2D> ();
		flinch = false;
		fTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log(jumpCount);

		float lJoyX = vars.lStickX;
		RaycastHit2D airCheck = Physics2D.Raycast(
			new Vector2(rb.position.x, rb.position.y - gameObject.GetComponent<SpriteRenderer>().bounds.size.y),
			-Vector2.up);

		// Vector to check if the character is in the air
		if (airCheck.collider != null && airCheck.collider.tag == "Platform")
		{
			if (airCheck.distance > 0)
			{
				inAir = true;
			}
			if (airCheck.distance == 0)
			{
				inAir = false;
				featherFall = false;
				jumpCount = 0;

			}
		}
		else if (airCheck.collider == null)
		{
			inAir = true;
		}

		// Count up the flinch timer
		if (flinch)
		{
			if (fTimer >= flinchTime)
			{
				flinch = false;
			}
			fTimer += Time.deltaTime;
		}

		// Joystick Movement
		if (!flinch)
		{
			if (inAir) {
				// Movement in the air
				if (Mathf.Abs(rb.velocity.x + ((moveForce / jumpMoveForce) * lJoyX)) <= maxAirVelocity)
				{
					rb.velocity += new Vector2((moveForce / jumpMoveForce) * lJoyX, 0.0f);
				}
				else
				{
					if ((((moveForce / jumpMoveForce) * lJoyX) > 0 && rb.velocity.x < 0) ||
					    (((moveForce / jumpMoveForce) * lJoyX) < 0 && rb.velocity.x > 0))
					{
						rb.velocity += new Vector2((moveForce / jumpMoveForce) * lJoyX, 0.0f);
					}
				}
			}
			else
			{
				// Movement on the ground
				if (Mathf.Abs(rb.velocity.x + (moveForce * lJoyX)) <= maxVelocity)
				{
					rb.velocity += new Vector2(moveForce * lJoyX, 0.0f);
				}
				else
				{
					if (((moveForce * lJoyX) > 0 && rb.velocity.x < 0) ||
					    ((moveForce * lJoyX) < 0 && rb.velocity.x > 0))
					{
						rb.velocity += new Vector2(moveForce * lJoyX, 0.0f);
					}
				}
			}
		}

		// Jumping
		if (vars.jumpTrig > 0.3  &&  !flinch) {

			RaycastHit2D lWallCheck = Physics2D.Raycast(
				new Vector2(rb.position.x - gameObject.GetComponent<SpriteRenderer>().bounds.size.x,rb.position.y),
				Vector2.right);

			RaycastHit2D rWallCheck = Physics2D.Raycast(
				new Vector2(rb.position.x + gameObject.GetComponent<SpriteRenderer>().bounds.size.x,rb.position.y),
				-Vector2.right);

			// Double jumping resets downward momentum
			if ( jumpCount < MAX_JUMP && canJump && !inAir && rb.velocity.x <= maxVelocity)
			{
				// First jump

				if (rb.velocity.y < 0)
				{
					rb.velocity = new Vector2 (rb.velocity.x / jumpMoveForce, 0);
				}
				else
				{
					rb.velocity = new Vector2 (rb.velocity.x / jumpMoveForce, rb.velocity.y);
				}
				rb.velocity += new Vector2(0.0f, jumpForce);
				canJump = false;
				fastFall = false;
				
				jumpCount++;

			}


			else if(rWallCheck.distance == 0)
			{
				if (rb.velocity.y < 0)
				{
					rb.velocity = new Vector2 (rb.velocity.x / jumpMoveForce - jumpForce, 0);
				}
				else
				{
					rb.velocity = new Vector2 (rb.velocity.x / jumpMoveForce - jumpForce, rb.velocity.y);
				}
				rb.velocity += new Vector2(0.0f, jumpForce);
				jumpCount = 0;
				canJump = false;
				fastFall = false;
				
				jumpCount++;

			}
			else if(lWallCheck.distance == 0)
			{
				if (rb.velocity.y < 0)
				{
					rb.velocity = new Vector2 (rb.velocity.x / jumpMoveForce + jumpForce, 0);
				}
				else
				{
					rb.velocity = new Vector2 (rb.velocity.x / jumpMoveForce + jumpForce, rb.velocity.y);
				}
				rb.velocity += new Vector2(0.0f, jumpForce);
				jumpCount = 0;
				canJump = false;
				fastFall = false;
				
				jumpCount++;

			}
			else if(jumpCount < MAX_JUMP && canJump && inAir && rb.velocity.x <= maxVelocity)
			{
				// Double jump
				if (rb.velocity.y < 0)
				{
					rb.velocity = new Vector2 (rb.velocity.x, 0);
				}
				else
				{
					rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y);
				}
				rb.velocity += new Vector2(0.0f, jumpForce);
				canJump = false;
				fastFall = false;
				
				jumpCount++;

			}




		}

		// Must release some to jump again
		if (!canJump && vars.jumpTrig < 0.3) {
			canJump = true;
		}

		/*// Featherfall
		if (vars.lStickY > 0.55
		    && inAir && !featherFall)
		{
			fastFall = false;
			featherFall = true;
		}
		if (featherFall && rb.velocity.y <= 0.0f)
		{
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / featherForce);
		}*/

		// Fastfall
		if (vars.lStickY < -0.55
		    && inAir && !fastFall && !flinch)
		{
			fastFall = true;
			featherFall = false;
			if (rb.velocity.y < 0)
			{
				rb.velocity = new Vector2 (rb.velocity.x, 0);
			}
			rb.velocity += new Vector2(0.0f, -1.00f * fallForce);
		}

		// Self slowdown
		if (!inAir && (Mathf.Abs(lJoyX) < 0.1f && !flinch))
		{

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
		/*
		if (coll.gameObject.tag == "Platform") {
			fastFall = false;
			featherFall = false;
			jumpCount = 0;
		}
		*/
	}

	public void hit(Vector2 dir, float force, float damage)
	{
		rb.velocity = new Vector2(0, 0);

		// Getting hit allows you to fastfall again
		fastFall = false;

		// Apply knockback
		rb.velocity += dir * force * damageRatio;

		// Damage
		damageRatio += damage;

		// Make the character flinch
		flinch = true;
		fTimer = 0;
	}

}
