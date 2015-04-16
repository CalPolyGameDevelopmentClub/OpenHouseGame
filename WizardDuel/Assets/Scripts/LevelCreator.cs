﻿using UnityEngine;
using System.Collections;

public class LevelCreator : MonoBehaviour {
	public GameObject testSprite;
	public int UIOFFSET;
	Sprite[] mossSprites;
	Sprite[] iceSprites;
	string[] currentLevel;
	ArrayList currentTiles = new ArrayList();
	//Lookup for uldr bstring to tilesheet.
	int[] tileTable=
	{
		9,
		10,
		13,
		14,
		8,
		11,
		12,
		15,
		5,
		6,
		1,
		2,
		4,
		7,
		0,
		3
	};
	string[] testLevel ={
		"#########################",
		"#          ###          #",
		"#          ###          #",
		"#                       #",
		"#     ###       ###     #",
		"#     ###       ###     #",
		"#     ###       ###     #",
		"#           #           #",
		"#   1               2   #",
		"#  ###             ###  #",
		"#        ## # ##        #",
		"# 3      ## # ##      4 #",
		"#####               #####",
		"#                       #",
		"     ###############     "};

	// Use this for initialization
	void Start () {
		loadSprites();
		loadLevel(testLevel);

	}

	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadLevel(string[] level)
	{
		currentLevel=level;
		int lvWdith = testLevel[0].Length;
		int lvHeight = testLevel.Length;
		bool[,] lvArray = new bool[lvWdith+2,lvHeight+2];
		for(int y = 0; y < lvHeight; y++)
		{
			for(int x = 0; x < lvWdith; x++)
			{
				if(testLevel[y][x] == '#')
				{
					lvArray[x+1,y+1] = true;
				}
			}
		}
		float blockwd=  testSprite.GetComponent<SpriteRenderer>().bounds.size.x;
		//		Debug.Log(blockwd);
		Vector3 mapTopLeft = this.transform.position - new Vector3(blockwd*lvWdith/2, blockwd*lvHeight/2);
		for(int y = 1; y <= lvHeight; y++)
		{
			for(int x = 1; x <= lvWdith; x++)
			{
				if(lvArray[x,y])
				{
					int tileIdx = 0;
					//Calculate free space in level
					tileIdx |= (lvArray[x,y-1]?0:1)<<3;
					tileIdx |= (lvArray[x-1,y]?0:1)<<2;
					tileIdx |= (lvArray[x,y+1]?0:1)<<1;
					tileIdx |= (lvArray[x+1,y]?0:1)<<0;
					
					
					
					float dx = mapTopLeft.x+x*blockwd;
					float dy = mapTopLeft.y+ (lvHeight-y)*blockwd + UIOFFSET;
					GameObject obj = (GameObject)Instantiate(testSprite,new Vector3(dx,dy,0), Quaternion.identity);
					obj.GetComponent<SpriteRenderer>().sprite=mossSprites[tileTable[tileIdx]];
					currentTiles.Add(obj);
				}
			}
		}
	}

	public void reset()
	{
		foreach(Object o in currentTiles)
		{
			Destroy(o);
		}
		loadLevel(currentLevel);
	}
	void loadSprites()
	{
		iceSprites = Resources.LoadAll<Sprite>(string.Format("blocks"));
		mossSprites = Resources.LoadAll<Sprite>(string.Format("stone blocks"));

	}

}
