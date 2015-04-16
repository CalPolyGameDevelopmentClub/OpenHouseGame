using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DamagePercent : MonoBehaviour {

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
				gameObject.GetComponent<Text>().text = pv.damageRatio * 10 + "%";
				if (pv.damageRatio >= 8)
				{
					gameObject.GetComponent<Text>().color = new Color(0.8f, 0, 0);
				}
				else if (pv.damageRatio >= 4)
				{
					gameObject.GetComponent<Text>().color = new Color(0.8f, 0.8f, 0);
				}
				else
				{
					gameObject.GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
				}
			}
		}
	}
}
