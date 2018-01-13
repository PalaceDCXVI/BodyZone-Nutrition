using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//Script for general popup.
public class Popup : MonoBehaviour {

	public Text TitleText;
	public Text PopupDescription;
	public Button CancelButton;
	public Button AcceptButton;

	public void SetTitleText(string titleText)
	{
		TitleText.text = titleText.Replace("\\n", "\n");
	}

	public void SetDescription(string descriptionText)
	{
		PopupDescription.text = descriptionText.Replace("\\n", "\n");
	}

	//Accepts a button clicked function for the accept button in order to add any additional functionality to any popup button's accept button.
	public void SetAcceptFunction(UnityEngine.UI.Button.ButtonClickedEvent functionName)
	{
		AcceptButton.onClick = (functionName);
	}

	public void DestroyPopup()
	{
		Destroy(gameObject);
	}
}
