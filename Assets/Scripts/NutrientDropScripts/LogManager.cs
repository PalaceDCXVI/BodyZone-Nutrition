using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LogManager : MonoBehaviour {

	[Tooltip("The currently active tab will be set to active when the menu begins")]
	public LogTab currentlyActiveTab = null;
	public LogItem currentlyClickedItem = null;

	public LogTab[] tabs;

	public enum TabType
	{
		Grain = 0,
		Drink,
		MeatAlts
	}
	public static TabType activeTab = TabType.Grain;
	

	// Use this for initialization
	void Start () 
	{
		switch (activeTab)
		{
			case TabType.Grain:
			currentlyActiveTab = tabs[(int)TabType.Grain];
			break;
			case TabType.Drink:
			currentlyActiveTab = tabs[(int)TabType.Drink];
			break;
			case TabType.MeatAlts:
			currentlyActiveTab = tabs[(int)TabType.MeatAlts];
			break;

			default:
			break;
		}
		currentlyActiveTab.SetTabActive(true);
	}

	void SetActiveTabByType()
	{

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
