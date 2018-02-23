using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the game state within the Food Quiz levels.
/// </summary>

public class FQ_GameController:MonoBehaviour {
	public static FQ_GameController inst;
	public LevelInput				m_levelInput;		//Variables needed for the level. What types of food to choose from, what dialogue is spoken, etc.
	public Animator					m_animDialogue;

	private bool					mp_firstFrameStart=true;

	private void Awake() {
		if(inst==null) inst=this;
		else {
			Debug.Log("FQ_GameController destroyed on '"+gameObject.name+"'. Are there duplicates in the scene?");
			DestroyImmediate(this);
		}
	}
	void Start(){}
	private void FirstFrameStart() {
		//Delayed start functions on the first frame.
		mp_firstFrameStart=false;
		FindLevelInfo();
	}
	
	void Update(){
		if(mp_firstFrameStart) FirstFrameStart();
	}

	private void FindLevelInfo() {
		//Search for a LevelInfo object in the scene to begin the game with.
		//Debug.Log("FQ_GameController.FindLevelInfo.");
		GameObject[] _levelInfos=GameObject.FindGameObjectsWithTag("LevelInfo");

		if(_levelInfos.Length==0) {
			Debug.Log("ERROR: FQ_GameController.FindLevelInfo can't find any objects in the scene tagged \"LevelInfo\"");
		}
		else {
			bool _passedInfo=false;
			for(int i=0; i<_levelInfos.Length; i++) {
				if(_levelInfos[i].name=="PassedLevelInfo") {
					//Debug.Log("FQ_GameController.FIndLevelInfo has found PassedLevelInfo.");
					StartLevel(_levelInfos[i].GetComponent<LevelInput>());
					_passedInfo=true;
				}
			}
			if(!_passedInfo) {		//If passed level info doesn't exist, use test info in the scene.
				for(int i=0; i<_levelInfos.Length; i++) {
					if(_levelInfos[i].name=="TestLevelInfo") {
						//Debug.Log("FQ_GameController.FIndLevelInfo has found TestLevelInfo.");
						StartLevel(_levelInfos[i].GetComponent<LevelInput>());
					}
				}
			}

			//Can't destroy the old objects, or else the local copy m_levelInput will be null.
			//for(int i=_levelInfos.Length-1; i>=0; i--)
			//	DestroyImmediate(_levelInfos[i]);
		}
	}
	private GameObject[] FindSceneInfo() {
		//Search for a SceneInfo object in the scene to begin the game with.
		GameObject[] _sceneInfos=GameObject.FindGameObjectsWithTag("SceneInfo");

		if(_sceneInfos.Length==0) {
			//Debug.Log("ERROR: FQ_GameController.FindSceneInfo can't find any objects in the scene tagged \"SceneInfo\"");
			return null;
		}
		else {
			return _sceneInfos;
		}
	}
	public void StartLevel(LevelInput _levelInput){
		//Call this immediately after loading the scene. Pass the LevelInput, and the level will start with those values.
		m_levelInput=_levelInput;
		FQ_FoodLineupHandler.inst.PopulateFoodList();
		//ND_RobotHandler.inst.SetWantedFoods();
		//ND_LevelTimer.inst.SetupTimer();

		//Start the intro dialogue if it exists and isn't set to be skipped, else start the drop game.
		if((FindSceneInfo()==null)||(!FindSceneInfo()[0].GetComponent<SceneInfo>().m_skipIntro)) {
			if(FindDialogue(DIALOGUETYPE.FQ_INTRO, false)!=null) {
				DialogueManager.inst.StartConversation(FindDialogue(DIALOGUETYPE.FQ_INTRO), SpeechBubble.SPEECHBUBBLETYPE.ASSISTANT);
				//m_animDialogue.SetTrigger("Go_BottomIn");
			}
			else StartGame();
		}			
		else StartGame();
	}
	public void StartGame() {
		//Start the Food Quiz game after the intro dialogue.
		//Debug.Log("FQ_GameController.StartDropGame.");
		
	}

	public Conversation FindDialogue(DIALOGUETYPE _type, bool _logError=true) {
		//Finds a loaded dialogue to use by type.
		for(int i=0; i<m_levelInput.m_foodQuizLevelInput.m_dialogues.Count; i++) {
			if(m_levelInput.m_foodQuizLevelInput.m_dialogues[i].m_dialogueType==_type) return m_levelInput.m_foodQuizLevelInput.m_dialogues[i];
		}

		if(_logError) Debug.Log("ERROR: FQ_GameController.FindDialogue can't find dialogue of type \""+_type.ToString()+"\"");
		return null;
	}
}
