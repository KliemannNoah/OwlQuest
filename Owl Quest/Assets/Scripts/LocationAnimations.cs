using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationAnimations : MonoBehaviour
{
    Animator animator;
    Renderer renderer;
    public int location;
    public backend b;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(b.occupied[location] == 0)
        {
            animator.SetInteger("AnimState", 0);
            renderer.material.color = new Color(1.0f,1.0f,1.0f,1.0f);
        }
        else
        {
            animator.SetInteger("AnimState", 1);
            renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
    }
}
