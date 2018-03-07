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
			GetComponent<Button>().colors = ColorBlock.defaultColorBlock;
		}
		else
		{
			ColorBlock newBlock = new ColorBlock();
			newBlock.normalColor = Color.grey;
			newBlock.highlightedColor = ColorBlock.defaultColorBlock.highlightedColor;
			newBlock.pressedColor = ColorBlock.defaultColorBlock.pressedColor;
			newBlock.colorMultiplier = 1;
			newBlock.fadeDuration = 0.1f;
			GetComponent<Button>().colors = newBlock;
			
		}
	}

}
