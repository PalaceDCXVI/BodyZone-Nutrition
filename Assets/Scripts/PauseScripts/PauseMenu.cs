using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public UnityEvent OnPauseEvents; //Events that occur when the game pauses.

	public UnityEvent OnResumeEvents; //Events that occur when the game resumes from pause.

	private int pauseCounter; //At 0, the game is running, above 0, the game is paused.

	void OnEnable() //I'm not sure if this is the way I want to do this, but as it is it's not bad.
	{
		RequestPause();
	}

	public void RequestPause()
	{
		if (pauseCounter == 0) //Game is active and should pause
		{
			OnPauseEvents.Invoke();
		}
		pauseCounter++;
	}

	public void RequestResume()
	{
		if (pauseCounter == 1) //Game should resume and there is no other pause requests active.
		{
			OnResumeEvents.Invoke();
		}
		pauseCounter--;
	}
}
