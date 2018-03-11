using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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

	LogSerializer logSerializer = new LogSerializer();
	private const string filePath = "FoodItems.xml";
	public NutrientFactsTable factsTable;
	public Text FoodNameText;
	public Text FoodDescriptionText;
	public Text FoodIngredientsText;

	List<LogItem> foodItemsInChildren = new List<LogItem>();

	const int HasBeenFoundIndex = 1;
	const int HasBeenClickedIndex = 2;

	//Items collected during Nutrient Drop
	public static List<Image> itemsCollected = new List<Image>();

	public static void ClearItemsCollected()
	{
		itemsCollected.Clear();
	}

	public static void AddItemToCollection(Image item)
	{
		itemsCollected.Add(item);
	}

	void Start()
	{
		//Load the xml database
		
		logSerializer.Load(Application.persistentDataPath + filePath);
	

		//Find all the log items in the on screen database.
		GetComponentsInChildren<LogItem>(true, foodItemsInChildren);

		//Ensure that all log items are in the serialized list.
		logSerializer.CompleteList(foodItemsInChildren);

		//Go through the list and set the found items to... found.
		foreach (LogItem item in foodItemsInChildren)
		{
			foreach (LogSerializer.SerialLogItem seralizedLogItem in logSerializer.foodItems)
			{
				if (item.name == seralizedLogItem.name)
				{
					if (seralizedLogItem.IsClicked)
					{
						item.SetBeenClicked(true);
					}
					else
					{
						item.SetBeenClicked(false);
					}

					if (seralizedLogItem.IsFound)
					{
						item.RevealItem();						
					}
					else
					{
						item.HideItem();
					}

				}
			}			
		}

		RevealCollectedItems();
	}

	

	public void SetItemFoundInDatabase(LogItem logItem)
	{
		LogSerializer.SerialLogItem serialLogItem;
		for (int i = 0; i < logSerializer.foodItems.Count; i++)
		{
			if (logSerializer.foodItems[i].name == logItem.name)
			{
				serialLogItem = logSerializer.foodItems[i];
				serialLogItem.IsFound = true;
				logSerializer.foodItems[i] = serialLogItem;
			}
		}
	}

	public void SetItemClickedInDatabase(LogItem logItem)
	{
		LogSerializer.SerialLogItem serialLogItem;
		for (int i = 0; i < logSerializer.foodItems.Count; i++)
		{
			if (logSerializer.foodItems[i].name == logItem.name)
			{
				serialLogItem = logSerializer.foodItems[i];
				serialLogItem.IsClicked = true;
				logSerializer.foodItems[i] = serialLogItem;
			}
		}
	}

	//Compare the items collected with items in the list, unlock if they exit and are not already unlocked.
	public void RevealCollectedItems()
	{
		foreach (Image collectedItem in itemsCollected)
		{
			foreach (LogItem logItem in foodItemsInChildren)
			{
				if (logItem.logImage.sprite == collectedItem.sprite)
				{
					if (logItem.hasBeenFound)
					{
						break;
					}
					else
					{
						SetItemFoundInDatabase(logItem);
						logItem.RevealItem();
					}
				}
			}
		}

		ClearItemsCollected();
	}

	void OnDestroy()
	{
		logSerializer.Save(Application.persistentDataPath + filePath);
	}
}
