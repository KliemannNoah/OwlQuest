using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationAnimations : MonoBehaviour
{
    Renderer renderer;
    public int location;
    public backend b;
    float timer = 0f;
    private int toggle = 1;


    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        if (b.occupied[location] == 0)
        {
                renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            if (location == 5)
            {
                    transform.Rotate(0f, 0f, Time.deltaTime * 30);
                    timer += Time.deltaTime;
            }
        }
        else
        {
            renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);

        }


        if (location == 4)
        {
            if (toggle >= 0 && toggle <= 80)
            {
                toggle += 1;
                transform.localScale += new Vector3(.00004f, .00004f, 0);
            }
            else if (toggle < 0)
            {
                toggle += 1;
                transform.localScale -= new Vector3(.00004f, .00004f, 0);
            }
            else if (toggle > 80)
            {
                toggle = -80;
            }
        }
        else { 
            if (toggle >= 0 && toggle <= 80)
            {
                toggle += 1;
                transform.localScale += new Vector3(.0002f, .0002f, 0);
            }
            else if (toggle < 0)
            {
                toggle += 1;
                transform.localScale -= new Vector3(.0002f, .0002f, 0);
            }
            else if (toggle > 80)
            {
                toggle = -80;
            }
        }
    }

}
