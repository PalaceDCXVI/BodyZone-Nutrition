using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the game state within the nutrient drop levels.
/// </summary>

public class ND_GameController:MonoBehaviour {
	public static ND_GameController inst;
	public ND_LevelInput			m_levelInput;	//Variables needed for the level. What types of food spawn, what dialogue is spoken, etc.

	private void Awake() {
		if(inst==null) inst=this;
		else {
			Debug.Log("ND_GameController destroyed on '"+gameObject.name+"'. Are there duplicates in the scene?");
			DestroyImmediate(this);
		}
	}
	void Start(){}
	
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.Alpha1)) StartLevel((ND_LevelInput)FindObjectOfType(typeof(ND_LevelInput)));
	}

	public void StartLevel(ND_LevelInput _levelInput){
		//Call this immediately after loading the scene. Pass the LevelInput, and the level will start with those values.
		m_levelInput=_levelInput;
		NutrientSpawner.inst.SetSpawnerInfo(_levelInput.m_spawnerInfo);
		ND_RobotHandler.inst.SetWantedFoods();
		GameplayController.inst.StartGame();
		DialogueManager.inst.StartDialogue(m_levelInput.m_dialogues[0]);
	}
	public void EndLevelSuccess(){
		//The food catching part is completed.
		NutrientSpawner.inst.EndGameSuccess();
	}
	public void EndLevelRobotDead() {
		//Robot has eaten too many non-foods.
		NutrientSpawner.inst.EndGameRobotDead();
	}
}
