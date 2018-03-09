using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogTab : MonoBehaviour {

	List<LogItem> ItemsOnPage = new List<LogItem>();

	void Awake()
	{
		GetComponentsInChildren<LogItem>(ItemsOnPage);	
		SetTabActive(false);
	}

	// Use this for initialization
	void Start () 
	{

	}

	public void SetTabActive(bool active)
	{
		foreach (LogItem item in ItemsOnPage)
		{
			item.gameObject.SetActive(active);
		}

		if (active)
		{
			GetComponentInChildren<Canvas>().sortingOrder = 4;
		}
		else
		{
			GetComponentInChildren<Canvas>().sortingOrder = 3;
			
		}
	}

}
