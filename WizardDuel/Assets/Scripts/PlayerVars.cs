using UnityEngine;
using System.Collections;

public class PlayerVars : MonoBehaviour {

	public string player;
	public GameObject staff;

	public float lStickX;
	public float lStickY;

	public float rStickX;
	public float rStickY;

	public float lTrig;
	public float rTrig;

	void Update()
	{
		lStickX = Input.GetAxis("LeftJoystickX" + player);
		lStickY = Input.GetAxis("LeftJoystickY" + player);

		rStickX = Input.GetAxis("RightJoystickX" + player);
		rStickY = Input.GetAxis("RightJoystickY" + player);

		lTrig = Input.GetAxis("LT" + player);
		rTrig = Input.GetAxis("RT" + player);

		Debug.Log("L" + lTrig + " R" + rTrig);
	}
}
