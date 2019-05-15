using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//Code that allows easy camera scaling
//Found via unity tutorial, do not claim code as own
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

[ExecuteInEditMode]
public class ScaleCamera : MonoBehaviour
{
    public int targetWidth = 640;
    public float pixelsToUnits = 100;
   

    // Update is called once per frame
    void Update()
    {
        int height = Mathf.RoundToInt(targetWidth / (float)Screen.width * Screen.height);
        GetComponent<Camera>().orthographicSize = height / pixelsToUnits / 2;
    }
}
