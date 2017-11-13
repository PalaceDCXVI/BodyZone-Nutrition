using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopupSpawner : MonoBehaviour {

	public GameObject popupObject;

	Popup popup;

	public string titleText = "Title Text";
	public string descriptionText = "Popup Description";
	public UnityEngine.UI.Button.ButtonClickedEvent passedDelegates;

	public void SpawnPopup()
	{
		popup = Instantiate(popupObject).GetComponent<Popup>();
		popup.SetTitleText(titleText);
		popup.SetDescription(descriptionText);

		popup.SetAcceptFunction(passedDelegates);
	}
}
