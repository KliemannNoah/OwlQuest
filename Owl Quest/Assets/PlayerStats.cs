using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{

    public enum Player
    {
        ONE,
        TWO,
        THREE,
        FOUR
    }

    public string[] name;
    public float[] VP;
    public string[] location;
    public int[] resources; //Spot 0:water, Spot 1: food, Spot 2: shelter, Spot 3: treasure (16 spots total)
}
