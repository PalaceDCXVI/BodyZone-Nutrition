using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This will be a script where scripts that need to be communicated with in the main scene can be collected and found.
public class FoodDropInterface : MonoBehaviour {

	public string FoodDropSceneName;

	//Pause Menu
	PauseMenu pauseMenu = null;

	// Use this for initialization
	void Start () {
		pauseMenu = PauseMenu.inst;
		if (pauseMenu == null)
		{
			Debug.Log("PauseMenu not found in " + gameObject.name);
			return;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void RequestPauseFromPauseMenu()
	{
		pauseMenu.RequestPause();
	}

	public void RequestResumeFromPauseMenu()
	{
		pauseMenu.RequestResume();
	}
}
