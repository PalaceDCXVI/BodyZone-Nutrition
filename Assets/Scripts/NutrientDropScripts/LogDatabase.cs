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

	void OnEnable()
	{
		//Load the xml database
		xmlDatabase = new XmlDocument();
		xmlDatabase.Load(Application.dataPath + "/Databases/FoodItems.xml");
		Debug.Log("Database loaded");

		List<Image> spritesInDatabase = new List<Image>();
		List<LogItem> foodItemsInChildren = new List<LogItem>();
		GetComponentsInChildren<LogItem>(foodItemsInChildren);

		//Get the sprites from the log items. Doing it the other way just adds most of the objects in the scene, so I'm just going to do it this way.
		foreach (LogItem item in foodItemsInChildren)
		{
			spritesInDatabase.Add(item.gameObject.GetComponent<Image>());
		}

		//Go through the list and set the found items to... found.
		XmlNode nodeInXMLDoc;
		foreach (Image item in spritesInDatabase)
		{
			nodeInXMLDoc = xmlDatabase.SelectSingleNode(string.Format("descendant::FoodItem[Name='{0}']", item.name));
			if (nodeInXMLDoc == null)
			{
				Debug.Log("item in database '" + item.name + "' was not found in xml document");
				item.color = Color.black;
				continue;
			}

			if (nodeInXMLDoc.LastChild.InnerText == "true")
			{
				item.color = Color.white;
			}
		}

		UpdateElements(); //Update new elements.
	}

	//Updates items in the database based on the itemsCollected variable in the NutrientDatabaseInterface.
	void UpdateElements()
	{
		List<Image> spritesInDatabase = new List<Image>();
		GetComponentsInChildren<Image>(spritesInDatabase);
		spritesInDatabase.RemoveAt(0);
		
		//Iterate through the list of sprites from the collection. For each item, go through the list of elements in the database and reveal them if not revealed.
		foreach (Image itemInCollected in NutrientDatabaseInterface.itemsCollected)
		{
			foreach (Image itemInDatabase in spritesInDatabase)
			{
				if (itemInCollected.sprite == itemInDatabase.sprite)
				{
					itemInDatabase.gameObject.GetComponent<LogItem>().RevealItem();

					//Select the xml node that matches the name of the item in the database scene
					//MAKE SURE THAT THE ITEMS IN THE DATABASE SCENE HAVE THE CORRECT NAME
					XmlNode itemNode = xmlDatabase.SelectSingleNode(string.Format("descendant::FoodItem[Name='{0}']", itemInDatabase.name));
					if (itemNode == null)
					{
						Debug.Log("food item '" + itemInDatabase.name + "' not found in database");
						continue;
					}
					itemNode.LastChild.InnerText = "true";
				}
			}
		}
	}

	void OnDestroy()
	{
		xmlDatabase.Save(Application.dataPath + "/Databases/FoodItems.xml");
	}
}
