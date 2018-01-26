using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopupGameplayController : MonoBehaviour {

	public bool PauseOnStart = false;
	public UnityEvent unpauseEvents;
	public UnityEvent pauseEvents;

	private UnityEvent endGameEvents;

	public UnityEvent endGameEventsStandard;

	public UnityEvent endGameEventsChallenge;

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

		switch (currentGameState)
		{
			case GameState.STANDARD:
			endGameEvents = endGameEventsStandard;
			break;

			case GameState.CHALLENGE:
			endGameEvents = endGameEventsChallenge;
			break;

			default:
			break;
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

	public void SetGameState(int newState)
	{
		currentGameState = (PopupGameplayController.GameState)newState;
	}
}
