﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static backend;

public class Player2 : MonoBehaviour
{
	public backend b;
	
	public int food = 0;
	public int water = 0;
	public int shelter = 0;
	public int treasure = 0;
	public int points = 0;
	public Text PlayerQuests;
	public Quests[] completedQuests = new Quests[10];
	
    // Start is called before the first frame update
    void Start()
    {
        PlayerQuests.text  = "";
    }

	
	public void completed(){
		PlayerQuests.text ="";
		for(int i = 0; i < 10; i++){
			if(completedQuests[i] != null){
			PlayerQuests.text += completedQuests[i].title + "\t" + completedQuests[i].water.ToString() + " Water \t"+ completedQuests[i].food.ToString() + " Food \t"+ completedQuests[i].shelter.ToString() + " Shelter \t" + completedQuests[i].treasure.ToString() + " Treasure \t" + completedQuests[i].points.ToString() + " points \n";
			}
		}
	}
	
	
    // Update is called once per frame
	void Update() {
		TurnDefs.Player currentTurn = b.turn.GetCurrentTurn();
		if(currentTurn == TurnDefs.Player.TWO){
			if(!b.preturnDone){
				PreTurn();
			}
			if(!b.completedAction && !b.questLocation){
				Round();
			}
			if(!b.completedAction && b.questLocation){
				handleQuests();
			}
		}
	}
	
	
	public void PreTurn () {
		//Each round, randomize 1-4 for the bonus space
		b.text5.text = "Pre Turn";
		b.bonusSpace = Random.Range(0,4);
		b.preturnDone = true;
		b.text5.text = "Current Turn";
	}
	
	// Update is called once per frame
	public void Round () {
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
			
			if(b.occupied[location] == 0){
				b.text2.text = "Location " + location;	
				b.occupied[location] = 2;
				b.text3.text = b.occupied[0] + "\t" + b.occupied[1] + "\t" + b.occupied[2] + "\t" + b.occupied[3] + "\t" + b.occupied[4]+ "\t" + b.occupied[5];
				
				
				//TODO: Handle Trading Post
				//Rework
				if(location == 4){
					location = b.bonusSpace;
				}
					
				//TODO: Handle Quests
				if(location == 5){
					//Let players pick the quest they want
					//questNumber -1 = input;
					b.questLocation = true;
				}else{
					locationHandler(location, b.bonusSpace);
					b.completedAction = true;
				}
		
		
				//Check if they have won
				if(this.points >= 9){
						Debug.Log("GAME OVER, YOU WIN!");					
				}
			}
		}
	
	}
	
	/**
		This function takes the player, their location, and the trading post number
		It then rolls and computes wether or not the player got the resources
	*/
	public void locationHandler( int location, int bonusNumber){
		
		int randomNumber = Random.Range(1,7);
		b.text1.text = "Roll of " + randomNumber.ToString() + "\n";
		if(location !=4){
			if (randomNumber >= b.probability[location]){
						if(location == 0) this.water++;
						if(location == 1) this.food++;
						if(location == 2) this.shelter++;
						if(location == 3) this.treasure++;

						b.text1.text += b.locationsText[location].ToString() + " Gained.";
						b.text4.text = this.water + "\t" + this.food + "\t" +this.shelter + "\t" + this.treasure + "\t" + this.points;
						
			}
		}else{
			if(randomNumber >= b.probability[bonusNumber]){
						if(bonusNumber == 0) this.water++;
						if(bonusNumber == 1) this.food++;
						if(bonusNumber == 2) this.shelter++;
						if(bonusNumber == 3) this.treasure++;
						b.text1.text += b.locationsText[location].ToString() + " Gained.";	
						b.text4.text = this.water + "\t" + this.food + "\t" +this.shelter + "\t" + this.treasure + "\t" + this.points;
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
	public int handleQuests(){	
		//quest = jobBoard[questNumber];
		//For each Resources
		if((Input.GetKeyDown("0")|| Input.GetKeyDown("1") || Input.GetKeyDown("2"))){
			b.questLocation = false;
			if(Input.GetKeyDown("0")){
				b.questNumber = 0;
			}else if(Input.GetKeyDown("1")){
				b.questNumber = 1;
			}else if(Input.GetKeyDown("2")){
				b.questNumber = 2;
			}

			if(this.water < b.jobBoard[b.questNumber].water){
				Debug.Log("Don't have the water.");
				return 1; //false
			}
			if(this.food < b.jobBoard[b.questNumber].food){
				Debug.Log("Don't have the food.");
				return 1; //false
			}
			if(this.shelter < b.jobBoard[b.questNumber].shelter){
				Debug.Log("Don't have the shelter.");
				return 1; //false
			}
			if(this.treasure < b.jobBoard[b.questNumber].treasure){
				Debug.Log("Don't have the treasure.");
				return 1; //false
			}
			
			//If at this point, player has resources
			Debug.Log("Quest Complete.");
			b.questsComplete++;
			
			//Remove resources from player
			this.water -= b.jobBoard[b.questNumber].water;
			this.food -= b.jobBoard[b.questNumber].food; 
			this.shelter -= b.jobBoard[b.questNumber].shelter;
			this.treasure -= b.jobBoard[b.questNumber].treasure;
			
			//Award player the points
			this.points += b.jobBoard[b.questNumber].points;
			
			this.completedQuests[System.Array.FindIndex(this.completedQuests, i => i == null)] = b.jobBoard[b.questNumber];
			//Replenish Job Board
			if(b.questsComplete < 4){
				b.jobBoard[b.questNumber] = b.questList[Random.Range(0,7)];
			}else if(b.questsComplete >= 4 && b.questsComplete < 10){
				b.jobBoard[b.questNumber] = b.questList[Random.Range(7,14)];
			}else{
				b.jobBoard[b.questNumber] = b.questList[Random.Range(14,21)];
			}
			b.questPrinter();
			this.completed();
			return 2; //true
		}
		return 0;
	}
}