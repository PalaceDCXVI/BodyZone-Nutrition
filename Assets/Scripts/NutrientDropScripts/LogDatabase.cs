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

	XmlDocument xmlDatabase;

	public NutrientFactsTable factsTable;
	public Text FoodNameText;
	public Text FoodDescriptionText;
	public Text FoodIngredientsText;

	List<LogItem> foodItemsInChildren = new List<LogItem>();

	const int HasBeenFoundIndex = 1;
	const int HasBeenClickedIndex = 2;

	void Start()
	{
		//Load the xml database
		xmlDatabase = new XmlDocument();
		xmlDatabase.Load(Application.dataPath + "/Databases/FoodItems.xml");
		Debug.Log("Database loaded");

		GetComponentsInChildren<LogItem>(true, foodItemsInChildren);

		//Go through the list and set the found items to... found.
		XmlNode nodeInXMLDoc;
		foreach (LogItem item in foodItemsInChildren)
		{
			nodeInXMLDoc = xmlDatabase.SelectSingleNode(string.Format("descendant::FoodItem[Name='{0}']", item.name));
			if (nodeInXMLDoc == null)
			{
				Debug.Log("item in database '" + item.name + "' was not found in xml document");
				continue;
			}

			//ItemNodeItself
			if (nodeInXMLDoc.ChildNodes[HasBeenFoundIndex].InnerText == "true")
			{
				item.RevealItem();
			}
			else
			{
				item.HideItem();
			}

			if (nodeInXMLDoc.ChildNodes[HasBeenClickedIndex].InnerText == "true")
			{
				item.SetBeenClicked(true);
			}
			else
			{
				item.SetBeenClicked(false);				
			}
		}

	}

	public void SetItemFoundInDatabase(LogItem logItem)
	{
		XmlNode itemNode = xmlDatabase.SelectSingleNode(string.Format("descendant::FoodItem[Name='{0}']", logItem.name));
		if (itemNode == null)
		{
			Debug.Log("food item '" + logItem.name + "' not found in database");
			return;
		}
		itemNode.ChildNodes[HasBeenFoundIndex].InnerText = "true"; //HasBeenFound is second in the child nodes
	}

	public void SetItemClickedInDatabase(LogItem logItem)
	{
		XmlNode itemNode = xmlDatabase.SelectSingleNode(string.Format("descendant::FoodItem[Name='{0}']", logItem.name));
		if (itemNode == null)
		{
			Debug.Log("food item '" + logItem.name + "' not found in database");
			return;
		}
		itemNode.ChildNodes[HasBeenClickedIndex].InnerText = "true"; //HasBeenClicked is third in the child nodes
	}

	void OnDestroy()
	{
		xmlDatabase.Save(Application.dataPath + "/Databases/FoodItems.xml");
	}
}
