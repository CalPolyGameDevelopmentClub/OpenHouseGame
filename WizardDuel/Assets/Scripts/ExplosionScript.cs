using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

	public float damage;
	public float explosionTime;
	public float force;

	private float explosionTimer;

	// Use this for initialization
	void Start () {
		explosionTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		explosionTimer += Time.deltaTime;

		if (explosionTimer >= explosionTime)
		{
			GameObject.Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player") {

			// Hit the player
			coll.gameObject.GetComponent<PlayerMovementScript>().hit(-coll.contacts[0].normal, force, damage);
		}
	}
}
