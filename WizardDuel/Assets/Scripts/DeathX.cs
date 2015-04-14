using UnityEngine;
using System.Collections;

public class DeathX : MonoBehaviour {

	private string player;

	// Use this for initialization
	void Start () {
		player = GetComponentInParent<UIPlayerInfo>().player;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string getPlayer()
	{
		return this.player;
	}
}
