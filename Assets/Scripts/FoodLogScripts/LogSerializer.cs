using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("FoodItemsRoot")]
public class LogSerializer{//} : MonoBehaviour {


	public class SerialLogItem
	{
		[XmlAttribute("Name")]
		public string name;

		[XmlAttribute("HasBeenFound")]
		public bool IsFound = false;
		
		[XmlAttribute("HasBeenClicked")]
		public bool IsClicked = false;
	}

	[XmlArray("FoodItems")]
	[XmlArrayItem("FoodItem")]
	public List<SerialLogItem> foodItems = new List<SerialLogItem>();

	public void Save(string path) //Application.persistentDataPath + "/Databases/FoodItems.xml"
	{
		XmlSerializer serializer = new XmlSerializer(typeof(LogSerializer));
		FileStream stream = new FileStream(path, FileMode.Create);
		serializer.Serialize(stream, this);
		stream.Close();
	}

	public void Load(string path) //Application.persistentDataPath + "/Databases/FoodItems.xml"
	{
		XmlSerializer serializer = new XmlSerializer(typeof(LogSerializer));
	 	FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
		try
		{
	 		LogSerializer container = serializer.Deserialize(stream) as LogSerializer;
			foodItems = container.foodItems;
		}
		catch (System.Xml.XmlException)
		{
			//Not really a worry.
 			stream.Close();		
			return;	
		}
 		stream.Close();
	}

	public void CompleteList(List<LogItem> logItems)
	{
		SerialLogItem newLogItem = null;

		foreach (LogItem item in logItems)
		{
			foreach (SerialLogItem serialItem in foodItems)
			{
				if (serialItem.name == item.name)
				{
					newLogItem = serialItem;
					break;
				}
			}

			if (newLogItem == null)
			{
				newLogItem = new SerialLogItem();
				newLogItem.name = item.name;
				newLogItem.IsFound = false;
				newLogItem.IsClicked = false;
				foodItems.Add(newLogItem);
			}

			newLogItem = null;
		}
	}
}
