using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//Script for a dialogue popup. Handles its creation, scale, text content and various behavioiurs.
public class PopupDialogue : MonoBehaviour 
{
	public Text popupText;
	public Button popupButton;
	public string dialogueString; 

	public float timeUntilClickable = 1.0f;
	public bool killOnClickable = false;

	public bool flipHorizontal = false;
	public bool flipVertical = false;

	// Use this for initialization
	void Start () 
	{
		popupText.text = dialogueString.Replace("\\n", "\n");

		if (flipHorizontal || flipVertical)
		{
			GetComponent<RectTransform>().localScale = new Vector3(1.0f * (flipHorizontal ? -1.0f : 1.0f), 1.0f * (flipVertical ? -1.0f : 1.0f), 1.0f);
			popupText.GetComponent<RectTransform>().localScale = new Vector3(1.0f * (flipHorizontal ? -1.0f : 1.0f), 1.0f * (flipVertical ? -1.0f : 1.0f), 1.0f);
			RectTransform popupButtonTransform = popupButton.GetComponent<RectTransform>();
			popupButtonTransform.localScale = new Vector3(1.0f * (flipHorizontal ? -1.0f : 1.0f), 1.0f * (flipVertical ? -1.0f : 1.0f), 1.0f);
			popupButtonTransform.anchoredPosition = new Vector2(0, popupButtonTransform.anchoredPosition.y * popupButtonTransform.localScale.y);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeUntilClickable -= Time.fixedDeltaTime;
		if (timeUntilClickable <= 0.0f)
		{
			if (killOnClickable)
			{
				Destroy(gameObject);
			}
			popupButton.gameObject.SetActive(true);
		}

	}	

	public void DeletePopup()
	{
		if (timeUntilClickable <= 0.0f)
		{
			Destroy(gameObject);
		}
	}
}