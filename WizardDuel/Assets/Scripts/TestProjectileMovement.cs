using UnityEngine;
using System.Collections;

public class TestProjectileMovement : MonoBehaviour {

	public GameObject explosion;
	public float force;
	public Vector2 vel;
	public AudioClip explosionSound;

	private Rigidbody2D rb;
	private AudioSource audioSouce;

	// Use this for initialization
	void Start () {
		audioSouce = gameObject.GetComponent<AudioSource>();
		rb = gameObject.GetComponent<Rigidbody2D> ();
		//gameObject.GetComponent<Rigidbody2D>().AddForce(force);
		rb.velocity = vel;
		Physics2D.IgnoreCollision(explosion.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D (Collision2D coll) {
		audioSouce.PlayOneShot(explosionSound);
		GameObject boom = (GameObject)Instantiate(explosion, 
		                                          transform.position,
		                                          Quaternion.identity);
		
		boom.GetComponent<ExplosionScript>().force = force;
		
		// Destroy self
		Transform PE = transform.Find("FireballSystem");
		PE.GetComponent<ParticleSystem>().Stop();
		PE.transform.parent = null;
		Destroy(PE.gameObject, 1.0f);
		GameObject.Destroy(gameObject);
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
