using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
	
	public bool isStart;
	public bool isQuit;
	
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
	
	
	public void PlayGame() {
		if (isQuit) {
			Application.Quit();
		} if(isStart) {
			SceneManager.LoadScene("MainScene");			
			GetComponent<Renderer>().material.color=Color.cyan;
		}
	}
}
