using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests
{
	public string title = "";
	public int food = 0;
	public int water = 0;
	public int shelter = 0;
	public int treasure = 0;
	public int points = 0;
	public int effect = 0;
	public string effectText = "";
    // Start is called before the first frame update
    void Start()
    {
		
    }

	public Quests(string titl, int wat, int foo, int shel, int trea, int poi, int eff, string efte){
		this.title = titl;
		this.water = wat;
		this.food = foo;
		this.shelter = shel;
		this.treasure = trea;
		this.points = poi;
		this.effect = eff;
		this.effectText = efte;
		
	}
    // Update is called once per frame
    void Update()
    {
		
    }
	

}
