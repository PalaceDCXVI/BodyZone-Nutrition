using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LogManager : MonoBehaviour {

	[Tooltip("The currently active tab will be set to active when the menu begins")]
	public LogTab currentlyActiveTab = null;
	public LogItem currentlyClickedItem = null;

	// Use this for initialization
	void Start () 
	{
		currentlyActiveTab.SetTabActive(true);
	}

	public void SetActiveTab(LogTab selectedTab)
	{
		//Close current tab
		if (currentlyActiveTab != null)
		{
			currentlyActiveTab.SetTabActive(false);
		}

		//Open selected tab
		currentlyActiveTab = selectedTab;
		currentlyActiveTab.SetTabActive(true);
	}

	public void SetActiveItem(LogItem item)
	{
		if (currentlyClickedItem != null)
		{
			currentlyClickedItem.SetIsCurrentlyClicked(false);
		}

		currentlyClickedItem = item;
		currentlyClickedItem.SetIsCurrentlyClicked(true);
	}
}
