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
    public Turn t;
	public GameObject Button1;
	public GameObject Button2;

    public GameObject InventoryCard;
    public GameObject card1;
    public GameObject card2;
    public GameObject card3;
    public GameObject card4;
    public GameObject card5;
    Animator a1;
    Animator a2;
    Animator a3;
    Animator a4;
    Animator a5;
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
        InventoryCard.gameObject.SetActive(false);
        a1 = card1.GetComponent<Animator>();
        a2 = card2.GetComponent<Animator>();
        a3 = card3.GetComponent<Animator>();
        a4 = card4.GetComponent<Animator>();
        a5 = card5.GetComponent<Animator>();
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
						Button1.SetActive(true);
						Button2.SetActive(true);
                        inventory = Panel.transform.GetChild(i).gameObject;
                        inventory.gameObject.SetActive(false);
                    }

                    if (makeActive)
                    {
						Button1.SetActive(false);
						Button2.SetActive(false);
                        inventory = Panel.transform.GetChild(player - 1).gameObject;
                        inventory.gameObject.SetActive(true);
                        InventoryCard.gameObject.SetActive(true);
                        GetInventoryCards(player);
                        makeActive = false;
                    }
                }
            }
        }

    }

    public void GetInventoryCards(int player) {
        Quests[] cardArray;
        Player playerValue;
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

        Updatethecards(a1, cardArray[0]);
        Updatethecards(a2, cardArray[1]);
        Updatethecards(a3, cardArray[2]);
        Updatethecards(a4, cardArray[3]);
        Updatethecards(a5, cardArray[4]);

    }



    public void Updatethecards(Animator animator, Quests cardArray)
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
