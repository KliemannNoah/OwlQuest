using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static backend;


public class Turn : MonoBehaviour
{
	public backend b;
	
    public TurnDefs player;
    public TurnDefs.Player currentPlayer = TurnDefs.Player.ONE;
    private float completedRound = 0;
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
                completedRound = 0;
                currentPlayer = TurnDefs.Player.THREE;
				resetVariables();
				//reset all occupied values to 0
				for(int j = 0; j < 6; j++){
					b.occupied[j] = 0;
				}
				b.tradingResource = Random.Range(0,4);
              //  Debug.Log(b.tradingResource);
                b.TradingPostResource.text = b.locationsText[b.tradingResource] + "\nRoll " + b.tradingRolls[b.tradingResource] + "+";
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
                completedRound = 0;
                currentPlayer = TurnDefs.Player.FOUR;
				resetVariables();
				//reset all occupied values to 0
				for(int j = 0; j < 6; j++){
					b.occupied[j] = 0;
				}
				b.tradingResource = Random.Range(0,4);
               // Debug.Log(b.tradingResource);
                b.TradingPostResource.text = b.locationsText[b.tradingResource] + "\nRoll " + b.tradingRolls[b.tradingResource] + "+";
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
                completedRound = 0;
                currentPlayer = TurnDefs.Player.ONE;
				resetVariables();
				//reset all occupied values to 0
				for(int j = 0; j < 6; j++){
					b.occupied[j] = 0;
				}
				b.tradingResource = Random.Range(0,4);
               // Debug.Log(b.tradingResource);
                b.TradingPostResource.text = b.locationsText[b.tradingResource] + "\nRoll " + b.tradingRolls[b.tradingResource] + "+";
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
                completedRound = 0;
                currentPlayer = TurnDefs.Player.TWO;
				resetVariables();
				//reset all occupied values to 0
				for(int j = 0; j < 6; j++){
					b.occupied[j] = 0;
				}
				b.tradingResource = Random.Range(0,4);
               // Debug.Log(b.tradingResource);
                b.TradingPostResource.text = b.locationsText[b.tradingResource] + "\nRoll " + b.tradingRolls[b.tradingResource] + "+";
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

}
