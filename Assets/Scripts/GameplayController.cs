using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayController : MonoBehaviour {

	public bool PauseOnStart = false;
	public UnityEvent unpauseEvents;
	public UnityEvent pauseEvents;
	public UnityEvent dialogueEvents;

	public enum GameState
	{
		STANDARD = 0,
		CHALLENGE = 1
	}
	public GameState currentGameState = GameState.STANDARD;


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

	

	public void StartDialogue()
	{
		{
			dialogueEvents.Invoke();
		}
	}


	public void SetTimeScale(float timeScale) //In case this is going to be used in gameplay for pausing. Note that this applies to Time.deltaTime but not Time.fixedDeltaTime.
	{
		Time.timeScale = timeScale;
	}

	public void SetGameState(int newState)
	{
		currentGameState = (GameplayController.GameState)newState;
	}
}
