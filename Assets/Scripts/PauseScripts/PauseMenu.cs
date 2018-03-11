using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//The game object will need to be set to active at the start of the scene to work properly.
//It will set itself to inactive immediately.
public class PauseMenu : MonoBehaviour {

	public static PauseMenu inst;

	public UnityEvent OnPauseEvents; //Events that occur when the game pauses.

	public UnityEvent OnResumeEvents; //Events that occur when the game resumes from pause.

	[Tooltip("At 0, the game is running, above 0, the game is paused.")]
	public int pauseCounter; //At 0, the game is running, above 0, the game is paused.



	void Awake()
	{
		if(inst==null) inst=this;
		else {
			Debug.Log("PauseMenu destroyed on '"+gameObject.name+"'. Are there duplicates in the scene?");
			DestroyImmediate(this);
		}

		
	}

	public void RequestPause()
	{
		if (pauseCounter == 1) //Game is active and should pause
		{
			OnPauseEvents.Invoke();
		}
		pauseCounter++;
	}

	public void RequestResume()
	{
		pauseCounter--;
		if (pauseCounter <= 1) //Game should resume and there is no other pause requests active.
		{
			pauseCounter = 1;
			OnResumeEvents.Invoke();
		}
	}

	public void quitGame()
	{
		Application.Quit();
	}
}
