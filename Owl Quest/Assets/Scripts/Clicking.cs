using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Clicking : MonoBehaviour
{
	public int spot;
	public backend b;
    public Turn turns;
    public GameObject c;
    public GameObject button;

    // Update is called once per frame
    void Update()
    {
        //determine if the click is valid
        if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection (ray, Mathf.Infinity);
			if (hit.collider != null && hit.collider.name == name) {

                //functionality if clicking on location object
                if (hit.collider.gameObject.tag == "Location")
                {
                    if (!b.completedAction && !b.questLocation)
                    {
                        TurnDefs.Player currentTurn = b.turn.GetCurrentTurn();
                        if (currentTurn == TurnDefs.Player.ONE)
                        {
                            b.player1.ClickRound(spot);
                        }
                        else if (currentTurn == TurnDefs.Player.TWO)
                        {
                            b.player2.ClickRound(spot);
                        }
                        else if (currentTurn == TurnDefs.Player.THREE)
                        {
                            b.player3.ClickRound(spot);
                        }
                        else if (currentTurn == TurnDefs.Player.FOUR)
                        {
                            b.player4.ClickRound(spot);
                        }
                    }
                }

                //functionality if clicking on quests
                else
                {
                    if (!b.completedAction && b.questLocation)
                    {
                        TurnDefs.Player currentTurn = b.turn.GetCurrentTurn();
                        if (currentTurn == TurnDefs.Player.ONE)
                        {
                            b.player1.ClickHandleQuests(spot);
                        }
                        else if (currentTurn == TurnDefs.Player.TWO)
                        {
                            b.player2.ClickHandleQuests(spot);
                        }
                        else if (currentTurn == TurnDefs.Player.THREE)
                        {
                            b.player3.ClickHandleQuests(spot);
                        }
                        else if (currentTurn == TurnDefs.Player.FOUR)
                        {
                            b.player4.ClickHandleQuests(spot);
                        }
                    }

                    else
                    {
                        if (c.activeSelf)
                        {
                            c.gameObject.SetActive(false);
                            button.gameObject.SetActive(false);
                        }
                        else
                        {
                            c.gameObject.SetActive(true);
                            button.gameObject.SetActive(true);
                        }
                    }
                }
             
			}
		}
    }
}
