using UnityEngine;
using System.Collections;

public class PlayerFireScript : MonoBehaviour
{
	public GameObject projectile;
	public float fireSpeed;
	public float reloadTime;
	public AudioClip shootSound;

	private Vector3 joyAim;
	private bool canShoot;
	private float reload;
	private PlayerVars vars;
	private AudioSource audioSource;

	private GameMonitorScript gm;

	// Use this for initialization
	void Start ()
	{
		gm = GameObject.FindGameObjectWithTag("GameMonitor").gameObject.GetComponent<GameMonitorScript>();
		audioSource = gameObject.GetComponent<AudioSource>();
		vars = gameObject.GetComponent<PlayerVars>();

		joyAim = new Vector3(1.0f, 0.0f, 0);
		canShoot = true;
		reload = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!gm.isGameOver())
		{
			// Get aim directions
			float stickX = vars.rStickX;
			float stickY = vars.rStickY;
			
			// Keeps the aim outside the character
			if (Mathf.Abs(stickX) + Mathf.Abs(stickY) > vars.shootStickSensitivity)
			{
				joyAim = new Vector3(stickX, stickY);
			}
			
			if (vars.shootTrig > 0.3 && canShoot)
			{
				audioSource.PlayOneShot(shootSound);
				Vector3 childPos = this.transform.GetChild(0).transform.localPosition;
				Vector3 vel3D = joyAim.normalized;
				
				GameObject bullet = (GameObject)Instantiate(projectile, 
				                                            transform.position + childPos * 13.0f,
				                                            Quaternion.identity);
				vel3D *= fireSpeed;
				bullet.GetComponent<TestProjectileMovement>().vel = new Vector2(vel3D.x,vel3D.y);
				canShoot = false;
			}
			if (!canShoot && reload < reloadTime)
			{
				reload += Time.deltaTime;
			}
			if (!canShoot && (reload >= reloadTime/* || vars.shootTrig < 0.3)*/))
			{
				canShoot = true;
				reload = 0;
			}
			
			/*if(Input.GetMouseButtonDown(0))
			{
				Vector3 mouse = Input.mousePosition;
				Vector3 mPos = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x,mouse.y,transform.position.z));
				Vector3 vel3D = (mPos-transform.position).normalized;
				GameObject bullet = (GameObject)Instantiate(projectile,transform.position + vel3D * 5, Quaternion.identity);
				Debug.DrawRay(transform.position,vel3D);
				vel3D *= fireSpeed;
				bullet.GetComponent<TestProjectileMovement>().vel = new Vector2(vel3D.x,vel3D.y);
				
			}*/
		}
	}

}
