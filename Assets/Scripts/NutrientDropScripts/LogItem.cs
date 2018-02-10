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
		if (logImage != null)
		{
			logText.text = ItemText;
			Animation animation = GetComponent<Animation>();
			if (animation != null)
			{
				GetComponent<Animation>().Play();
			}
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
