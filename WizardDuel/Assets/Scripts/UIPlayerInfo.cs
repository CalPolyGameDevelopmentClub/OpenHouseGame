using UnityEngine;
using System.Collections;

public class UIPlayerInfo : MonoBehaviour {
	public  GameObject crownObject;
	private Vector2 offset = new Vector2(-310f , -200f);
	private Vector2 crownOffset = new Vector2(100,10);
	private int numCrowns = 0;
	public string player;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.K))
		{
			addCrown();
		}
		GameMonitorScript gm = GameObject.FindGameObjectWithTag("GameMonitor").GetComponent<GameMonitorScript>();
		bool active = false;

		foreach (PlayerInfo pi in gm.activePlayers)
		{
			if (player == pi.playerNum)
			{
				active = true;
			}
		}
		
		if (active)
		{
			gameObject.GetComponent<CanvasGroup>().alpha = 1.0f;
		}
		else
		{
			gameObject.GetComponent<CanvasGroup>().alpha = 0.0f;
		}
	}
	public void addCrown()
	{
		Vector3 crownUIPosition = new Vector3(this.offset.x,this.offset.y + this.crownOffset.y*0.9f * this.numCrowns + 20, this.numCrowns);
		GameObject crownObj = (GameObject)Instantiate(crownObject,Vector3.zero,Quaternion.AngleAxis(Random.Range (-15f,15f),new Vector3(0,0,1)));
		crownObj.transform.SetParent(this.transform);
		crownObj.transform.localPosition = crownUIPosition;
		this.numCrowns++;
	}
}
