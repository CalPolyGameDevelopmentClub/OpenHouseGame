using UnityEngine;
using System.Collections;

public class CrownScript : MonoBehaviour {
	Vector3 targPosition;
	Quaternion targAngle;
	Vector3 startPosition;
	Quaternion startAngle;
	public float lerpTime;
	// Use this for initialization
	float interp(float time)
	{
		return 1/Mathf.Pow (2.718f,time);
	}
	void Start () {
		targPosition = transform.localPosition;
		targAngle = transform.localRotation;
		startPosition = transform.localPosition + new Vector3(0,-50,0);
		startAngle = Quaternion.identity;
		transform.GetComponent<CanvasRenderer>().SetAlpha(0);
	}
	
	// Update is called once per frame
	void Update () {
		if(lerpTime < 1.0)
		{
			lerpTime+=Time.deltaTime ;
			float iter  = interp(lerpTime * 2.718f);
			transform.localPosition = Vector3.Lerp(startPosition,targPosition,iter);
			transform.localRotation = Quaternion.Slerp(startAngle,targAngle,iter);
			transform.GetComponent<CanvasRenderer>().SetAlpha(lerpTime);
		}
	}
}
