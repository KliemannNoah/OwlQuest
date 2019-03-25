using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static backend;


public class Turn : MonoBehaviour
{
	public backend b;
	public Player1 p1;
	
	
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
				b.completedAction = false;
				b.preturnDone = false;
            }
            else
            {
                currentPlayer = TurnDefs.Player.TWO;
                completedRound++;
				b.completedAction = false;
				b.preturnDone = false;
            }
        }


        else if (currentPlayer == TurnDefs.Player.TWO)
        {
            if (completedRound >= 3)
            {
                completedRound = 0;
                currentPlayer = TurnDefs.Player.FOUR;
            }
            else
            {
                currentPlayer = TurnDefs.Player.THREE;
                completedRound++;
            }
        }


        else if (currentPlayer == TurnDefs.Player.THREE)
        {
            if (completedRound >= 3)
            {
                completedRound = 0;
                currentPlayer = TurnDefs.Player.ONE;
            }
            else
            {
                currentPlayer = TurnDefs.Player.FOUR;
                completedRound++;
            }
        }


        else if (currentPlayer == TurnDefs.Player.FOUR)
        {
            if (completedRound >= 3)
            {
                completedRound = 0;
                currentPlayer = TurnDefs.Player.TWO;
            }
            else
            {
                currentPlayer = TurnDefs.Player.ONE;
                completedRound++;
            }
        }

    }



}
