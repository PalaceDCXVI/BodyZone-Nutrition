using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopupGameplayController : MonoBehaviour {

	public bool PauseOnStart = false;
	public UnityEvent unpauseEvents;
	public UnityEvent pauseEvents;

	public UnityEvent endGameEvents;


	void Start()
	{
		if (PauseOnStart)
		{
			pauseEvents.Invoke();
		}
	}

	public void StartGame()
	{
		{
			unpauseEvents.Invoke(); //Unpause events?
		}
	}

	public void PauseGame()
	{
		{
			pauseEvents.Invoke(); //Pause events?
		}
	}

	public void EndGame()
	{
		{
			endGameEvents.Invoke(); //events at the end of the game.
		}
	}

	public void SetTimeScale(float timeScale) //In case this is going to be used in gameplay for pausing. Note that this applies to Time.deltaTime but not Time.fixedDeltaTime.
	{
		Time.timeScale = timeScale;
	}
}
