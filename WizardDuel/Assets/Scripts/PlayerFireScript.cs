﻿using UnityEngine;
using System.Collections;

public class PlayerFireScript : MonoBehaviour
{
	public GameObject projectile;
	public float fireSpeed;
	public float reloadTime;

	private string player;
	private string shootTrigger = "RT";

	private Vector3 joyAim;
	private bool canShoot;
	private float reload;

	// Use this for initialization
	void Start ()
	{
		player = gameObject.GetComponent<PlayerVars>().player;

		joyAim = new Vector3(1.0f, 0.0f, 0);
		canShoot = true;
		reload = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Get aim directions
		float stickX = Input.GetAxis("RightJoystickX" + player);
		float stickY = Input.GetAxis("RightJoystickY" + player);

		// Keeps the aim outside the character
		if (Mathf.Abs(stickX) + Mathf.Abs(stickY) > 0.88f)
		{
			joyAim = new Vector3(stickX, stickY);
		}


		if (Input.GetAxis(shootTrigger + player) < -0.3 && canShoot)
		{
			Vector3 spriteSize = gameObject.GetComponent<SpriteRenderer>().bounds.size;
			Vector3 vel3D = joyAim.normalized;

			GameObject bullet = (GameObject)Instantiate(projectile, 
			                                            transform.position + vel3D * 1.6f,
			                                            Quaternion.identity);
			Debug.DrawRay(transform.position, vel3D);
			vel3D *= fireSpeed;
			bullet.GetComponent<TestProjectileMovement>().vel = new Vector2(vel3D.x,vel3D.y);
			canShoot = false;
		}
		if (!canShoot && reload < reloadTime)
		{
			reload += Time.deltaTime;
		}
		if (!canShoot && (reload >= reloadTime || Input.GetAxis(shootTrigger + player) > -0.3))
		{
			canShoot = true;
			reload = 0;
		}

		if(Input.GetMouseButtonDown(0))
		{
			Vector3 mouse = Input.mousePosition;
			Vector3 mPos = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x,mouse.y,transform.position.z));
			Vector3 vel3D = (mPos-transform.position).normalized;
			GameObject bullet = (GameObject)Instantiate(projectile,transform.position + vel3D * 5, Quaternion.identity);
			Debug.DrawRay(transform.position,vel3D);
			vel3D *= fireSpeed;
			bullet.GetComponent<TestProjectileMovement>().vel = new Vector2(vel3D.x,vel3D.y);
			
		}
	}

}
