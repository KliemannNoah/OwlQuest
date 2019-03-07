using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backend : MonoBehaviour {

	int[,] questList = new int[20,5];  // 1 row for each quest, and then the 4 columns of resources and 1 column of points
	//int[,] questListEasy = new int[20,5];  // 1 row for each quest, and then the 4 columns of resources and 1 column of points
	//int[,] questListMedium = new int[20,5];  // 1 row for each quest, and then the 4 columns of resources and 1 column of points
	//int[,] questListHard = new int[20,5];  // 1 row for each quest, and then the 4 columns of resources and 1 column of points
	
	
	
	//probability for all of the locations.
	// 0 = Water, 1 = Food, 2 = Shelter, 3 = Treasure, 4 = Trading Post
	int[] probability = new int[5] {2,3,4,5,0}; 
	
	int[] occupied = new int[6]; //Who is at what location
	
	//text for each spot
	string[] locationsText = new string[6] { "Water", "Food", "Shelter", "Treasure", "Trading Post", "Job Board"}; 
	 
	
	int[] jobBoard = new int[3];
	
	int bonusSpace = 0; //tradingPost

	int firstPlayer = 0;
	
	
	//Array looped through for player turns
	int[,] turnArray = new int[4,4] {
			{ 0, 1, 2, 3},
			{ 1, 2, 3, 0},
			{ 2, 3, 0, 1},
			{ 3, 0, 1, 2}
			};
	
	//1 = water, 2 = food, 3 = shelter, 4 = treasure, 5 Points
	int[,] resources = new int[4,5];

	// Use this for initialization
	void Start () {
		//turnArray[0] = { 0, 1, 2, 3};
		//turnArray[1] = { 1, 2, 3, 0};
		//turnArray[2] = { 2, 3, 0, 1};
		//turnArray[3] = { 3, 0, 1, 2};
		
		jobBoard[0] = Random.Range(0,19);
		jobBoard[1] = Random.Range(0,19);
		jobBoard[2] = Random.Range(0,19);
	}
	
	// Update is called once per frame
	void Update () {
		
		int location = -1;
		//Each round, randomize 1-4 for the bonus space
		bonusSpace = Random.Range(0,3);
		
		
			//For Each player
			for(int i = 0; i < 4; i++){
				int player = turnArray[firstPlayer,i];
				//have them pick their location to travel
				//Get their input 1-6
				//input-1 = array
				location =  3;
					
				occupied[location] = 1;
				
				//Handle Trading Post
				if(location == 4){
					location = bonusSpace - 1;
				}
					
				//Handle Quests
				if(location == 5){
					//Let players pick the quest they want
					//questNumber -1 = input;
					int questNumber = 0;
					handleQuests(questNumber, player, questNumber);
				}else{
					locationHandler(player, location, bonusSpace);	
				}
		
		
				//Check if they have won
				if(resources[player,4] >= 7){
						Debug.Log("GAME OVER, YOU WIN!");					
				}
			}
			//End Turn	
		
		
		
		//reset all occupied values to 0
		for(int i = 0; i < 6; i++){
			occupied[i] = 0;
		}
		
		//Advance firstPlayer
		firstPlayer = (firstPlayer + 1) % 4;
	
	}
	
	/**
		This function takes the player, their location, and the trading post number
		It then rolls and computes wether or not the player got the resources
	*/
	public void locationHandler(int player, int location, int bonusNumber){
		
		if(location !=4){
			if (Random.Range(1,5) > probability[location]){ 
						resources[player,location]++;
						Debug.Log("Resource Gained.");
			}
		}else{
			if(Random.Range(1,5) > probability[bonusNumber]){
						resources[player,bonusNumber]++;
						Debug.Log("Resource Gained.");	
			}
		}	
	}

	/**
		This Function is used when players attempt to turn in a quest
		It first checks to see if the player has all of the nessecary resources
		If it does it subtracts them from the players inventory, and gives the player the points
		It also replaces that quest with a new one from the list
	*/
	public bool handleQuests(int questNumber, int player, int quest){	
		//For each Resources
		for(int i = 0; i < 4; i++){
			if( resources[player,i] < questList[quest,i]){
				Debug.Log("Don't have the resources.");
				return false;
			}
		}
		
		//If at this point, player has resources
		Debug.Log("Quest Complete.");
		
		//Remove resources from player
		for(int i = 0; i < 4; i++){
			resources[player,i] -= questList[quest,i];
		}
		
		//Award player the points
		resources[player,4] += questList[quest,4];
		
		//Replenish Job Board
		jobBoard[questNumber] = Random.Range(1, 20);

		return true;
	}
	
}
