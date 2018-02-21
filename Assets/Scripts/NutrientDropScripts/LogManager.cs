using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LogManager : MonoBehaviour {

	public GameObject currentlyActiveTab = null;

	// Use this for initialization
	void Start () 
	{
	}

	public void SetActiveTab(GameObject selectedTab)
	{
		//Close current tab
		if (currentlyActiveTab != null)
		{
			currentlyActiveTab.SetActive(false);
		}

		//Open selected tab
		selectedTab.SetActive(true);
		currentlyActiveTab = selectedTab;
	}
}
