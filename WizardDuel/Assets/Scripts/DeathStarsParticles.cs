using UnityEngine;
using System.Collections;

public class DeathStarsParticles : MonoBehaviour {

	public GameObject particle;

	private int numStars = 4;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Shoot(Vector2 pos, Vector2 dir, float vel, float ratio)
	{
		for (int i = 0; i < numStars * (int)ratio + numStars; i++)
		{
			GameObject star = (GameObject)Instantiate(particle, 
			                                          pos,
			                                          Quaternion.identity);
			star.GetComponent<Rigidbody2D>().velocity = 
				new Vector2(dir.x * vel * Random.Range(-1.5f, 1.5f), dir.y * vel * Random.Range(-1.5f, 1.5f));
		}
	}
}
