using UnityEngine;
using System.Collections;

public class PlayerFireScript : MonoBehaviour
{
	public GameObject projectile;
	public float fireSpeed;

	private string player = "P1";
	private float joyX;
	private float joyY;
	private Vector3 joyAim;

	// Use this for initialization
	void Start ()
	{
		joyX = 1.0f;
		joyY = 0.0f;
		joyAim = new Vector3(joyX, joyY, 0);
	}
	
		// Update is called once per frame
	void Update ()
	{
		float stickX = Input.GetAxis("RightJoystickX" + player);
		float stickY = Input.GetAxis("RightJoystickY" + player);

		if (Mathf.Abs(stickX) + Mathf.Abs(stickY) > 0.9f)
		{
			joyX = stickX;
			joyY = stickY;
		}
		
		joyAim = new Vector3(joyX, joyY, 0);
		Debug.Log(joyAim);

		if (Input.GetAxis("LT" + player) > 0.3)
		{
			Vector3 vel3D = joyAim.normalized;

			GameObject bullet = (GameObject)Instantiate(projectile, transform.position + vel3D * 2, Quaternion.identity);
			Debug.DrawRay(transform.position, vel3D);
			vel3D *= fireSpeed;
			bullet.GetComponent<TestProjectileMovement>().vel = new Vector2(vel3D.x,vel3D.y);
		}
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 mouse = Input.mousePosition;
			Vector3 mPos = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x,mouse.y,transform.position.z));
			Vector3 vel3D = (mPos-transform.position).normalized;
			GameObject bullet = (GameObject)Instantiate(projectile,transform.position + vel3D * 5 ,Quaternion.identity);
			Debug.DrawRay(transform.position,vel3D);
			vel3D *= fireSpeed;
			bullet.GetComponent<TestProjectileMovement>().vel = new Vector2(vel3D.x,vel3D.y);
			
		}
	}

}
