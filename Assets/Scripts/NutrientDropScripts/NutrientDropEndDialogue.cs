using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NutrientDropEndDialogue : MonoBehaviour {

	public NutrientDropState dropState;

	public Text popupText;

	public string EndText;

	// Use this for initialization
	void Start () 
	{
		popupText.text = EndText;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
