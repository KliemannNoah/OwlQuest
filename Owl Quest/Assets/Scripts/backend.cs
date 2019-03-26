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
	//public Player2 player2;
	//public Player3 player3;
	//public Player4 player4;

	//probability for all of the locations.	
	// 0 = Water, 1 = Food, 2 = Shelter, 3 = Treasure, 4 = Trading Post
	int[] probability = new int[5] {2,3,4,5,0}; 
	
	public int[] occupied = new int[6]; //Who is at what location
	
	//text for each spot
	string[] locationsText = new string[6] { "Water", "Food", "Shelter", "Treasure", "Trading Post", "Job Board"}; 
	 
	int questNumber = 0;
	//int quest = 0;
	int questsComplete = 0;
	public Quests[] jobBoard = new Quests[3];
	public Quests[] questList = new Quests[21];
	int bonusSpace = 0; //tradingPost


	// Use this for initialization
	void Start () {
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
	
	void Update() {
		if(!preturnDone){
			PreTurn();
		}
		if(!completedAction && !questLocation){
			Round(1);
		}
		if(!completedAction && questLocation){
			handleQuests(1);
		}
		if(completedAction){
			//PostTurn();
		}
	}
	/*public void PostTurn () {
		text5.text = "Post Turn";
		//reset all occupied values to 0
		for(int j = 0; j < 6; j++){
			occupied[j] = 0;
		}
	}*/
	
	public void PreTurn () {
		//Each round, randomize 1-4 for the bonus space
		text5.text = "Pre Turn";
		bonusSpace = Random.Range(0,4);
		preturnDone = true;
		text5.text = "Current Turn";
	}
	
	// Update is called once per frame
	public void Round (int player) {
		int location = -1;
		//have them pick their location to travel
		//Get their input 0-5
		if((Input.GetKeyDown("0")|| Input.GetKeyDown("1") || Input.GetKeyDown("2") || Input.GetKeyDown("3") || Input.GetKeyDown("4") || Input.GetKeyDown("5")))
		{
			if(Input.GetKeyDown("0")){
				location = 0;
			}else if(Input.GetKeyDown("1")){
				location = 1;
			}else if(Input.GetKeyDown("2")){
				location = 2;
			}else if(Input.GetKeyDown("3")){
				location = 3;
			}else if(Input.GetKeyDown("4")){
				location = 4;
			}else if(Input.GetKeyDown("5")){
				location = 5;
			}

			text2.text = "Location " + location;	
			occupied[location] = 1;
			text3.text = occupied[0] + "\t" + occupied[1] + "\t" + occupied[2] + "\t" + occupied[3] + "\t" + occupied[4]+ "\t" + occupied[5];
			
			
			//TODO: Handle Trading Post
			//Rework
			if(location == 4){
				location = bonusSpace;
			}
				
			//TODO: Handle Quests
			if(location == 5){
				//Let players pick the quest they want
				//questNumber -1 = input;
				questLocation = true;
			}else{
				locationHandler(player, location, bonusSpace);
				completedAction = true;
			}
	
	
			//Check if they have won
			if(player1.points >= 9){
			//if(player1.points >= 9){
					Debug.Log("GAME OVER, YOU WIN!");					
			}
			
		}
	
	}
	
	/**
		This function takes the player, their location, and the trading post number
		It then rolls and computes wether or not the player got the resources
	*/
	public void locationHandler(int player, int location, int bonusNumber){
		
		int randomNumber = Random.Range(1,7);
		text1.text = "Roll of " + randomNumber.ToString() + "\n";
		if(location !=4){
			if (randomNumber >= probability[location]){
						if(location == 0) player1.water++;
						if(location == 1) player1.food++;
						if(location == 2) player1.shelter++;
						if(location == 3) player1.treasure++;

						text1.text += locationsText[location].ToString() + " Gained.";
						text4.text = player1.water + "\t" + player1.food + "\t" +player1.shelter + "\t" + player1.treasure + "\t" + player1.points;
						
			}
		}else{
			if(randomNumber >= probability[bonusNumber]){
						if(bonusNumber == 0) player1.water++;
						if(bonusNumber == 1) player1.food++;
						if(bonusNumber == 2) player1.shelter++;
						if(bonusNumber == 3) player1.treasure++;
						text1.text += locationsText[location].ToString() + " Gained.";	
						text4.text = player1.water + "\t" + player1.food + "\t" +player1.shelter + "\t" + player1.treasure + "\t" + player1.points;
			}
		}	
	}

	/**
		This Function is used when players attempt to turn in a quest
		It first checks to see if the player has all of the nessecary resources
		If it does it subtracts them from the players inventory, and gives the player the points
		It also replaces that quest with a new one from the list
	*/
	//public bool handleQuests(int questNumber, int player, int quest){	
	public int handleQuests(int player){	
		//quest = jobBoard[questNumber];
		//For each Resources
		if((Input.GetKeyDown("0")|| Input.GetKeyDown("1") || Input.GetKeyDown("2"))){
			questLocation = false;
			if(Input.GetKeyDown("0")){
				questNumber = 0;
			}else if(Input.GetKeyDown("1")){
				questNumber = 1;
			}else if(Input.GetKeyDown("2")){
				questNumber = 2;
			}

			if(player1.water < jobBoard[questNumber].water){
				Debug.Log("Don't have the water.");
				return 1; //false
			}
			if(player1.food < jobBoard[questNumber].food){
				Debug.Log("Don't have the food.");
				return 1; //false
			}
			if(player1.shelter < jobBoard[questNumber].shelter){
				Debug.Log("Don't have the shelter.");
				return 1; //false
			}
			if(player1.treasure < jobBoard[questNumber].treasure){
				Debug.Log("Don't have the treasure.");
				return 1; //false
			}
			
			//If at this point, player has resources
			Debug.Log("Quest Complete.");
			questsComplete++;
			
			//Remove resources from player
			player1.water -= jobBoard[questNumber].water;
			player1.food -= jobBoard[questNumber].food; 
			player1.shelter -= jobBoard[questNumber].shelter;
			player1.treasure -= jobBoard[questNumber].treasure;
			
			//Award player the points
			player1.points += jobBoard[questNumber].points;
			
			player1.completedQuests[System.Array.FindIndex(player1.completedQuests, i => i == null)] = jobBoard[questNumber];
			//Replenish Job Board
			if(questsComplete < 4){
				jobBoard[questNumber] = questList[Random.Range(0,7)];
			}else if(questsComplete >= 4 && questsComplete < 10){
				jobBoard[questNumber] = questList[Random.Range(7,14)];
			}else{
				jobBoard[questNumber] = questList[Random.Range(14,21)];
			}
			questPrinter();
			player1.completed();
			return 2; //true
		}
		return 0;
	}
	
	public void questPrinter(){
		QuestText1.text = jobBoard[0].title + "\n" + jobBoard[0].water.ToString() + " Water \n"+ jobBoard[0].food.ToString() + " Food \n"+ jobBoard[0].shelter.ToString() + " Shelter \n" + jobBoard[0].treasure.ToString() + " Treasure \n";
		QuestText2.text = jobBoard[1].title + "\n" + jobBoard[1].water.ToString() + " Water \n"+ jobBoard[1].food.ToString() + " Food \n"+ jobBoard[1].shelter.ToString() + " Shelter \n" + jobBoard[1].treasure.ToString() + " Treasure \n";
		QuestText3.text = jobBoard[2].title + "\n" + jobBoard[2].water.ToString() + " Water \n"+ jobBoard[2].food.ToString() + " Food \n"+ jobBoard[2].shelter.ToString() + " Shelter \n" + jobBoard[2].treasure.ToString() + " Treasure \n";
	}
}
