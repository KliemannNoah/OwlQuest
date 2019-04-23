using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMusic : MonoBehaviour
{
    static bool AudioBegin = false;
    AudioSource audio;
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
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "VictoryScreen")
        {
            audio.Stop();
            AudioBegin = false;
        }
    }
}
