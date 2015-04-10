using UnityEngine;
using System.Collections;

public class ProjectileOriginScript : MonoBehaviour {
	
	private string player;

	private float dirX;
	private float dirY;

	// Use this for initialization
	void Start () {
		player = gameObject.GetComponent<PlayerVars>().player;
	}
	
	// Update is called once per frame
	void Update () {
		// Get aim directions
		float stickX = Input.GetAxis("RightJoystickX" + player);
		float stickY = Input.GetAxis("RightJoystickY" + player);

		// Keeps the aim outside the character
		if (Mathf.Abs(stickX) + Mathf.Abs(stickY) > 0.88f)
		{
			dirX = stickX;
			dirY = stickY;
		}
		gameObject.GetComponent<PlayerVars>().staff.GetComponent<Rigidbody2D>().position = new Vector2(dirX, dirY);
		/*gameObject.GetComponentInChildren<Rigidbody2D>().position = new Vector2(dirX, dirY);*/
		
		if (dirX > 0)
		{
			
		}
		else if (dirX < 0)
		{
			
		}


	}
}
