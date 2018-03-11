using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LogScreenInterface : MonoBehaviour {

	public static LogScreenInterface inst;

	public PauseMenu pauseMenu = null;

	public Button logButton = null;
	public Button pauseButton = null;

	void Awake()
	{
		if(inst==null) inst=this;
		else {
			Debug.Log("LogScreenInterface destroyed on '"+gameObject.name+"'. Are there duplicates in the scene?");
			DestroyImmediate(this);
		}
	}

	void Start()
	{
		//TODO: DELETE THIS BEFORE RELEASE
		File.Delete(Application.persistentDataPath + "/FoodItems.xml");

		//pauseMenu = PauseMenu.inst;
		if (pauseMenu == null)
		{
			Debug.Log("pauseMenu not found in " + gameObject.name);
		}

		if (logButton == null)
		{
			Debug.Log("LogButton has not been set in " + gameObject.name);
		}		

		if (pauseButton == null)
		{
			Debug.Log("pauseButton has not been set in " + gameObject.name);
		}
	}
	
	public void RequestPauseFromPauseMenu()
	{
		pauseMenu.RequestPause();
	}

	public void RequestResumeFromPauseMenu()
	{
		pauseMenu.RequestResume();
	}

	public void SetButtonsInteractable(bool interactable = true)
	{
		logButton.interactable = interactable;
		pauseButton.interactable = interactable;
	}
}
