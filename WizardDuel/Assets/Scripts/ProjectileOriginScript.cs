using UnityEngine;
using System.Collections;

public class ProjectileOriginScript : MonoBehaviour {
	
	private string player;

	private float dirX;
	private float dirY;
	private PlayerVars vars;

	// Use this for initialization
	void Start () {
		vars = gameObject.GetComponentInParent<PlayerVars>();
	}
	
	// Update is called once per frame
	void Update () {
		// Get aim directions
		float stickX = vars.rStickX;
		float stickY = vars.rStickY;

		// Keeps the aim outside the character
		if (Mathf.Abs(stickX) + Mathf.Abs(stickY) > vars.shootStickSensitivity)
		{
			dirX = stickX;
			dirY = stickY;
		}
		gameObject.transform.localPosition = new Vector2(dirX, dirY).normalized * 0.2f;
		
		if (dirX > 0)
		{
			
		}
		else if (dirX < 0)
		{
			
		}


	}
}
