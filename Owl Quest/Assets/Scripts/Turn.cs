using System.Collections;
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
	
	public Text button;
	public Text warning;
	
	public bool waited = false;
	
    public void AdvanceTurn()
    {
		
		if (currentPlayer == TurnDefs.Player.ONE && System.Array.IndexOf(b.occupied, 1) == -1){
            warning.text = "Please Select A Location";
        }else if (currentPlayer == TurnDefs.Player.TWO && System.Array.IndexOf(b.occupied, 2) == -1){
            warning.text = "Please Select A Location";
        }else if(currentPlayer == TurnDefs.Player.THREE && System.Array.IndexOf(b.occupied, 3) == -1){
            warning.text = "Please Select A Location";
        }else if(currentPlayer == TurnDefs.Player.FOUR && System.Array.IndexOf(b.occupied, 4) == -1){
            warning.text = "Please Select A Location";
        }else{
			warning.text ="";
			NextPlayer();
		}
		
    }

    void NextPlayer()
    {
        if (currentPlayer == TurnDefs.Player.ONE)
        {
            if (completedRound >= 3)
            {
				Invoke("printText", .3f);
				//printText();
				Invoke("rollDice", .3f);
				//rollDice();
				currentPlayer = TurnDefs.Player.THREE;
				//roundSign();
            }
            else
            {
				turnSign();
				currentPlayer = TurnDefs.Player.TWO;
            }
        }


        else if (currentPlayer == TurnDefs.Player.TWO)
        {
            if (completedRound >= 3)
            {
				Invoke("printText", .3f);
				//printText();
				Invoke("rollDice", .3f);
				//rollDice();
				currentPlayer = TurnDefs.Player.FOUR;
				//roundSign();
            }
            else
            {
				turnSign();
				currentPlayer = TurnDefs.Player.THREE;
            }
        }


        else if (currentPlayer == TurnDefs.Player.THREE)
        {
            if (completedRound >= 3)
            {
				Invoke("printText", .3f);
				//printText();
				Invoke("rollDice", .3f);
				//rollDice();
				currentPlayer = TurnDefs.Player.ONE;
				//roundSign();
            }
            else
            {
				turnSign();
				currentPlayer = TurnDefs.Player.FOUR;
            }
        }


        else if (currentPlayer == TurnDefs.Player.FOUR)
        {
            if (completedRound >= 3)
            {
				Invoke("printText", .3f);
				//printText();
				Invoke("rollDice", .3f);
				//rollDice();
                currentPlayer = TurnDefs.Player.TWO;
				//roundSign();
            }
            else
            {
				turnSign();
				currentPlayer = TurnDefs.Player.ONE;
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
			if(System.Array.IndexOf(b.occupied, 1) == 4){
				t1.text = b.locationsText[System.Array.IndexOf(b.occupied, 1)] + "\n" + b.locationsText[b.tradingResource];
			}else{
				t1.text = b.locationsText[System.Array.IndexOf(b.occupied, 1)];
			}
		}else{
			t1.text = "No Location";
		}
		if(System.Array.IndexOf(b.occupied, 2) != -1){
			if(System.Array.IndexOf(b.occupied, 2) == 4){
				t2.text = b.locationsText[System.Array.IndexOf(b.occupied, 2)] + "\n" + b.locationsText[b.tradingResource];
			}else{
				t2.text = b.locationsText[System.Array.IndexOf(b.occupied, 2)];
			}
		}else{
			t2.text = "No Location";
		}
		if(System.Array.IndexOf(b.occupied, 3) != -1){
			if(System.Array.IndexOf(b.occupied, 3) == 4){
				t3.text = b.locationsText[System.Array.IndexOf(b.occupied, 3)] + "\n" + b.locationsText[b.tradingResource];
			}else{
				t3.text = b.locationsText[System.Array.IndexOf(b.occupied, 3)];
			}
		}else{
			t3.text = "No Location";
		}
		if(System.Array.IndexOf(b.occupied, 4) != -1){
			if(System.Array.IndexOf(b.occupied, 4) == 4){
				t4.text = b.locationsText[System.Array.IndexOf(b.occupied, 4)] + "\n" + b.locationsText[b.tradingResource];
			}else{
				t4.text = b.locationsText[System.Array.IndexOf(b.occupied, 4)];
			}
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
	
	void Update(){
		if (currentPlayer == TurnDefs.Player.ONE){
            button.text = "Player One's Turn";
        }
        else if (currentPlayer == TurnDefs.Player.TWO)
        {
            button.text = "Player Two's Turn";
        }
        else if(currentPlayer == TurnDefs.Player.THREE)
        {
            button.text = "Player Three's Turn";
        }
        else
        {
            button.text = "Player Four's Turn";
        }
		
		if(waited){
			waited = false;
			StartCoroutine("SwapRound");

		}
	}
	
	public void flip(){
		waited = true;
		
	}
	
	private void turnSign()
    {
        StartCoroutine("SwapTurn");
    }
	
	// Coroutine that rolls the dice
    private IEnumerator SwapTurn()
    {
		yield return new WaitForSeconds(.5f);
		//Do animation
		completedRound++;
		resetVariables();
    }
	
	private void roundSign()
    {
        StartCoroutine("SwapRound");
    }
	
	// Coroutine that rolls the dice
    private IEnumerator SwapRound()
    {
		yield return new WaitForSeconds(.2f);
		completedRound = 0;
		displayUpdate();
		//reset all occupied values to 0
		for(int j = 0; j < 6; j++){
			b.occupied[j] = 0;
		}
		b.tradingResource = Random.Range(0,4);
		yield return new WaitForSeconds(2f);
		d1.Outcome.text = "";
		d2.Outcome.text = "";
		d3.Outcome.text = "";
		d4.Outcome.text = "";
		resetVariables();
    }
}
