using UnityEngine;
using System.Collections;

public class LevelCreator : MonoBehaviour {
	public GameObject testSprite;
	public GameObject playerGameObject;
	public int UIOFFSET;

	Sprite[] mossSprites;
	Sprite[] iceSprites;
	string[] currentLevel;
	bool[] currPlayers;

	ArrayList currentTiles = new ArrayList();
	ArrayList currentPlayers = new ArrayList();
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
	private string[][] levels ={new string[]{
		"###########   ###########",
		"                         ",
		"                         ",
		"####       ###       ####",
		"           ###           ",
		" 3         ###         4 ",
		"###     1       2     ###",
		"       ###     ###       ",
		"                         ",
		"####                 ####",
		"#         # # #         #",
		"#         #   #         #",
		"    ##    #   #    ##    ",
		"    ##    #   #    ##    ",
		"###########   ###########"
	}, new string[]
	{
		"###########   ###########",
		"                         ",
		"                         ",
		"####       ###       ####",
		"           ###           ",
		" 3         ###         4 ",
		"###     1       2     ###",
		"       ###     ###       ",
		"                         ",
		"####                 ####",
		"#         # # #         #",
		"#         #   #         #",
		"    ##    #   #    ##    ",
		"    ##    #   #    ##    ",
		"###########   ###########"
	}, new string[]
	{
		"###########   ###########",
		"                         ",
		"                         ",
		"####       ###       ####",
		"           ###           ",
		" 3         ###         4 ",
		"###                   ###",
		"        1       2        ",
		"       ###     ###       ",
		"####                 ####",
		"#         # # #         #",
		"#         #   #         #",
		"    ##    #   #    ##    ",
		"    ##    #   #    ##    ",
		"###########   ###########"
	}, new string[]
	{
		"###########   ###########",
		"                         ",
		"                         ",
		"####       ###       ####",
		"           ###           ",
		" 3         ###         4 ",
		"###     1       2     ###",
		"       ###     ###       ",
		"                         ",
		"####                 ####",
		"#         # # #         #",
		"#         #   #         #",
		"    ##    #   #    ##    ",
		"    ##    #   #    ##    ",
		"###########   ###########"
	}, new string[]
	{
		"###########   ###########",
		"                         ",
		"                         ",
		"####       ###       ####",
		"           ###           ",
		" 3         ###         4 ",
		"###     1       2     ###",
		"       ###     ###       ",
		"                         ",
		"####                 ####",
		"#         # # #         #",
		"#         #   #         #",
		"    ##    #   #    ##    ",
		"    ##    #   #    ##    ",
		"###########   ###########"
	},new string[]
	{
		"###########   ###########",
		"                         ",
		"                         ",
		"####       ###       ####",
		"           ###           ",
		" 3         ###         4 ",
		"###     1       2     ###",
		"       ###     ###       ",
		"                         ",
		"####                 ####",
		"#         # # #         #",
		"#         #   #         #",
		"    ##    #   #    ##    ",
		"    ##    #   #    ##    ",
		"###########   ###########"
	},new string[]
	{   "###########################",
		"#                         #",
	    "#                         #",
		"#	1                   2   #",
		"#####     3     4     #####",
	    "#####    ###   ###    #####",
		"    ####           ####    ",
		"    ####           ####    ",
	    "      ###         ###      ",
	    "      ###         ###      ",
	    "        ##       ##        ",
	    "        ##       ##        ",
		"         ##     ##         ",
	    "  ####    ##   ##    ####  ",
	    "   ##        #        ##   "
	},new string[]
	{
		"###########   ###########",
		"#                       #",
		"# 3                   4 #",
		"####                 ####",
		"                         ",
		"                         ",
		"###               # 2 #  ",
		"   # 1#  #####    #####  ",
		"   ####           #####  ",
		"   ####            ###   ",
		"    ##    #####    ###   ",
		"    ##    # # #    ###   ",
		"    ##    #####    ###   ",
		"    ##    #####    ###   ",
		"#########################"
	},new string[]
	{
		"###########   ###########",
		"#                       #",
		"#                       #",
		"#          ####         #",
		"#       1        2      #",
		"       ###  ##  ###      ",
		"           ####          ",
		"        3  ####  4       ",
		"       ###  ##  ###      ",
		"#####               #####",
		"           ####          ",
		"    ##             ##    ",
		"    ##             ##    ",
		"###########   ###########"
	}};
	int levelIndex;

	// Use this for initialization
	void Start () {
		loadSprites();
		levelIndex = 6;


	}

	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadLevel(string[] level, bool[] players)
	{
		Debug.Log("Loading level " + levelIndex);
		this.currPlayers=players;
		currentLevel=level;
		int lvWdith = level[0].Length;
		int lvHeight = level.Length;

		bool[,] lvArray = new bool[lvWdith+2,lvHeight+2];
		float blockwd=  testSprite.GetComponent<SpriteRenderer>().bounds.size.x;
		Vector3 mapTopLeft = this.transform.position - new Vector3(blockwd*lvWdith/2, blockwd*lvHeight/2);
		
		for(int y = 0; y < lvHeight; y++)
		{
			for(int x = 0; x < lvWdith; x++)
			{
				int player;
					if(level[y][x] == '#')
				{
					lvArray[x+1,y+1] = true;
				}

				else if(int.TryParse(""+level[y][x], out player)){
					if(players[player-1])
					{
						float dx = mapTopLeft.x+(x+1)*blockwd;
						float dy = mapTopLeft.y+ (lvHeight-(y+1))*blockwd + UIOFFSET;
						GameObject obj = (GameObject)Instantiate(playerGameObject,new Vector3(dx,dy,0), Quaternion.identity);
						obj.GetComponent<PlayerVars>().player="P"+player;
						/* SET ANIMATOR HERE!!*/
						obj.GetComponent<Animator>(); 
						currentPlayers.Add (obj);
					}
				}
			}
		}
		//		Debug.Log(blockwd);
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
					obj.GetComponent<SpriteRenderer>().sprite=iceSprites[tileTable[tileIdx]];
					currentTiles.Add(obj);
				}
			}
		}
	}

	public void reset()
	{
		clear();
		loadLevel(levels[(++levelIndex)%levels.Length],currPlayers);
	}

	public void clear()
	{
		foreach(Object o in currentTiles)
		{
			DestroyImmediate(o);
		}
		foreach(Object o in currentPlayers)
		{
			DestroyImmediate(o);
		}
	}
	public void dramaticExplosion()
	{
		foreach(Object o in currentTiles)
		{
			if(o != null)
			{
				GameObject obj = (GameObject) o;
				obj.GetComponent<WallScript>().dramaticFall();
			}
		}

	}

	void loadSprites()
	{
		iceSprites = Resources.LoadAll<Sprite>(string.Format("blocks"));
		mossSprites = Resources.LoadAll<Sprite>(string.Format("stone blocks"));

	}
	public string[] getLevel()
	{
		return levels[levelIndex % levels.Length];
	}
}
