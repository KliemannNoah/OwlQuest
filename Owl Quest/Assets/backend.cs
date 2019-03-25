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

	
	public bool completedAction = false;
	public bool preturnDone = false;
    public bool questLocation = false;
	public bool selectingCard = false;
    public bool canPurchase = false;
    public Turn turn;
	int[,] questList = new int[20,5];  // 1 row for each quest, and then the 4 columns of resources and 1 column of points
	/*
	//int[,] questListEasy = new int[20,5];  // 1 row for each quest, and then the 4 columns of resources and 1 column of points
	//int[,] questListMedium = new int[20,5];  // 1 row for each quest, and then the 4 columns of resources and 1 column of points
	//int[,] questListHard = new int[20,5];  // 1 row for each quest, and then the 4 columns of resources and 1 column of points
	*/
	//public Player1 player1;
    //public Quests quest;
	//Quests quest1;

	//probability for all of the locations.	
	// 0 = Water, 1 = Food, 2 = Shelter, 3 = Treasure, 4 = Trading Post
	int[] probability = new int[5] {2,3,4,5,0}; 
	
	int[] occupied = new int[6]; //Who is at what location
	
	//text for each spot
	string[] locationsText = new string[6] { "Water", "Food", "Shelter", "Treasure", "Trading Post", "Job Board"}; 
	 
	int questNumber = 0;
	int quest = 0;
	//int[] jobBoard = new int[3];
	public Quests[] jobBoard = new Quests[3];
	int bonusSpace = 0; //tradingPost

	//int firstPlayer = 0;
	
	
	//Array looped through for player turns
	/*int[,] turnArray = new int[4,4] {
			{ 0, 1, 2, 3},
			{ 1, 2, 3, 0},
			{ 2, 3, 0, 1},
			{ 3, 0, 1, 2}
			};
	*/
	//1 = water, 2 = food, 3 = shelter, 4 = treasure, 5 Points
	public int[,] resources = new int[4,5];

	// Use this for initialization
	void Start () {
		/*
		//turnArray[0] = { 0, 1, 2, 3};
		//turnArray[1] = { 1, 2, 3, 0};
		//turnArray[2] = { 2, 3, 0, 1};
		//turnArray[3] = { 3, 0, 1, 2};
		*/
		//jobBoard[0] = Random.Range(0,19);
		//jobBoard[1] = Random.Range(0,19);
		//jobBoard[2] = Random.Range(0,19);

		jobBoard[0] = new Quests("1 water 2 food",1,2,0,0,1);
		jobBoard[1] = new Quests("2 water 1 food",2,1,0,0,1);
		jobBoard[2] = new Quests("Trail Mix",1,1,1,1,2);

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
			PostTurn();
		}
	}
	public void PostTurn () {
		text5.text = "Post Turn";
		//reset all occupied values to 0
		for(int j = 0; j < 6; j++){
			occupied[j] = 0;
		}
	}
	
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
			if(resources[player,4] >= 9){
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
						resources[player,location]++;
						text1.text += locationsText[location].ToString() + " Gained.";
						text4.text = resources[1,0] + "\t" + resources[1,1] + "\t" + resources[1,2] + "\t" + resources[1,3] + "\t" + resources[1,4];
						
			}
		}else{
			if(randomNumber >= probability[bonusNumber]){
						resources[player,bonusNumber]++;
						text1.text += locationsText[location].ToString() + " Gained.";	
						text4.text = resources[1,0] + "\t" + resources[1,1] + "\t" + resources[1,2] + "\t" + resources[1,3] + "\t" + resources[1,4];
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
			/*	
			for(int i = 0; i < 4; i++){
				if( resources[player,i] < questList[quest,i]){
					Debug.Log("Don't have the resources.");
					return 1; //false
				}
			}
			*/
			if(resources[player,0] < jobBoard[questNumber].water){
				Debug.Log("Don't have the water.");
				return 1; //false
			}
			if(resources[player,1] < jobBoard[questNumber].food){
				Debug.Log("Don't have the food.");
				return 1; //false
			}
			if(resources[player,2] < jobBoard[questNumber].shelter){
				Debug.Log("Don't have the shelter.");
				return 1; //false
			}
			if(resources[player,3] < jobBoard[questNumber].treasure){
				Debug.Log("Don't have the treasure.");
				return 1; //false
			}
			
			//If at this point, player has resources
			Debug.Log("Quest Complete.");
			
			//Remove resources from player
			//for(int i = 0; i < 4; i++){
			//	resources[player,i] -= questList[quest,i];
			//}
			resources[player,0] -= jobBoard[questNumber].water;
			resources[player,1] -= jobBoard[questNumber].food; 
			resources[player,2] -= jobBoard[questNumber].shelter;
			resources[player,3] -= jobBoard[questNumber].treasure;
			
			//Award player the points
			resources[player,4] += jobBoard[questNumber].points;
			Debug.Log(resources[player,4]);
			//Replenish Job Board
			//jobBoard[questNumber] = Random.Range(1, 20);

			return 2; //true
		}
		return 0;
	}
	
}
