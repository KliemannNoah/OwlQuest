using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backend : MonoBehaviour {

	public Text text1;
	public Text text2;
	public Text text3;
	public Text text4;
	public Text text5;
	public Text QuestText1;
	public Text QuestText2;
	public Text QuestText3;

	public bool completedAction = false;
	public bool preturnDone = false;
    public bool questLocation = false;
	public bool selectingCard = false;
    public bool canPurchase = false;
    public Turn turn;
	public Player1 player1;
	public Player2 player2;
	public Player3 player3;
	public Player4 player4;

	//probability for all of the locations.
	// 0 = Water, 1 = Food, 2 = Shelter, 3 = Treasure, 4 = Trading Post
	public int[] probability = new int[5] {2,3,4,5,0};

	public int[] occupied = new int[6]; //Who is at what location

	//text for each spot
	public string[] locationsText = new string[6] { "Water", "Food", "Shelter", "Treasure", "Trading Post", "Job Board"};

	public int questNumber = 0;
	//int quest = 0;
	public int questsComplete = 0;
	public Quests[] jobBoard = new Quests[3];
	public Quests[] questList = new Quests[21];
	public int tradingResource = 0; //tradingPost


	// Use this for initialization
	void Start () {
		turn = GetComponent<Turn>();
		tradingResource = Random.Range(0,4);
		//												Wa  Fo  Sh  Tr Points

		//Easy Quests
		questList[0] = new Quests("A Day at the Pond",	3,	0,	0,	0,	1);
		questList[1] = new Quests("Travel Rations",		2,	1,	0,	0,	1);
		questList[2] = new Quests("Refreshing Snack",	1,	1,	0,	0,	1);
		questList[3] = new Quests("Riverside Home",		1,	0,	1,	0,	1);
		questList[4] = new Quests("Baby Hero",			0,	0,	0,	1,	1);
		questList[5] = new Quests("Lake Hideaway",		1,	0,	0,	1,	2);
		questList[6] = new Quests("Winter Stockpile",	0,	3,	0,	0,	2);
		//Medium Quests
		questList[7] = new Quests("Soup",				3,	1,	0,	0,	2);
		questList[8] = new Quests("Dinner for 1",		1,	1,	1,	0,	2);
		questList[9] = new Quests("The Vault",			0,	0,	1,	1,	2);
		questList[10] = new Quests("Town Mayor",		2,	1,	1,	0,	2);
		questList[11] = new Quests("Quartermaster",		1,	2,	1,	0,	2);
		questList[12] = new Quests("Ocean Dungeon",		3,	0,	0,	1,	3);
		questList[13] = new Quests("Deputy Sheriff",	0,	0,	0,	2,	3);
		//Hard Quests
		questList[14] = new Quests("Flood Shelter",		2,	0,	2,	0,	3);
		questList[15] = new Quests("Dinner for 2",		2,	2,	1,	0,	3);
		questList[16] = new Quests("Great Rewards",		1,	0,	0,	2,	3);
		questList[17] = new Quests("Village Picnic",	0,	2,	2,	0,	3);
		questList[18] = new Quests("Owl Yacht",			3,	0,	1,	1,	4);
		questList[19] = new Quests("Trail Mix",			1,	1,	1,	1,	4);
		questList[20] = new Quests("Sheriff",			0,	0,	0,	3,	4);


		jobBoard[0] = questList[Random.Range(0,7)];
		jobBoard[1] = questList[Random.Range(0,7)];
		jobBoard[2] = questList[Random.Range(0,7)];

		questPrinter();

	}

	public void questPrinter(){
		QuestText1.text = jobBoard[0].title + "\n" + jobBoard[0].water.ToString() + " Water \n"+ jobBoard[0].food.ToString() + " Food \n"+ jobBoard[0].shelter.ToString() + " Shelter \n" + jobBoard[0].treasure.ToString() + " Treasure \n";
		QuestText2.text = jobBoard[1].title + "\n" + jobBoard[1].water.ToString() + " Water \n"+ jobBoard[1].food.ToString() + " Food \n"+ jobBoard[1].shelter.ToString() + " Shelter \n" + jobBoard[1].treasure.ToString() + " Treasure \n";
		QuestText3.text = jobBoard[2].title + "\n" + jobBoard[2].water.ToString() + " Water \n"+ jobBoard[2].food.ToString() + " Food \n"+ jobBoard[2].shelter.ToString() + " Shelter \n" + jobBoard[2].treasure.ToString() + " Treasure \n";
	}
}
