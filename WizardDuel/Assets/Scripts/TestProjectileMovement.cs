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
		Physics2D.IgnoreCollision(explosion.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Explosion")
		{
			Debug.Log("IGNORE ME!");

		}
		else
		{
			GameObject boom = (GameObject)Instantiate(explosion, 
			                                          transform.position,
			                                          Quaternion.identity);
			
			boom.GetComponent<ExplosionScript>().force = force;
			
			// Destroy self
			GameObject.Destroy(gameObject);
		}
	}
	/*void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Explosion")
		{
			Physics2D.IgnoreCollision(coll.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
		}
		else
		{
			GameObject boom = (GameObject)Instantiate(explosion, 
			                                          transform.position,
			                                          Quaternion.identity);
			
			boom.GetComponent<ExplosionScript>().force = force;
			
			// Destroy self
			GameObject.Destroy(gameObject);
		}
	}*/
}
