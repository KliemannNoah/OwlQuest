using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlPosition : MonoBehaviour
{
    Animator animator;
    public int owlLocation;
    public backend b;
    int value;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        value = b.occupied[owlLocation];
        animator.SetInteger("AnimState", value);
    }
}
