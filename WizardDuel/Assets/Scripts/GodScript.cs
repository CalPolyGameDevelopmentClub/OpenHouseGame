using UnityEngine;
using System.Collections;

public class GodScript : MonoBehaviour {

	float time = 1.0f;
	// Use this for initialization
	void Start () {
		transform.GetComponent<CanvasRenderer>().SetAlpha(0);

	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate(new Vector3(0,0,1),10f*time);
		time-=Time.deltaTime;
		transform.GetComponent<CanvasRenderer>().SetAlpha(0.75f * Mathf.Abs (1-Mathf.Pow ((2*time-1),2)));

		if(time < 0)
		{
			Destroy(gameObject);
		}
	}
}
