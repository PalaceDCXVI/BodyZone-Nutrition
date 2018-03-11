using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogItem : MonoBehaviour {

	public Image logImage;
	public Image GlowOverlay;
	public Image ShadowOverlay;

	public bool hasBeenFound = false;
	public bool hasBeenClicked = false;
	public bool isClicked = false;

	public string ItemText;
	public string ItemDescription;
	public string ItemIngredients;

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
			ShadowOverlay.gameObject.SetActive(true);//Dim the image.
		}
		else
		{
			ShadowOverlay.gameObject.SetActive(false);//Set it to normal.
		}
	}

	public void RevealItem()
	{
		hasBeenFound = true;
		logImage.color = Color.white;
		GetComponent<Button>().interactable = true;
		GlowOverlay.gameObject.SetActive(true);
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
			logDatabase.FoodIngredientsText.text = "Ingredients: " + ItemIngredients;
		}
	}
}
