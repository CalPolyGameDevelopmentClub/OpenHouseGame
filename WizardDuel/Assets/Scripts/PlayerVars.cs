using UnityEngine;
using System.Collections;

public class PlayerVars : MonoBehaviour {

	public string player;
	public GameObject staff;

	public float lStickX;
	public float lStickY;

	public float rStickX;
	public float rStickY;
	public float shootStickSensitivity = 0.88f;

	public float jumpTrig;
	public float shootTrig;

	public string shoot = "RT";
	public string jump = "LT";

	public float damageRatio = 0.0f;

	void Update()
	{
		lStickX = Input.GetAxis("LeftJoystickX" + player);
		lStickY = Input.GetAxis("LeftJoystickY" + player);

		rStickX = Input.GetAxis("RightJoystickX" + player);
		rStickY = Input.GetAxis("RightJoystickY" + player);

		if(Input.GetKey(KeyCode.A))
		{
			lStickX = -1;
		}
		else if(Input.GetKey(KeyCode.D))
		{
			lStickX = 1;
		}


		jumpTrig = Input.GetAxis(jump + player);
		shootTrig = Input.GetAxis(shoot + player);
		if(Input.GetKeyDown(KeyCode.Space))
		{
			jumpTrig = 0.5f;
		}
	}
}
