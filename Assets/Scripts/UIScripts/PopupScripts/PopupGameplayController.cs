using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopupGameplayController : MonoBehaviour {

	public UnityEvent unpauseEvents;
	public UnityEvent pauseEvents;

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
}
