using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//Displays correct corresponding card to given player inventory
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
public class InventoryQuestCard : MonoBehaviour
{

    public backend b;

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Given player number, get the correct card values
    //send those values to an update function for visuals to be updated accordingly
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void GetInventoryCards(int player, GameObject inventoryCard, int spot, Animator animator)
    {
        Quests[] cardArray;
        Player playerValue;
		AI aiValue;
        inventoryCard.gameObject.SetActive(true);

        //set cardArray to correct inventory and playerValue to correct player
        if (player == 1)
        {
            cardArray = b.player1.completedQuests;
            playerValue = b.player1;
        }
        else if (player == 2)
        {
			if(StaticStart.numberOfPlayers > 1){
				cardArray = b.player2.completedQuests;
				playerValue = b.player2;
			}else{
				cardArray = b.AI2.completedQuests;
				aiValue = b.AI2;
			}
        }
        else if (player == 3)
        {
			if(StaticStart.numberOfPlayers > 2){
				cardArray = b.player3.completedQuests;
				playerValue = b.player3;
			}else{
				cardArray = b.AI3.completedQuests;
				aiValue = b.AI3;
			}
        }else{
			if(StaticStart.numberOfPlayers > 3){
				cardArray = b.player4.completedQuests;
                playerValue = b.player4;
			}else{
				cardArray = b.AI4.completedQuests;
				aiValue = b.AI4;
			}
        }

        //called to update the visuals
        Updatethecards(animator, cardArray[spot], spot);

    }


    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Displays correct corresponding card to given player inventory
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void Updatethecards(Animator animator, Quests cardArray, int spot)
    {
        int difficulty;

        //shows blank cards when there is nothing in the array
        if (cardArray == null)
        {
            animator.SetInteger("AnimState", 10);
            animator.SetInteger("Difficulty", 10);
            return;
        }

        for (int i = 0; i < 20; i++)
        {
            if (cardArray == b.questList[i])
            {
                if (i < 7)
                {
                    difficulty = 0;
                }
                else if (i < 14)
                {
                    difficulty = 1;
                    i = i - 7;
                }
                else
                {
                    difficulty = 2;
                    i = i - 14;
                }
                animator.SetInteger("AnimState", i); //indicates which card on a page to show
                animator.SetInteger("Difficulty", difficulty); //indicates if the card is on the easy, medium, or hard page
                return;
            }
        }
    }

}
