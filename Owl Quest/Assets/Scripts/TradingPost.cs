using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//Sets up the correct card on the trading post
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
public class TradingPost : MonoBehaviour
{
    Animator animator;
    public backend b;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("AnimState", b.tradingResource);
    }
}
