using UnityEngine;
using System.Collections;

public class ExplosionParticleSpawner : MonoBehaviour {
	public GameObject explosionSprite;
	int numParticles = 30;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < numParticles; i++){
			GameObject obj = (GameObject)Instantiate(explosionSprite,transform.position,Quaternion.AngleAxis(Random.Range(0,360),new Vector3(0,0,1)));
			obj.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(Random.Range(1,5),0),ForceMode2D.Impulse);
		}
		Destroy(this,1.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
