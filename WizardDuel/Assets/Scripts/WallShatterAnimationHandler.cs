using UnityEngine;
using System.Collections;

public class WallShatterAnimationHandler : MonoBehaviour {

	static Sprite[] frames;
	public int damage = 0;

	// Use this for initialization
	void Start () {
		frames = Resources.LoadAll<Sprite>(string.Format("Break Mask"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void setDamage(int damage)
	{
		this.GetComponent<SpriteRenderer>().sprite=frames[damage];
	}
}
