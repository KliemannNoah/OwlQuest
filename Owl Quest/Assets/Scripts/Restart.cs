using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
	
	public bool isRestart;
	public bool isQuit;
	public Text Winner;
	
    // Start is called before the first frame update
    void Start()
    {
		if(isQuit){
			Winner.text = "Player "+ StaticStart.winningPlayer.ToString() + " WINS!";
		}
    }

    // Update is called once per frame
    void Update()
    {
		
    }
	
	
	public void PlayGame() {
		if (isQuit) {
			Application.Quit();
		} if(isRestart) {
			SceneManager.LoadScene("StartScreen");			
			GetComponent<Renderer>().material.color=Color.cyan;
		}
	}
}
