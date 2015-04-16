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
		dirX = vars.rStickX;
		dirY = vars.rStickY;
	}
	
	// Update is called once per frame
	void Update () {
		float xOff = 0.0f;
		// Get aim directions
		float stickX = vars.rStickX;
		float stickY = vars.rStickY;

		// Keeps the aim outside the character
		if (Mathf.Abs(stickX) + Mathf.Abs(stickY) > vars.shootStickSensitivity)
		{
			dirX = stickX;
			dirY = stickY;
		}

		
		if (dirX > 0)
		{
			xOff = -0.02f;
		}
		else if (dirX < 0)
		{
			xOff = 0.02f;
		}

		gameObject.transform.localPosition = (new Vector2(dirX, dirY).normalized * 0.2f) + new Vector2(xOff, 0.0f);
	}
}
