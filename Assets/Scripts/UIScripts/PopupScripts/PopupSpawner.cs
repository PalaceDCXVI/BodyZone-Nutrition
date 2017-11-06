using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSpawner : MonoBehaviour {

	public GameObject popupObject;

	Popup popup;

	public string titleText = "Title Text";
	public string descriptionText = "Popup Description";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnPopup()
	{
		popup = Instantiate(popupObject).GetComponent<Popup>();
		popup.SetTitleText(titleText);
		popup.SetDescription(descriptionText);
	}
}
