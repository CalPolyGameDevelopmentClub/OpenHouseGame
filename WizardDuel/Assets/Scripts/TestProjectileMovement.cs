using UnityEngine;
using System.Collections;

public class TestProjectileMovement : MonoBehaviour {

	public Vector2 force;
	public Vector2 vel;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
		//gameObject.GetComponent<Rigidbody2D>().AddForce(force);
		rb.velocity = vel;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player") {

			float playerRatio = coll.gameObject.GetComponent<PlayerMovementScript>().damageRatio;



			Debug.Log(coll.contacts[0].normal * playerRatio);
			GameObject.Destroy(gameObject);
			coll.gameObject.GetComponent<Rigidbody2D>().AddForce(100 * -coll.contacts[0].normal * playerRatio / Time.fixedDeltaTime);

		}
	}

	/*
	void OnCollisionStay2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			Debug.Log("HITING!");
			coll.gameObject.GetComponent<Rigidbody2D>().AddForce(rb.velocity + (new Vector2(100, 100)));
		}
	}*/
}
