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

	public void StartLevel(ND_LevelInput _levelInput) {
		m_levelInput=_levelInput;
		GameplayController.inst.StartGame();
		DialogueManager.inst.StartDialogue(m_levelInput.m_dialogues[0]);
	}
}
