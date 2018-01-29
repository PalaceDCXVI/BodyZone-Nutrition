using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutrientDropState : MonoBehaviour {

	
	public List<bool> foodList;

	private GameplayController gameplayController;


	// Use this for initialization
	void Start () 
	{
		foodList = new List<bool>();
		gameplayController = GetComponent<GameplayController>();
	}
	

	
	public GameplayController.GameState GetGameState()
	{
		return gameplayController.currentGameState;
	}

}
