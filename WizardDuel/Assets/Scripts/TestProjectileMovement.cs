using UnityEngine;
using System.Collections;

public class TestProjectileMovement : MonoBehaviour {

	public GameObject explosion;
	public float force;
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
		GameObject boom = (GameObject)Instantiate(explosion, 
		                                            transform.position,
		                                            Quaternion.identity);

		boom.GetComponent<ExplosionScript>().force = force;

		// Destroy self
		GameObject.Destroy(gameObject);
	}

	/*
	void OnCollisionStay2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			Debug.Log("HITING!");
			coll.gameObject.GetComponent<Rigidbody2D>().AddForce(rb.velocity + (new Vector2(100, 100)));
		}
	}*/
}
