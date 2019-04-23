using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backend : MonoBehaviour {

	public Text RollText;
	public Text Player1Resources;
	public Text Player2Resources;
	public Text Player3Resources;
	public Text Player4Resources;
	public Text Player1Quests;
	public Text Player2Quests;
	public Text Player3Quests;
	public Text Player4Quests;
	public bool completedAction = false;
    public bool questLocation = false;
	public bool selectingCard = false;
    public bool canPurchase = false;
    public Turn turn;
	public int numPlayers;
	
	public GameObject Panel;
	
	
	public AI AI1;
	public AI AI2;
	public AI AI3;
	public AI AI4;
	public Player player1;
	public Player player2;
	public Player player3;
	public Player player4;
	

	//probability for all of the locations.
	// 0 = Water, 1 = Food, 2 = Shelter, 3 = Treasure, 4 = Trading Post
	public int[] probability = new int[5] {2,3,4,5,0};
    public int[] tradingRolls = new int[4] { 3, 3, 4, 4 };

	public int[] occupied = new int[6]; //Who is at what location

	//text for each spot
	public string[] locationsText = new string[6] { "Water", "Food", "Shelter", "Treasure", "Trading Post", "Job Board"};

	public int questNumber = 0;
	public int questsComplete;
	public Quests[] jobBoard = new Quests[3];
	public Quests[] questList = new Quests[21];
	public Quests[] easyQuestList = new Quests[7];
	public Quests[] mediumQuestList = new Quests[7];
	public Quests[] hardQuestList = new Quests[7];
	public Quests[] combinedQuestList = new Quests[19];
	public int tradingResource = 0; //tradingPost
	public Quests[] replaced = new Quests[21]; //for sheriiff
	public int replacedTracker = 0;
	
	public int[] rolls = new int[4];

	// Use this for initialization
	void Start () {
		turn = GetComponent<Turn>();

		tradingResource = Random.Range(0,4);
		//												Wa  Fo  Sh  Tr Pnt eff
		//Easy Quests
		questList[0] = new Quests("A Day at the Pond",	3,	0,	0,	0,	1, 1, "Passive Ability: Add +1 to all Water Hole rolls");
		questList[1] = new Quests("Travel Rations",		2,	1,	0,	0,	1, 2, "Immediate Ability: Gain 1 Food Token");
		questList[2] = new Quests("Refreshing Snack",	1,	1,	0,	0,	1, 0, "");
		questList[3] = new Quests("Riverside Home",		1,	0,	1,	0,	1, 3, "Immediate Ability: Gain 1 Shelter Token");
		questList[4] = new Quests("Baby Hero",			0,	0,	0,	1,	1, 0, "");
		questList[5] = new Quests("Lake Hideaway",		1,	0,	0,	1,	2, 0, "");//
		questList[6] = new Quests("Winter Stockpile",	0,	3,	0,	0,	2, 4, "Passive Ability:  Add +1 to all Orchard rolls");
		//Medium Quests
		questList[7] = new Quests("Soup",				3,	1,	0,	0,	2, 0, "");
		questList[8] = new Quests("Dinner for 1",		1,	1,	1,	0,	2, 8, "Active Ability: If you wish, you may flip the trading post card again.");
		questList[9] = new Quests("The Vault",			0,	0,	1,	1,	2, 5, "Passive Ability: Add +1 to all Trading Post rolls");
		questList[10] = new Quests("Village Mayor",		2,	1,	1,	0,	2, 0, "");
		questList[11] = new Quests("Quartermaster",		1,	2,	1,	0,	2, 6, "Passive Ability: Roll 2 dice at the Trading Post and take the higher value");
		questList[12] = new Quests("Battle at the Dam",	3,	0,	0,	1,	3, 0, "");
		questList[13] = new Quests("Deputy Sheriff",	0,	0,	0,	2,	3, 7, "Passive Ability: Add +1 to all Foxes rolls");
		//Hard Quests
		questList[14] = new Quests("Flood Shelter",		2,	0,	2,	0,	3, 0, "");
		questList[15] = new Quests("Dinner for 2",		2,	2,	1,	0,	3, 0, "");
		questList[16] = new Quests("Great Rewards",		1,	0,	0,	2,	3, 12, "Active Ability: Every turn, select any ability from a quest on the quest board and use it. Chosen Ability effect expires at the end of the round.");
		questList[17] = new Quests("Village Picnic",	0,	2,	2,	0,	3, 0, "");
		questList[18] = new Quests("Owl Yacht",			3,	0,	1,	1,	4, 0, "");
		questList[19] = new Quests("Trail Mix",			1,	1,	1,	1,	4, 11, "Passive Effect: Gain +1 to rolls at a location of your choice. You must choose that location when the card is picked up and lasts the rest of the game.");
		questList[20] = new Quests("Sheriff",			0,	0,	0,	3,	4, 10, "Active Ability: Place any quest from the quest board on the bottom of the deck");

		for(int i = 0; i < 7; i++){
			easyQuestList[i] = questList[i];
		}		
		for(int i = 0; i < 7; i++){
			mediumQuestList[i] = questList[i+7];
		}		
		for(int i = 0; i < 7; i++){
			hardQuestList[i] = questList[i+14];
		}
		
		
		Shuffle(easyQuestList);
		Shuffle(mediumQuestList);
		Shuffle(hardQuestList);
		
		
		for(int i = 0; i < 6; i++){
			combinedQuestList[i] = easyQuestList[i];
		}		
		for(int i = 0; i < 6; i++){
			combinedQuestList[i+6] = mediumQuestList[i];
		}		
		for(int i = 0; i < 7; i++){
			combinedQuestList[i+12] = hardQuestList[i];
		}
		
		//for(int i = 0; i < 19; i++){
		//	Debug.Log(combinedQuestList[i].title);
		//}
		jobBoard[0] = combinedQuestList[0];
		jobBoard[1] = combinedQuestList[1];
		jobBoard[2] = combinedQuestList[2];
		questsComplete = 3;
		//Fill in missing player spots with AI
		numPlayers = StaticStart.numberOfPlayers;
		
		if(numPlayers == 0){ //Zero Players: 4 AI
		
			AI1 = new AI(1, "ONE", Player1Quests, Player1Resources, TurnDefs.Player.ONE, Panel);
			AI2 = new AI(2, "TWO", Player2Quests, Player2Resources, TurnDefs.Player.TWO, Panel);
			AI3 = new AI(3, "THREE", Player3Quests, Player3Resources, TurnDefs.Player.THREE, Panel);
			AI4 = new AI(4, "FOUR", Player4Quests, Player4Resources, TurnDefs.Player.FOUR, Panel);
		
			AI1.Update();
			AI2.Update();
			AI3.Update();
			AI4.Update();
			
			
		}else if(numPlayers == 1){ //One Player: 3 AI
			player1 = new Player(1, "ONE", Player1Quests, Player1Resources, TurnDefs.Player.ONE, Panel);
			AI2 = new AI(2, "TWO", Player2Quests, Player2Resources, TurnDefs.Player.TWO, Panel);
			AI3 = new AI(3, "THREE", Player3Quests, Player3Resources, TurnDefs.Player.THREE, Panel);
			AI4 = new AI(4, "FOUR", Player4Quests, Player4Resources, TurnDefs.Player.FOUR, Panel);
			
			player1.Start();
			AI2.Start();
			AI3.Start();
			AI4.Start();
			
		}else if(numPlayers == 2){ //Two Players: 2 AI
			player1 = new Player(1, "ONE", Player1Quests, Player1Resources, TurnDefs.Player.ONE, Panel);
			player2 = new Player(2, "TWO", Player2Quests, Player2Resources, TurnDefs.Player.TWO, Panel);
			AI3 = new AI(3, "THREE", Player3Quests, Player3Resources, TurnDefs.Player.THREE, Panel);
			AI4 = new AI(4, "FOUR", Player4Quests, Player4Resources, TurnDefs.Player.FOUR, Panel);
			
			player1.Start();
			player2.Start();
			AI3.Start();
			AI4.Start();
			
		}else if(numPlayers == 3){ //Three Players: 1 AI
			player1 = new Player(1, "ONE", Player1Quests, Player1Resources, TurnDefs.Player.ONE, Panel);
			player2 = new Player(2, "TWO", Player2Quests, Player2Resources, TurnDefs.Player.TWO, Panel);
			player3 = new Player(3, "THREE", Player3Quests, Player3Resources, TurnDefs.Player.THREE, Panel);
			AI4 = new AI(4, "FOUR", Player4Quests, Player4Resources, TurnDefs.Player.FOUR, Panel);
			
			player1.Start();
			player2.Start();
			player3.Start();
			AI4.Start();
			
		}else if(numPlayers == 4){ //Four Players: 0 AI
			player1 = new Player(1, "ONE", Player1Quests, Player1Resources, TurnDefs.Player.ONE, Panel);
			player2 = new Player(2, "TWO", Player2Quests, Player2Resources, TurnDefs.Player.TWO, Panel);
			player3 = new Player(3, "THREE", Player3Quests, Player3Resources, TurnDefs.Player.THREE, Panel);
			player4 = new Player(4, "FOUR", Player4Quests, Player4Resources, TurnDefs.Player.FOUR, Panel);
			
			player1.Start();
			player2.Start();
			player3.Start();
			player4.Start();
		}
	}


	void Update(){
		if(numPlayers == 0){ //Zero Players: 4 AI
			AI1.Update();
			AI2.Update();
			AI3.Update();
			AI4.Update();
		}else if(numPlayers == 1){ //One Player: 3 AI
			player1.Update();
			AI2.Update();
			AI3.Update();
			AI4.Update();
		}else if(numPlayers == 2){ //Two Players: 2 AI
			player1.Update();
			player2.Update();
			AI3.Update();
			AI4.Update();
		}else if(numPlayers == 3){ //Three Players: 1 AI
			player1.Update();
			player2.Update();
			player3.Update();
			AI4.Update();
		}else if(numPlayers == 4){ //Four Players: 0 AI
			player1.Update();
			player2.Update();
			player3.Update();
			player4.Update();
		}
	}
	
	static void Shuffle(Quests[] arr)
	{
		for (int i = 6; i > 0; i--) {
			int r = Random.Range(0,i);
			Quests tmp = arr[i];
			arr[i] = arr[r];
			arr[r] = tmp;
		}
	}
	
		// Coroutine that rolls the dice
    public IEnumerator SwapRound2()
    {
		yield return new WaitForSeconds(1f);
		turn.AdvanceTurn();
	}
}
