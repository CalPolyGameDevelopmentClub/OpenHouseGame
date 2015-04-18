using UnityEngine;
using System.Collections;

public class PressStartAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3(0,-0.45f * ((int)(Time.fixedTime*2)%2),-1);
	}
}
