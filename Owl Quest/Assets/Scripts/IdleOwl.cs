using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleOwl : MonoBehaviour
{
    Animator animator;
    public int playerNumber;
    public backend b;
    int tester;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        tester = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i<6; i++)
        {
            if(b.occupied[i] == playerNumber) {
                animator.SetInteger("AnimState", 0);
            }
            else {
                tester++;
            }
        }
        if (tester > 5) {
            animator.SetInteger("AnimState", playerNumber);
        }
        tester = 0;

    }
}
