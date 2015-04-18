using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DamagePercent : MonoBehaviour {
	public Sprite white;
	public Sprite yellow;
	public Sprite red;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Object[] players = GameObject.FindGameObjectsWithTag("Player");

		foreach (GameObject p in players)
		{
			PlayerVars pv = p.GetComponent<PlayerVars>();
			if (pv.player == GetComponentInParent<UIPlayerInfo>().player)
			{
				gameObject.GetComponent<Text>().text = (pv.damageRatio * 10).ToString();
				if (pv.damageRatio >= 8)
				{
					gameObject.GetComponentInChildren<Image>().sprite = red;
					gameObject.GetComponent<Text>().color = new Color(0.8f, 0, 0);
				}
				else if (pv.damageRatio >= 4)
				{
					gameObject.GetComponentInChildren<Image>().sprite = yellow;
					gameObject.GetComponent<Text>().color = new Color(0.8f, 0.8f, 0);
				}
				else
				{
					gameObject.GetComponentInChildren<Image>().sprite = white;
					gameObject.GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
				}
			}
		}
	}
}
