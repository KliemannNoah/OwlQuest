using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryOwl : MonoBehaviour
{
    Animator animator;
    //~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Initializes the animator
    //~~~~~~~~~~~~~~~~~~~~~~~~~~
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Alters what owl appears on the victory screen according to who has won the game
    //~~~~~~~~~~~~~~~~~~~~~~~~~~
    void Update()
    {
        int winner = StaticStart.winningPlayer;
        animator.SetInteger("AnimState", winner - 1);
    }
}
