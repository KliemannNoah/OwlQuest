using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
	
	public bool isStart;
	public bool isQuit;
	public int playerNumber = 0;
	
	public void PlayGame() {
		if (isQuit) {
			Application.Quit();
		} if(isStart) {
			StaticStart.numberOfPlayers = playerNumber;
			SceneManager.LoadScene("MainScene");		
			GetComponent<Renderer>().material.color=Color.green;
		}
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection (ray, Mathf.Infinity);
			if (hit.collider != null && hit.collider.name == name) {
                if (hit.collider.gameObject.tag == "Owl1"){
                    playerNumber = 1;
					PlayGame();
				}else if (hit.collider.gameObject.tag == "Owl2"){
                    playerNumber = 2;
					PlayGame();					
				}else if (hit.collider.gameObject.tag == "Owl3"){
                    playerNumber = 3;
					PlayGame();					
				}else if (hit.collider.gameObject.tag == "Owl4"){
                    playerNumber = 4;
					PlayGame();					
				}
			}
		}
    }
}
