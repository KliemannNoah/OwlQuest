using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPictures : MonoBehaviour
{
    Animator animator; //gets animator
    public int questSpot; //tells script what location spot is at
    public backend b;
    private int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 20; i++)
        {
            if(b.questList[i] == b.jobBoard[questSpot])
            {
                if(i < 7)
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
                animator.SetInteger("AnimState",i);
                animator.SetInteger("Difficulty",difficulty);
                break;
            }
        }

    }
}
