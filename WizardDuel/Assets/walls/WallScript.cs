using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {

	public float breakThreshhold = 150f;
	public float forceThreshhold = 100f;
	bool isWall = true;
	// Use this for initialization
	void Start () {
		this.rigidbody2D.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D col)
	{

		Vector2 force = col.rigidbody.mass * col.rigidbody.velocity / Time.fixedDeltaTime;
		print (col.rigidbody.velocity);
		if(force.magnitude > forceThreshhold)
		{
			breakThreshhold -= force.magnitude;
			if (breakThreshhold < 0) {
				isWall = false;
				this.rigidbody2D.isKinematic = false;
				this.rigidbody2D.AddForce(new Vector2(-force.x,force.y));
				this.rigidbody2D.AddTorque(Random.Range(-25f,25f));
				this.rigidbody2D.collider2D.isTrigger=true;
			}
			else
			{
				//col.collider.rigidbody2D.AddForce(-2*force);
			}
		}
	}
}
