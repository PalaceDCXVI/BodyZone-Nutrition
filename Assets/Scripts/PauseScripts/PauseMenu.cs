using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public UnityEvent OnPauseEvents; //Events that occur when the game pauses.

	public UnityEvent OnResumeEvents; //Events that occur when the game resumes from pause.

	[Tooltip("At 0, the game is running, above 0, the game is paused.")]
	public int pauseCounter; //At 0, the game is running, above 0, the game is paused.

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
		if (pauseCounter <= 1) //Game should resume and there is no other pause requests active.
		{
			pauseCounter = 1;
			OnResumeEvents.Invoke();
		}
		pauseCounter--;
	}
}
