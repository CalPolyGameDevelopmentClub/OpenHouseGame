using UnityEngine;
using System.Collections;

public class PlayerSlideScript : MonoBehaviour {
	public bool playing = false;
	private float lerp = 0;
	public float speed = 0.2f;
	public int offset = 1;
	private Vector3 startPos;
	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(playing && lerp < 1.0)
		{
			lerp += speed;
		}
		else if(!playing && lerp > 0)
		{
			lerp -= speed;
		}
		transform.position = new Vector3(startPos.x - offset*(1.0f-lerp),startPos.y,startPos.z);
		                           
	}
	void setPlaying(bool playing)
	{
		playing = true;
	}
}
