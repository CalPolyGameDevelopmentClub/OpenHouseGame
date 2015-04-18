using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameOverTextScript : MonoBehaviour {

	public Sprite RoundOverP1;
	public Sprite RoundOverP2;
	public Sprite RoundOverP3;
	public Sprite RoundOverP4;

	public Sprite WinnerP1;
	public Sprite WinnerP2;
	public Sprite WinnerP3;
	public Sprite WinnerP4;

	private GameMonitorScript gm;
	private Text text;
	private string winner;
	private SpriteRenderer sp;

	// Use this for initialization
	void Start () {
		sp = gameObject.GetComponent<SpriteRenderer>();
		text = gameObject.GetComponent<Text>();
		gm = GameObject.FindGameObjectWithTag("GameMonitor").gameObject.GetComponent<GameMonitorScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gm.isGameOver() || gm.isGameOverOver())
		{
			winner = gm.getWinner();
		}
		if (gm.isGameOverOver())
		{
			if (winner == "P1")
			{
				sp.sprite = WinnerP1;
			}
			else if (winner == "P2")
			{
				sp.sprite = WinnerP2;
			}
			else if (winner == "P3")
			{
				sp.sprite = WinnerP3;
			}
			else if (winner == "P4")
			{
				sp.sprite = WinnerP4;
			}
			else
			{
				sp.sprite = WinnerP1;
			}
			sp.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}
		else if (gm.isGameOver())
		{
			if (winner == "P1")
			{
				sp.sprite = RoundOverP1;
			}
			else if (winner == "P2")
			{
				sp.sprite = RoundOverP2;
			}
			else if (winner == "P3")
			{
				sp.sprite = RoundOverP3;
			}
			else if (winner == "P4")
			{
				sp.sprite = RoundOverP4;
			}
			else
			{
				sp.sprite = RoundOverP1;
			}
			sp.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}
		else
		{
			sp.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		}
	}
}
