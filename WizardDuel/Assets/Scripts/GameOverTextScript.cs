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
			Text text = gameObject.GetComponent<Text>();

			string winner = gm.getWinner();

			if (winner == "P1")
			{
				text.color = new Color(0.32f, 0.74f, 0.74f);
			}
			else if (winner == "P2")
			{
				text.color = new Color(0.75f, 0.21f, 0.21f);
			}
			else if (winner == "P3")
			{
				text.color = new Color(0.63f, 0.66f, 0.19f);
			}
			else if (winner == "P4")
			{
				text.color = new Color(0.50f, 0.28f, 0.67f);
			}
			else
			{
				text.color = new Color(1.0f, 1.0f, 1.0f);
			}
			text.text = winner + " WINS!!";
		}
		else
		{
			gameObject.GetComponent<Text>().text = "";
		}
	}
}
