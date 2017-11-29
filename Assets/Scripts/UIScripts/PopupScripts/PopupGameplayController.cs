using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopupGameplayController : MonoBehaviour {

	public bool PauseOnStart = false;
	public UnityEvent unpauseEvents;
	public UnityEvent pauseEvents;


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
}
