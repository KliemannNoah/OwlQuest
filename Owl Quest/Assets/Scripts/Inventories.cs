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
    private GameObject inventoryCards;
    private GameObject inventory;
    private bool makeActive;
	public backend b;
    public Turn t;
	public GameObject Button1;
	public GameObject Button2;

    public GameObject allPlayerCards;
    public GameObject playerCards;
    public GameObject currentCard;

    public InventoryQuestCard c;
    Text [] newText ;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            inventory = Panel.transform.GetChild(i).gameObject;
            inventory.gameObject.SetActive(false);
            makeActive = false;
        }

        playerCards.SetActive(false);

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
                        GameObject othercards = allPlayerCards.transform.GetChild(i).gameObject;
                        othercards.SetActive(false);
                    }

                    playerCards.SetActive(false);

                    if (makeActive)
                    {
						Button1.SetActive(false);
						Button2.SetActive(false);
                        inventory = Panel.transform.GetChild(player - 1).gameObject;
                        inventory.gameObject.SetActive(true);
                        playerCards.SetActive(true);
                        for (int i = 0; i < 5; i++)
                        {
                            currentCard = playerCards.transform.GetChild(i).gameObject;
                            Animator animator = currentCard.GetComponent<Animator>();
                            c.GetInventoryCards(player, currentCard, i, animator);
                        }
                        makeActive = false;
                    }
                }
            }
        }

    }

 
}
