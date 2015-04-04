using UnityEngine;
using System.Collections;

public class PlayerFireScript : MonoBehaviour
{
	public GameObject projectile;
	public float fireSpeed;
	// Use this for initialization
	void Start ()
	{
	
	}
	
		// Update is called once per frame
	void Update ()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 mouse = Input.mousePosition;
			Vector3 mPos = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x,mouse.y,transform.position.z));
			Vector3 vel3D = (mPos-transform.position).normalized;
			GameObject bullet = (GameObject)Instantiate(projectile,transform.position + vel3D * 3 ,Quaternion.identity);
			Debug.DrawRay(transform.position,vel3D);
			vel3D *= fireSpeed;
			bullet.GetComponent<TestProjectileMovement>().vel = new Vector2(vel3D.x,vel3D.y);
			
		}
	}

}
