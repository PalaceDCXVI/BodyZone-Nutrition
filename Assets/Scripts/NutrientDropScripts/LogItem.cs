using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogItem : MonoBehaviour {

	Image logImage;
	Text logText;

	public string ItemText;

	// Use this for initialization
	void Start () {
		logImage = GetComponent<Image>();
		logText = GetComponentInChildren<Text>();
	}
	
	public void RevealItem()
	{
		logImage.color = Color.white;
		logText.text = ItemText;
	}
}
