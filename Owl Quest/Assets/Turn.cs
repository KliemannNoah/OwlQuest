using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    public PlayerStats player;
    public PlayerStats.Player currentPlayer = PlayerStats.Player.ONE;
    private float completedRound = 0;
    void OnGUI()
    {
        string display;
        //string display = (currentPlayer == PlayerStats.ONE) ? "Player One" : "Player Two";
        if (currentPlayer == PlayerStats.Player.ONE){
            display = "Player One";
        }
        else if (currentPlayer == PlayerStats.Player.TWO)
        {
            display = "Player Two";
        }
        else if(currentPlayer == PlayerStats.Player.THREE)
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
        if (currentPlayer == PlayerStats.Player.ONE)
        {
            if (completedRound >= 3)
            {
                completedRound = 0;
                currentPlayer = PlayerStats.Player.THREE;
            }
            else
            {
                currentPlayer = PlayerStats.Player.TWO;
                completedRound++;
            }
        }


        else if (currentPlayer == PlayerStats.Player.TWO)
        {
            if (completedRound >= 3)
            {
                completedRound = 0;
                currentPlayer = PlayerStats.Player.FOUR;
            }
            else
            {
                currentPlayer = PlayerStats.Player.THREE;
                completedRound++;
            }
        }


        else if (currentPlayer == PlayerStats.Player.THREE)
        {
            if (completedRound >= 3)
            {
                completedRound = 0;
                currentPlayer = PlayerStats.Player.ONE;
            }
            else
            {
                currentPlayer = PlayerStats.Player.FOUR;
                completedRound++;
            }
        }


        else if (currentPlayer == PlayerStats.Player.FOUR)
        {
            if (completedRound >= 3)
            {
                completedRound = 0;
                currentPlayer = PlayerStats.Player.TWO;
            }
            else
            {
                currentPlayer = PlayerStats.Player.ONE;
                completedRound++;
            }
        }

    }



}
