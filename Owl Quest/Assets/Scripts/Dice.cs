using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static backend;

public class Dice : MonoBehaviour {

	private int val;
	public int playerNumber;
	private int location;
    // Array of dice sides sprites to load from Resources folder
    private Sprite[] diceSides;
	public backend b;
	public Text Outcome;

    // Reference to sprite renderer to change sprites
    private SpriteRenderer rend;

	// Use this for initialization
	private void Start () {

        // Assign Renderer component
        rend = GetComponent<SpriteRenderer>();

        // Load dice sides sprites to array from DiceSides subfolder of Resources folder
        diceSides = Resources.LoadAll<Sprite>("Dice/");
	}
	
    // If you left click over the dice then RollTheDice coroutine is started
    private void OnMouseDown()
    {
        StartCoroutine("RollTheDice");
    }
	
	public void roll(int value){
		val = value;
		StartCoroutine("RollTheDice");
	}

    // Coroutine that rolls the dice
    private IEnumerator RollTheDice()
    {
		yield return new WaitForSeconds(0.5f);
        // Variable to contain random dice side number.
        // It needs to be assigned. Let it be 0 initially
        int randomDiceSide = 0;

        // Final side or value that dice reads in the end of coroutine
        int finalSide = 0;

        // Loop to switch dice sides ramdomly
        // before final side appears. 20 itterations here.
        for (int i = 0; i <= 18; i++)
        {
            // Pick up random value from 0 to 5 (All inclusive)
            randomDiceSide = Random.Range(0, 5);

            // Set sprite to upper face of dice from array according to random value
            rend.sprite = diceSides[randomDiceSide];

            // Pause before next itteration
            yield return new WaitForSeconds(0.05f);
        }
		location = System.Array.IndexOf(b.occupied, playerNumber);
        // Assigning final side so you can use this value later in your game
        // for player movement for example
		rend.sprite = diceSides[val-1];
        finalSide = val;
		if (finalSide >= b.probability[location] && location != 4){
			Outcome.text = "Success!\n+1 " + b.locationsText[System.Array.IndexOf(b.occupied, playerNumber)];
		}else if(location == 4){
			if (finalSide >= (4)) {
				Outcome.text = "Success!\n+1 " + b.locationsText[b.tradingResource];
				yield break;
			}
			else if (finalSide >= (3) && b.tradingResource < 2) {
				Outcome.text = "Success!\n+1 " + b.locationsText[b.tradingResource];
			}else{
				Outcome.text = "Failure";
			}
		}else{
			Outcome.text = "Failure";
		}
        // Show final dice value in Console
        //Debug.Log(finalSide);
    }
}
