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
				gameObject.GetComponent<Text>().text = pv.damageRatio.ToString() + "%";
			}
		}
	}
}
