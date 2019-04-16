﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static backend;


public class Turn : MonoBehaviour
{
	public backend b;
	
    public TurnDefs player;
    public TurnDefs.Player currentPlayer = TurnDefs.Player.ONE;
    private float completedRound = 0;
	int numPlayers = StaticStart.numberOfPlayers;
	
	public GameObject roundScreen;
	
	public Dice d1;
	public Dice d2;
	public Dice d3;
	public Dice d4;
	
	public Text t1;
	public Text t2;
	public Text t3;
	public Text t4;
	
    void OnGUI()
    {
        string display;
        if (currentPlayer == TurnDefs.Player.ONE){
            display = "Player One";
        }
        else if (currentPlayer == TurnDefs.Player.TWO)
        {
            display = "Player Two";
        }
        else if(currentPlayer == TurnDefs.Player.THREE)
        {
            display = "Player Three";
        }
        else
        {
            display = "Player Four";
        }

		if (GUILayout.Button(display + ": Click to change Player"))
		{
				NextPlayer();
		}
    }

    void NextPlayer()
    {
        if (currentPlayer == TurnDefs.Player.ONE)
        {
            if (completedRound >= 3)
            {
				printText();
				rollDice();
                completedRound = 0;
				displayUpdate();
				//reset all occupied values to 0
				for(int j = 0; j < 6; j++){
					b.occupied[j] = 0;
				}
				b.tradingResource = Random.Range(0,4);
				currentPlayer = TurnDefs.Player.THREE;
				resetVariables();
            }
            else
            {
                currentPlayer = TurnDefs.Player.TWO;
                completedRound++;
				resetVariables();
            }
        }


        else if (currentPlayer == TurnDefs.Player.TWO)
        {
            if (completedRound >= 3)
            {
				printText();
				rollDice();
                completedRound = 0;
				displayUpdate();
				//reset all occupied values to 0
				for(int j = 0; j < 6; j++){
					b.occupied[j] = 0;
				}
				b.tradingResource = Random.Range(0,4);
				currentPlayer = TurnDefs.Player.FOUR;
				resetVariables();
            }
            else
            {
                currentPlayer = TurnDefs.Player.THREE;
                completedRound++;
				resetVariables();
            }
        }


        else if (currentPlayer == TurnDefs.Player.THREE)
        {
            if (completedRound >= 3)
            {
				printText();
				rollDice();
                completedRound = 0;
				displayUpdate();
				//reset all occupied values to 0
				for(int j = 0; j < 6; j++){
					b.occupied[j] = 0;
				}
				b.tradingResource = Random.Range(0,4);
				currentPlayer = TurnDefs.Player.ONE;
				resetVariables();
            }
            else
            {
                currentPlayer = TurnDefs.Player.FOUR;
                completedRound++;
				resetVariables();
            }
        }


        else if (currentPlayer == TurnDefs.Player.FOUR)
        {
            if (completedRound >= 3)
            {
				printText();
				rollDice();
                completedRound = 0;
                
				displayUpdate();
				//reset all occupied values to 0
				for(int j = 0; j < 6; j++){
					b.occupied[j] = 0;
				}
				b.tradingResource = Random.Range(0,4);
				currentPlayer = TurnDefs.Player.TWO;
				resetVariables();
            }
            else
            {
                currentPlayer = TurnDefs.Player.ONE;
                completedRound++;
				resetVariables();
            }
        }
    }

    public TurnDefs.Player GetCurrentTurn() {
        return currentPlayer;
    }
	
	public void resetVariables(){
		b.completedAction = false;
		b.questLocation = false;
		b.selectingCard = false;
		b.canPurchase = false;
	}
	
	public void printText(){
		if(System.Array.IndexOf(b.occupied, 1) != -1){
			t1.text = b.locationsText[System.Array.IndexOf(b.occupied, 1)];
		}else{
			t1.text = "No Location";
		}
		if(System.Array.IndexOf(b.occupied, 2) != -1){
			t2.text = b.locationsText[System.Array.IndexOf(b.occupied, 2)];
		}else{
			t2.text = "No Location";
		}
		if(System.Array.IndexOf(b.occupied, 3) != -1){
			t3.text = b.locationsText[System.Array.IndexOf(b.occupied, 3)];
		}else{
			t3.text = "No Location";
		}
		if(System.Array.IndexOf(b.occupied, 4) != -1){
			t4.text = b.locationsText[System.Array.IndexOf(b.occupied, 4)];
		}else{
			t4.text = "No Location";
		}
	}
	
	public void rollDice(){
		roundScreen.SetActive(true);
		if(b.rolls[0] != 0 && System.Array.IndexOf(b.occupied, 1) != 5){
			d1.gameObject.SetActive(true);
			d1.roll(b.rolls[0]);
		}else{
			d1.gameObject.SetActive(false);
		}
		
		if(b.rolls[1] != 0 && System.Array.IndexOf(b.occupied, 2) != 5){
			d2.gameObject.SetActive(true);
			d2.roll(b.rolls[1]);
		}else{
			d2.gameObject.SetActive(false);
		}
		if(b.rolls[2] != 0 && System.Array.IndexOf(b.occupied, 3) != 5){
			d3.gameObject.SetActive(true);
			d3.roll(b.rolls[2]);
		}else{
			d3.gameObject.SetActive(false);
		}
		
		if(b.rolls[3] != 0 && System.Array.IndexOf(b.occupied, 4) != 5){
			d4.gameObject.SetActive(true);
			d4.roll(b.rolls[3]);
		}else{
			d4.gameObject.SetActive(false);
		}		
		
		b.rolls[0] = 0;
		b.rolls[1] = 0;
		b.rolls[2] = 0;
		b.rolls[3] = 0;
	}
	
	public void displayUpdate(){
		if(numPlayers == 1){
			b.player1.updateValues();
			b.AI2.updateValues();
			b.AI3.updateValues();
			b.AI4.updateValues();	
			
		}else if(numPlayers == 2){
			b.player1.updateValues();
			b.player2.updateValues();
			b.AI3.updateValues();
			b.AI4.updateValues();
			
		}else if(numPlayers == 3){
			b.player1.updateValues();
			b.player2.updateValues();
			b.player3.updateValues();
			b.AI4.updateValues();
			
		}else if(numPlayers == 4){
			b.player1.updateValues();
			b.player2.updateValues();
			b.player3.updateValues();
			b.player4.updateValues();
			
		}else if(numPlayers == 0){
			b.AI1.updateValues();
			b.AI2.updateValues();
			b.AI3.updateValues();
			b.AI4.updateValues();
		}
		

	}
	

}
