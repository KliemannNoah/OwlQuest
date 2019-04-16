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
			GetComponent<Renderer>().material.color=Color.cyan;
		}
	}
}
