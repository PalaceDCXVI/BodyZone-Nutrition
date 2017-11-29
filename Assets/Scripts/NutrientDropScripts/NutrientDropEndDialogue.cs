using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NutrientDropEndDialogue : MonoBehaviour {

	public NutrientDropState dropState;

	public Text popupText;

	public string GreatText;
	public string GoodText;
	public string PoorText;
	public string FailedText;

	private int goodChoices = 0;

	private int badChoices = 0;
	// Use this for initialization
	void Start () 
	{
		foreach (bool item in dropState.foodList)
		{
			if (item)
			{
				goodChoices++;
			}
			else
			{
				badChoices++;
			}
		}	

		if (goodChoices >= dropState.endSize)
		{
			popupText.text = GreatText;
		}
		else if (goodChoices > badChoices)
		{
			popupText.text = GoodText;
		}
		else if (goodChoices <= badChoices && goodChoices != 0)
		{
			popupText.text = PoorText;
		}
		else
		{
			popupText.text = FailedText;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
