using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMusic : MonoBehaviour
{
    static bool AudioBegin = false;
    AudioSource audio;

    //~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Initializes the main music of the game
    //~~~~~~~~~~~~~~~~~~~~~~~~~~
    void Awake()
    {
        if (!AudioBegin)
        {
            audio = GetComponent<AudioSource>();
            audio.Play();
            DontDestroyOnLoad(gameObject);
            AudioBegin = true;
        }
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Checks for victory screen
    //Ends music if screen is detected
    //~~~~~~~~~~~~~~~~~~~~~~~~~~
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "VictoryScreen")
        {
            audio.Stop();
            AudioBegin = false;
        }
    }
}
