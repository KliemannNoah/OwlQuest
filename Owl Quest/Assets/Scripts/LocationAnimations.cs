using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//~~~~~~~~~~~~~~~~~~~~~~~~~~
//Controls the juice of locations (animations):
//Growing/shrinking of locations
//Transparency if a location is selected
//Rotating of quest board star
//~~~~~~~~~~~~~~~~~~~~~~~~~~

public class LocationAnimations : MonoBehaviour
{
    Renderer renderer;
    public int location;
    public backend b;
    float timer = 0f;
    private int toggle = 1;


    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void FixedUpdate()
    {
        
        
        if (b.occupied[location] == 0)
        {
            //keep opaque if not selected
            renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            //rotate the quest board star at a constant rate
            if (location == 5)
            {
                    transform.Rotate(0f, 0f, Time.deltaTime * 30);
                    timer += Time.deltaTime;
            }
        }
        else
        {
            //turn location slightly transparent if selected
            renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);

        }

        //trading post location
        if (location == 4)
        {
            //location is growing
            if (toggle >= 0 && toggle <= 80)
            {
                toggle += 1;
                transform.localScale += new Vector3(.00004f, .00004f, 0);
            }

            //location is shrinking
            else if (toggle < 0)
            {
                toggle += 1;
                transform.localScale -= new Vector3(.00004f, .00004f, 0);
            }

            //toggle variable is reset so the cycle can start again
            else if (toggle > 80)
            {
                toggle = -80;
            }
        }

        //all other locations
        else { 

            //location is growing
            if (toggle >= 0 && toggle <= 80)
            {
                toggle += 1;
                transform.localScale += new Vector3(.0002f, .0002f, 0);
            }

            //location is shrinking
            else if (toggle < 0)
            {
                toggle += 1;
                transform.localScale -= new Vector3(.0002f, .0002f, 0);
            }

            //toggle variable is reset so the cycle can start again
            else if (toggle > 80)
            {
                toggle = -80;
            }
        }
    }

}
