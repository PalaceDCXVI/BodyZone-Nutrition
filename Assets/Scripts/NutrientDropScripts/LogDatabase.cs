using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;


public class LogDatabase : MonoBehaviour 
{
	enum FoodType
	{
		Food = 0,
		Drink = 1
	}

	enum FoodValue
	{
		Bad = 0,
		Good = 1
	}

	XmlDocument xmlDatabase;

	public NutrientFactsTable factsTable;

	void Start()
	{
		xmlDatabase = new XmlDocument();
		xmlDatabase.Load(Application.dataPath + "/Databases/FoodItems.xml");
		Debug.Log("Database loaded");

		//TODO: Check which objects in the file are revealed or not and set appropriately.
	}

	void UpdateElements(Image[] ItemsCollected)
	{
		//Iterate through the list of sprites from the collection. For each item, go through the list of elements in the database and reveal them if not revealed.
		List<Image> spritesInDatabase = new List<Image>();
		GetComponentsInChildren<Image>(spritesInDatabase);
		spritesInDatabase.RemoveAt(0);
		
		foreach (Image itemInCollected in ItemsCollected)
		{
			foreach (Image itemInDatabase in spritesInDatabase)
			{
				if (itemInCollected.sprite == itemInDatabase.sprite)
				{
					itemInDatabase.gameObject.GetComponent<LogItem>().RevealItem();
					//TODO: Reveal the object in the file database.
				}
			}
		}
	}
}
