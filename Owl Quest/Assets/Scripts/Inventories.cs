using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static backend;
using UnityEngine.EventSystems;

public class Inventories : MonoBehaviour
{
    public int player;
    public GameObject Panel;
    private GameObject inventory;
    private bool makeActive;
	public backend b;
	 Text [] newText ;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            inventory = Panel.transform.GetChild(i).gameObject;
            inventory.gameObject.SetActive(false);
            makeActive = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider != null && hit.collider.name == name)
            {
                if (hit.collider.gameObject.tag == "Owl")
                {
                    if (!Panel.transform.GetChild(player - 1).gameObject.activeSelf)
                    {
                        makeActive = true;
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        inventory = Panel.transform.GetChild(i).gameObject;
                        inventory.gameObject.SetActive(false);
                    }

                    if (makeActive)
                    {
                        inventory = Panel.transform.GetChild(player - 1).gameObject;
						/*
						newText = inventory.GetComponentsInChildren<Text> ();
						newText [0].text = "name";
						newText [1].text = "health";
						newText [2].text = "attack";
						newText [3].text = "description";
						*/
                        inventory.gameObject.SetActive(true);
                        makeActive = false;
                    }
                }
            }
        }
    }
}
