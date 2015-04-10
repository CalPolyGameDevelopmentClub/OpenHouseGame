using UnityEngine;
using System.Collections;

public class ProjectileOriginScript : MonoBehaviour {
	
	private string player;

	private float dirX;
	private float dirY;
	private PlayerVars vars;

	// Use this for initialization
	void Start () {
		vars = gameObject.GetComponent<PlayerVars>();
	}
	
	// Update is called once per frame
	void Update () {
		// Get aim directions
		float stickX = vars.rStickX;
		float stickY = vars.rStickY;

		// Keeps the aim outside the character
		if (Mathf.Abs(stickX) + Mathf.Abs(stickY) > 0.88f)
		{
			dirX = stickX;
			dirY = stickY;
		}
		vars.staff.GetComponent<Rigidbody2D>().position = new Vector2(dirX, dirY);
		//gameObject.GetComponentInChildren<Rigidbody2D>().position = new Vector2(dirX * 2, dirY * 2);
		
		if (dirX > 0)
		{
			
		}
		else if (dirX < 0)
		{
			
		}


	}
}
