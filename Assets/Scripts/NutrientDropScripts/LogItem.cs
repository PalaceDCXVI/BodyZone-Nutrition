using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogItem : MonoBehaviour {

	Image logImage;
	Text logText;

	public string ItemText;

	public NutrientFactsTable.NutrientFactsData factsData;

	public LogDatabase logDatabase;

	// Use this for initialization
	void Start () {
		logImage = GetComponent<Image>();
		logText = GetComponentInChildren<Text>();
	}
	
	public void RevealItem()
	{
		//Reveal item in the log.
		if (logImage != null)
		{
			logText.text = ItemText;
			Animation animation = GetComponent<Animation>();
			if (animation != null)
			{
				GetComponent<Animation>().Play();
			}
			
			//Find game object for the wanted food and tell it to go to the next item.
			//TODO: for the love of god find a better way to do this.
			((WantedFood)GameObject.FindObjectOfType(typeof(WantedFood))).NextFood();
		}
	}

	//For use in the FoodDatabase scene, to pass the facts table ui prefab the data from this item.
	public void SetNutritionFactsTable()
	{
		if (logImage.color != Color.black)
		{
			logDatabase.factsTable.SetData(ref factsData);
		}
	}
}
