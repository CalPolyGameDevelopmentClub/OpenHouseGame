using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

	public float explosionTime;
	public float force;

	private float explosionTimer;
	private float r;
	private Vector3 scale;

	// Use this for initialization
	void Start () {
		explosionTimer = 0;
		r = (float)gameObject.GetComponent<CircleCollider2D>().radius;
		scale = gameObject.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		/*gameObject.GetComponent<CircleCollider2D>().radius = r * (explosionTimer + 1.0f);*/
		/*gameObject.transform.localScale = scale * (explosionTimer + 1.0f);*/

		explosionTimer += Time.fixedDeltaTime;

		if (explosionTimer >= explosionTime)
		{
			GameObject.Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			float dist;
			float damage;

			Debug.Log("Hitting " + coll.gameObject.GetComponent<PlayerVars>().player + "!!");

			// Calculate damge
			dist = Vector2.Distance(coll.transform.position, gameObject.transform.position);

			damage = 1.0f / dist + 0.1f;

			// Hit the player
			coll.gameObject.GetComponent<PlayerMovementScript>().hit(-coll.contacts[0].normal, force, damage * 3.0f);
		}
	}
}
