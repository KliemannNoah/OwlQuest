using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{
	public string title = "";
	public int food = 0;
	public int water = 0;
	public int shelter = 0;
	public int treasure = 0;
	public int points = 0;
    // Start is called before the first frame update
    void Start()
    {
		
    }

	public Quests(string titl, int foo, int wat, int shel, int trea, int poi){
		this.title = titl;
		this.food = foo;
		this.water = wat;
		this.shelter = shel;
		this.treasure = trea;
		this.points = poi;
		
	}
    // Update is called once per frame
    void Update()
    {
		
    }
}
