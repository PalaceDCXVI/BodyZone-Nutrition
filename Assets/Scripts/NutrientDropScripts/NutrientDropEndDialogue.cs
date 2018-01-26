using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NutrientDropEndDialogue : MonoBehaviour {

	public Text textComponent;
	public Button buttonComponent;

	int goodFoodsCollected;
	int badFoodsCollected;

	public int requiredSampleSize;
	public int badFoodsLimit;

	public Button.ButtonClickedEvent WinEvent;
	public Button.ButtonClickedEvent FailEvent;

	// Use this for initialization
	void Start()
	{
		if ( requiredSampleSize > (goodFoodsCollected + badFoodsCollected))
		{
			textComponent.text = "Hmm, we didn't get a large enough sample size for the test, let's try again"; //Handle redoing the challenge section.
			buttonComponent.onClick = FailEvent;
			
			return;
		}

		if (badFoodsLimit < badFoodsCollected)
		{
			textComponent.text = "Hmm, my readings show me that the robot has consumed too many refined grains, let's try again";
			buttonComponent.onClick = FailEvent;

			return;
		}

		textComponent.text = "Fantastic! My readings show that the robot is in top shape thanks to your food choices! Let's move on!";
		buttonComponent.onClick = WinEvent;
	}

	public void AddItem(bool IsGood)
	{
		if (IsGood)
		{
			goodFoodsCollected++;
		}
		else
		{
			badFoodsCollected++;
		}
	}

	public void ResetCounters()
	{
		goodFoodsCollected = 0;
		badFoodsCollected = 0;
	}
}
