using UnityEngine;
using System.Collections;

public class TestFireThings : MonoBehaviour {
	public GameObject bullet;
	public float fireX;
	public float fireY;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			GameObject clone = (GameObject)Instantiate(bullet,transform.position,Quaternion.identity);
			clone.rigidbody2D.AddForce(new Vector2(fireX,fireY)/Time.fixedDeltaTime);
		}
	}

}
