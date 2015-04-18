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
	public float flinchTime;
	public AudioClip jump1Sound;
	public AudioClip jump2Sound;
	public AudioClip hurtSound;

	private int MAX_JUMP = 2;

	private bool canJump;
	private int jumpCount;
	private bool inAir;
	private bool fastFall;
	private Rigidbody2D rb;
	private bool flinch;
	private float fTimer;
	private PlayerVars vars;
	private Animator animator;
	private bool isHit;
	private bool slowMo;
	private AudioSource audioSouce;

	private GameMonitorScript gm;
	
	// Use this for initialization
	void Start () {
		gm = GameObject.FindGameObjectWithTag("GameMonitor").gameObject.GetComponent<GameMonitorScript>();
		audioSouce = gameObject.GetComponent<AudioSource>();
		vars = gameObject.GetComponent<PlayerVars>();
		rb = gameObject.GetComponent<Rigidbody2D> ();

		jumpCount = 0;
		inAir = false;
		fastFall = false;

		flinch = false;
		fTimer = 0;
		animator = gameObject.GetComponent<Animator>();
		//switch(vars.player) {
		//case "P1":
		//	animator.runtimeAnimatorController.set("Assets");
		//}
		isHit = false;
		slowMo = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (!gm.isGameOver() && !gm.isGameStart())
		{
			slowMo = false;
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
				else if (airCheck.distance <= 0 && rb.velocity.y <= 0)
				{
					inAir = false;
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
			if (vars.jumpTrig > 0.3  && canJump && !flinch) {
				
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
						audioSouce.PlayOneShot(jump1Sound);
;						rb.velocity = new Vector2 (rb.velocity.x / jumpMoveForce, 0);
					}
					else
					{
						audioSouce.PlayOneShot(jump1Sound);
						rb.velocity = new Vector2 (rb.velocity.x / jumpMoveForce, rb.velocity.y);
					}
					rb.velocity += new Vector2(0.0f, jumpForce);
					canJump = false;
					fastFall = false;
					
					jumpCount++;
				}
				
				// Right wall jump
				else if(rWallCheck.distance == 0 && rWallCheck.collider.tag == "Platform")
				{
					if (rb.velocity.y < 0)
					{
						rb.velocity = new Vector2 (rb.velocity.x / jumpMoveForce - jumpForce / 1.5f, 0);
					}
					else
					{
						rb.velocity = new Vector2 (rb.velocity.x / jumpMoveForce - jumpForce / 1.5f, rb.velocity.y);
					}
					audioSouce.PlayOneShot(jump1Sound);
					rb.velocity += new Vector2(0.0f, jumpForce);
					jumpCount = 0;
					canJump = false;
					fastFall = false;
					
					jumpCount++;
				}
				// Left wall jump
				else if(lWallCheck.distance == 0  && lWallCheck.collider.tag == "Platform")
				{
					if (rb.velocity.y < 0)
					{
						rb.velocity = new Vector2 (rb.velocity.x / jumpMoveForce + jumpForce / 1.5f, 0);
					}
					else
					{
						rb.velocity = new Vector2 (rb.velocity.x / jumpMoveForce + jumpForce / 1.5f, rb.velocity.y);
					}
					audioSouce.PlayOneShot(jump1Sound);
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
						if (jumpCount == 0)
							audioSouce.PlayOneShot(jump1Sound);
						if (jumpCount > 0)
							audioSouce.PlayOneShot(jump2Sound);
						rb.velocity = new Vector2 (rb.velocity.x, 0);
					}
					else
					{
						audioSouce.PlayOneShot(jump2Sound);
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
				
			// Fastfall
			if (vars.lStickY < -0.55
			    && inAir && !fastFall && !flinch)
			{
				fastFall = true;
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
			animate();
		}

		// Game over
		else
		{
			if (!slowMo)
			{
				rb.velocity = new Vector2(rb.velocity.x / 5.0f, rb.velocity.y / 2.0f);
				slowMo = true;
			}
			else
			{
				rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 1.5f);
			}
		}

	}

	void FixedUpdate()
	{
		isHit = false;
	}

	public void animate() {
		//0 Is right idle
		//1 is left idle
		//2 is right walk
		//3 is left walk
		//4 is r jump
		//5 is l jump
		//6 is r fall
		//7 is l fall

		int rand = animator.GetInteger("Direction");
		if (inAir) {
			if(rb.velocity.y > 0){
				if(rb.velocity.x > 0) {
					animator.SetInteger ("Direction", 4);
				}
				else 
					animator.SetInteger ("Direction", 5);
			}
			else {
				if(rb.velocity.x > 0) {
					animator.SetInteger ("Direction", 6);
				}
				else 
					animator.SetInteger ("Direction", 7);
			}
		}
		else if (vars.lStickX == 0) {
			if(rand == 2 || rand == 0) {
				animator.SetInteger ("Direction", 0);
			}
			else if(rand == 3 || rand == 1) {
				animator.SetInteger ("Direction", 1);
			}
		}
		else {
			if(vars.lStickX > 0.0f) {
				animator.SetInteger ("Direction", 2);
			}
			else if(vars.lStickX < 0.0f) {
				animator.SetInteger ("Direction", 3);
			}
		}

	}

	public void hit(Vector2 dir, float force, float damage)
	{
		if (!isHit)
		{
			audioSouce.PlayOneShot(hurtSound);
			isHit = true;
			rb.velocity = new Vector2(0, 0);
			
			// Getting hit allows you to fastfall again
			fastFall = false;
			
			// Apply knockback
			rb.velocity += dir * force * Mathf.Sqrt(vars.damageRatio * 4.0f);
			
			// Damage
			vars.damageRatio += damage;
			
			// Make the character flinch
			flinch = true;
			fTimer = 0;
		}
	}
	public bool isFlinching()
	{
		return flinch;
	}

}
