using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static backend;

public class Player1 : MonoBehaviour
{
	public backend b;
	
	public int food = 0;
	public int water = 0;
	public int shelter = 0;
	public int treasure = 0;
	public int points = 0;
	public Text PlayerQuests;
	public Quests[] completedQuests = new Quests[10];
	
    // Start is called before the first frame update
    void Start()
    {
        PlayerQuests.text  = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void completed(){
		PlayerQuests.text ="";
		for(int i = 0; i < 10; i++){
			if(completedQuests[i] != null){
			PlayerQuests.text += completedQuests[i].title + "\t" + completedQuests[i].water.ToString() + " Water \t"+ completedQuests[i].food.ToString() + " Food \t"+ completedQuests[i].shelter.ToString() + " Shelter \t" + completedQuests[i].treasure.ToString() + " Treasure \t" + completedQuests[i].points.ToString() + " points \n";
			}
		}
	}
}
