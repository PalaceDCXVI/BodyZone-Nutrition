using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LogScreenController : MonoBehaviour {

	public GameObject LogScreenResearcher;
	public Text LogScreenResearcherText;
	public Button ContinueButton; public bool ContinueClosesScreen = true;
	public Button.ButtonClickedEvent continueCloseScreen;
	public Button.ButtonClickedEvent continueGoToScene;


	// Use this for initialization
	void Start () {
		if (ContinueClosesScreen)
		{
			SetLogScreenDeactivates();
		}
		else
		{
			SetLogScreenGoesToScene();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetResearcherText(string text)
	{
		LogScreenResearcherText.text = text;
	}

	public void SetLogScreenDeactivates()
	{
		ContinueButton.onClick = continueCloseScreen;
	}

	public void SetLogScreenGoesToScene()
	{
		ContinueButton.onClick = continueGoToScene;
	}
}
