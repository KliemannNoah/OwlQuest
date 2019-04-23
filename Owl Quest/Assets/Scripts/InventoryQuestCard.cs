using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryQuestCard : MonoBehaviour
{

    public backend b;

    public void GetInventoryCards(int player, GameObject inventoryCard, int spot, Animator animator)
    {
        Quests[] cardArray;
        Player playerValue;

        inventoryCard.gameObject.SetActive(true);

        if (player == 1)
        {
            cardArray = b.player1.completedQuests;
            playerValue = b.player1;
        }
        else if (player == 2)
        {
            cardArray = b.player2.completedQuests;
            playerValue = b.player2;
        }
        else if (player == 3)
        {
            cardArray = b.player3.completedQuests;
            playerValue = b.player3;
        }
        else
        {
            cardArray = b.player4.completedQuests;
            playerValue = b.player4;
        }

        Updatethecards(animator, cardArray[spot], spot);

    }


    //the animator
    public void Updatethecards(Animator animator, Quests cardArray, int spot)
    {
        int difficulty;
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
                animator.SetInteger("AnimState", i);
                animator.SetInteger("Difficulty", difficulty);
                return;
            }
        }
    }

}
