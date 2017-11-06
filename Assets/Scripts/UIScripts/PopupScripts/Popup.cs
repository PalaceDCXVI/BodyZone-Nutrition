using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour {

	public Text TitleText;
	public Text PopupDescription;
	public Button CancelButton;
	public Button AcceptButton;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void SetTitleText(string titleText)
	{
		TitleText.text = titleText.Replace("\\n", "\n");
	}

	public void SetDescription(string descriptionText)
	{
		PopupDescription.text = descriptionText.Replace("\\n", "\n");
	}

	public void DestroyPopup()
	{
		Destroy(gameObject);
	}
}
