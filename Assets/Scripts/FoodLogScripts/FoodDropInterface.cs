using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//This exists in the LogScreen as a way to communicate with the Nutrient Drop scene. I know the names are confusing a little.
public class FoodDropInterface : MonoBehaviour {

	LogScreenInterface logScreenInterface;

	//Pause Menu

	// Use this for initialization
	void Start () {
		logScreenInterface = LogScreenInterface.inst;
		if (logScreenInterface == null)
		{
			Debug.Log("logScreenInterface Has Not Been Found in " + gameObject.name);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void RequestPauseFromPauseMenu()
	{
		logScreenInterface.RequestPauseFromPauseMenu();
	}

	public void RequestResumeFromPauseMenu()
	{
		logScreenInterface.RequestResumeFromPauseMenu();
	}

	public void TurnFoodDropButtonsBackOn()
	{
		logScreenInterface.SetButtonsInteractable(true);
	}
}
