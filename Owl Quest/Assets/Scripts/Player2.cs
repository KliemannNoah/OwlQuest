using System.Collections;
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
	public Text PlayerQuests2;
	public Quests[] completedQuests = new Quests[10];
	int[] rollProbability = new int[5] {2,3,4,5,0};
	int tradingPostModifier = 0;
	int advantage = 1;
	bool trailMix = false;
	
	bool sheriff = false;
	bool rewards = false;
	bool reroll = false;
	bool tempSheriff = false;
	bool tempRewards = false;
	bool tempRewards2 = false;
	bool tempReroll = false;
	bool undoReroll = false;
	bool undoSheriff = false;
    // Start is called before the first frame update
    void Start()
    {
        PlayerQuests2.text  = "";
    }

	
	public void completed(){
		PlayerQuests2.text ="";
		for(int i = 0; i < 10; i++){
			if(completedQuests[i] != null){
			PlayerQuests2.text += completedQuests[i].title + "\t" + completedQuests[i].water.ToString() + " Water \t"+ completedQuests[i].food.ToString() + " Food \t"+ completedQuests[i].shelter.ToString() + " Shelter \t" + completedQuests[i].treasure.ToString() + " Treasure \t" + completedQuests[i].points.ToString() + " points \n";
			}
		}
	}
	
	
    // Update is called once per frame
	void Update() {
		TurnDefs.Player currentTurn = b.turn.GetCurrentTurn();
		if(currentTurn == TurnDefs.Player.TWO){
			if(trailMix){
				selectSpot();
			}else if(rewards && tempRewards){ //Want to use another quests ability?
				if((Input.GetKeyDown("y")|| Input.GetKeyDown("n"))){
					if(Input.GetKeyDown("y")){
						tempRewards2 = true;
					}else if(Input.GetKeyDown("n")){
						tempRewards = false;
					}
				}
			}else if(rewards && tempRewards2){
				if((Input.GetKeyDown("0")|| Input.GetKeyDown("1") || Input.GetKeyDown("2"))){
					if(Input.GetKeyDown("0")){
						temporaryRewards(b.jobBoard[0].effect);
					}else if(Input.GetKeyDown("1")){
						temporaryRewards(b.jobBoard[1].effect);
					}else if(Input.GetKeyDown("2")){
						temporaryRewards(b.jobBoard[2].effect);
					}
					tempRewards2 = false;
				}				
			}else if(sheriff && tempSheriff){
				if((Input.GetKeyDown("0")|| Input.GetKeyDown("1") || Input.GetKeyDown("2"))){
					b.questsComplete++;

					if(Input.GetKeyDown("0")){
						b.replaced[System.Array.FindIndex(b.replaced, i => i == null)] = b.jobBoard[0];
						newQuest(0);
					}else if(Input.GetKeyDown("1")){
						b.replaced[System.Array.FindIndex(b.replaced, i => i == null)] = b.jobBoard[1];
						newQuest(1);
					}else if(Input.GetKeyDown("2")){
						b.replaced[System.Array.FindIndex(b.replaced, i => i == null)] = b.jobBoard[2];
						newQuest(2);
					}
					tempSheriff = false;
				}
			}else if(reroll && tempReroll){
				if((Input.GetKeyDown("y")|| Input.GetKeyDown("n"))){
					if(Input.GetKeyDown("y")){
						b.tradingResource = Random.Range(0,4);
						tempReroll = false;
					}else if(Input.GetKeyDown("n")){
						tempReroll = false;
					}
				}
			}else if(!b.completedAction && !b.questLocation){
				Round();
				
			}else if(!b.completedAction && b.questLocation){
				handleQuests();
			}
			
		}
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
				b.occupied[location] = 2;
				
				//TODO: Handle Trading Post
				//Rework
				if(location == 4){
					location = b.tradingResource;
				}
					
				//TODO: Handle Quests
				if(location == 5){
					//Let players pick the quest they want
					//questNumber -1 = input;
					b.questLocation = true;
				}else if(location == 4) {
					TradingPost(b.tradingResource);
					b.completedAction = true;
					if(undoSheriff){
						sheriff = false;
						tempSheriff = false;
						undoSheriff = false;
					}else if(undoReroll){
						reroll = false;
						tempReroll = false;
						undoReroll = false;
					}
				}else{
					locationHandler(location);
					b.completedAction = true;
					if(undoSheriff){
						sheriff = false;
						tempSheriff = false;
						undoSheriff = false;
					}else if(undoReroll){
						reroll = false;
						tempReroll = false;
						undoReroll = false;
					}
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
	public void locationHandler(int location){
		
		int randomNumber = Random.Range(1,7);
		b.RollText.text = "Roll of " + randomNumber.ToString() + "\n";
		if (randomNumber >= b.probability[location]){
					if(location == 0) this.water++;
					if(location == 1) this.food++;
					if(location == 2) this.shelter++;
					if(location == 3) this.treasure++;

					b.RollText.text += b.locationsText[location].ToString() + " Gained.";
					b.Player2Resources.text = this.water + "\t" + this.food + "\t" +this.shelter + "\t" + this.treasure + "\t" + this.points;
					
		}

	}

	public void TradingPost(int resource) {
        int randomNumber = Random.Range(1, 7);
		int randomNumber2 = Random.Range(1,7);
		if(randomNumber2 > randomNumber && advantage == 2){
			randomNumber = randomNumber2;
		}
        b.RollText.text = "Roll of " + randomNumber.ToString() + "\n";
        if (randomNumber >= (4-tradingPostModifier)) {
            if (resource == 0) this.water++;
            if (resource == 1) this.food++;
            if (resource == 2) this.shelter++;
            if (resource == 3) this.treasure++;
            b.RollText.text += b.locationsText[resource].ToString() + " Gained.";
            b.Player2Resources.text = this.water + "\t" + this.food + "\t" + this.shelter + "\t" + this.treasure + "\t" + this.points;
        }
        else if (randomNumber >= (3-tradingPostModifier) && resource < 2) {
            if (resource == 0) this.water++;
            if (resource == 1) this.food++;
            b.RollText.text += b.locationsText[resource].ToString() + " Gained.";
            b.Player2Resources.text = this.water + "\t" + this.food + "\t" + this.shelter + "\t" + this.treasure + "\t" + this.points;
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
			b.Player2Resources.text = this.water + "\t" + this.food + "\t" +this.shelter + "\t" + this.treasure + "\t" + this.points;
			
			//Award player the points
			this.points += b.jobBoard[b.questNumber].points;
			
			//Add card to personal quest list
			this.completedQuests[System.Array.FindIndex(this.completedQuests, i => i == null)] = b.jobBoard[b.questNumber];
			
			//Apply Effects
			questEffects(b.jobBoard[b.questNumber].effect);

			newQuest(b.questNumber);
			//Replenish Job Board
			
			b.questPrinter();
			this.completed();
			b.completedAction = true;
			if(undoSheriff){
				sheriff = false;
				tempSheriff = false;
				undoSheriff = false;
			}else if(undoReroll){
				reroll = false;
				tempReroll = false;
				undoReroll = false;
			}
			return 2; //true
		}
		return 0;
	}
	
	
	public void newQuest(int questNumber){
		if(b.questsComplete < 4){
			b.jobBoard[questNumber] = b.questList[Random.Range(0,7)];
		}else if(b.questsComplete >= 4 && b.questsComplete < 10){
			b.jobBoard[questNumber] = b.questList[Random.Range(7,14)];
		}else{
			b.jobBoard[questNumber] = b.questList[Random.Range(14,21)];
		}
		
	}
	
	
	public void questEffects(int effectNumber){
		//8, 10, and 12 are active
		if(effectNumber == 0){
			return;
		}else if(effectNumber == 1){ //Passive
			rollProbability[0]--;
		}else if(effectNumber == 2){ //Immediate
			this.food++;
		}else if(effectNumber == 3){ //Immediate
			this.shelter++;
		}else if(effectNumber == 4){ //Passive
			rollProbability[1]--;
		}else if(effectNumber == 5){ //Passive
			tradingPostModifier = 1;
		}else if(effectNumber == 6){ //Passive
			advantage = 2;
		}else if(effectNumber == 7){ //Passive
			rollProbability[3]--;
		}else if(effectNumber == 8){ //Active Ability: Reroll trading post
			reroll = true;
			tempReroll = true;
		}else if(effectNumber == 9){ //
		
		}else if(effectNumber == 10){ // Active Ability: Place any quest from the quest board on the bottom of the deck
			sheriff = true;
			tempSheriff = true;
		}else if(effectNumber == 11){ // Passive Effect: Gain +1 to rolls at a location of your choice. You must choose that location when the card is picked up and lasts the rest of the game.
			trailMix = true;
		}else if(effectNumber == 12){ // Active Ability: Every turn, select any ability from a quest on the quest board and use it. Chosen Ability effect expires at the end of the round.
			rewards = true;
			tempRewards = true;
		}
	}
	
	public void selectSpot(){
		if((Input.GetKeyDown("0")|| Input.GetKeyDown("1") || Input.GetKeyDown("2") || Input.GetKeyDown("3") || Input.GetKeyDown("4")))
		{
			if(Input.GetKeyDown("0")){
				rollProbability[0]--;
				trailMix = false;
			}else if(Input.GetKeyDown("1")){
				rollProbability[1]--;
				trailMix = false;
			}else if(Input.GetKeyDown("2")){
				rollProbability[2]--;
				trailMix = false;
			}else if(Input.GetKeyDown("3")){
				rollProbability[3]--;
				trailMix = false;
			}else if(Input.GetKeyDown("4")){
				tradingPostModifier++;
				trailMix = false;
			}
		}
	}
	
	public void temporaryRewards(int effectNumber){
		if(effectNumber == 8){ //Active Ability: Reroll trading post
			reroll = true;
			tempReroll = true;
			undoReroll = true;
		}else if(effectNumber == 10){ // Active Ability: Place any quest from the quest board on the bottom of the deck
			sheriff = true;
			tempSheriff = true;
			undoSheriff = true;
		}
	}
}
