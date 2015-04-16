using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameOverTextScript : MonoBehaviour {

	private GameMonitorScript gm;

	// Use this for initialization
	void Start () {
		gm = GameObject.FindGameObjectWithTag("GameMonitor").gameObject.GetComponent<GameMonitorScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gm.isGameOver())
		{
			string winner = gm.getWinner();
			gameObject.GetComponent<Text>().text = winner + " WINS!!";
		}
		else
		{
			gameObject.GetComponent<Text>().text = "";
		}
	}
}
