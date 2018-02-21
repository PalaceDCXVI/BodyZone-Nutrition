using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogItem : MonoBehaviour {

	public Image logImage;
	public Image GlowOverlay;

	bool hasBeenFound = false;
	bool hasBeenClicked = false;
	bool isClicked = false;

	public string ItemText;
	public string ItemDescription;

	public NutrientFactsTable.NutrientFactsData factsData;

	public LogDatabase logDatabase;

	// Use this for initialization
	void Start () 
	{
	}
	
	public void SetBeenClicked(bool clicked)
	{
		hasBeenClicked = clicked;

		if (hasBeenClicked)
		{
			GlowOverlay.gameObject.SetActive(false);
			logDatabase.SetItemClickedInDatabase(this);
		}
		else
		{
			GlowOverlay.gameObject.SetActive(true);
		}
	}

	public void SetIsCurrentlyClicked(bool isCurrentlyClicked)
	{
		isClicked = isCurrentlyClicked;

		if (isClicked)
		{
			//Dim the image.
		}
		else
		{
			//Set it to normal.
		}
	}

	public void RevealItem()
	{
		hasBeenFound = true;
		logImage.color = Color.white;
		GetComponent<Button>().interactable = true;
	}

	public void HideItem()
	{
		hasBeenFound = false;
		logImage.color = Color.black;
		GetComponent<Button>().interactable = false;
		GlowOverlay.gameObject.SetActive(false);
	}

	//For use in the FoodDatabase scene, to pass the facts table ui prefab the data from this item.
	public void SetNutritionFactsTable()
	{
		if (logImage.color != Color.black)
		{
			logDatabase.factsTable.SetData(ref factsData);
			logDatabase.FoodNameText.text = ItemText;
			logDatabase.FoodDescriptionText.text = ItemDescription.Replace(". ", ".\n").Replace("! ", "!\n");
		}
	}
}
