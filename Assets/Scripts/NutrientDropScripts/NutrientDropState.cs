using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutrientDropState : MonoBehaviour {

	// Currently, contains whether the food is good or not. 
	// Fill this out with an actual data structure containing info on the food, which can then be used to determine the food's value
	public List<bool> foodList;
	public int endSize = 5;

	private PopupGameplayController gameplayController;

	public GameObject endGameWrap;

	// Use this for initialization
	void Start () 
	{
		foodList = new List<bool>();
		gameplayController = GetComponent<PopupGameplayController>();
	}
	

	public void AddFood(bool IsGoodFood)
	{
		foodList.Add(IsGoodFood);
	
		if (foodList.Count >= endSize && gameplayController.currentGameState == PopupGameplayController.GameState.STANDARD)
		{
			//endGame;
			gameplayController.EndGame();

			// Create end popup.
			endGameWrap.SetActive(true);
		}
	}

	public PopupGameplayController.GameState GetGameState()
	{
		return gameplayController.currentGameState;
	}

	public void EndGame()
	{
		//endGame;
		gameplayController.EndGame();

		// Create end popup.
		endGameWrap.SetActive(true);
	}
}
