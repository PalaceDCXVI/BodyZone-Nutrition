using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the game state within the nutrient drop levels.
/// </summary>

public class ND_GameController:MonoBehaviour {
	public static ND_GameController inst;
	public ND_LevelInput			m_levelInput;		//Variables needed for the level. What types of food spawn, what dialogue is spoken, etc.
	public GameObject				m_pre_SceneInfo;	//Prefab for scene info.

	private bool	mp_firstFrameStart=true;			//Helps the FirstFrameStart to run.

	private void Awake() {
		if(inst==null) inst=this;
		else {
			Debug.Log("ND_GameController destroyed on '"+gameObject.name+"'. Are there duplicates in the scene?");
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
		GameObject[] _levelInfos=GameObject.FindGameObjectsWithTag("LevelInfo");

		if(_levelInfos.Length==0) {
			Debug.Log("ERROR: ND_GameController.FindLevelInfo can't find any objects in the scene tagged \"LevelInfo\"");
		}
		else {
			bool _passedInfo=false;
			for(int i=0; i<_levelInfos.Length; i++) {
				if(_levelInfos[i].name=="PassedLevelInfo") {
					//Debug.Log("ND_GameController.FIndLevelInfo has found PassedLevelInfo.");
					StartLevel(_levelInfos[i].GetComponent<ND_LevelInput>());
					_passedInfo=true;
				}
			}
			if(!_passedInfo) {		//If passed level info doesn't exist, use test info in the scene.
				for(int i=0; i<_levelInfos.Length; i++) {
					if(_levelInfos[i].name=="TestLevelInfo") {
						//Debug.Log("ND_GameController.FIndLevelInfo has found TestLevelInfo.");
						StartLevel(_levelInfos[i].GetComponent<ND_LevelInput>());
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
			//Debug.Log("ERROR: ND_GameController.FindSceneInfo can't find any objects in the scene tagged \"SceneInfo\"");
			return null;
		}
		else {
			return _sceneInfos;
		}
	}
	public void StartLevel(ND_LevelInput _levelInput){
		//Call this immediately after loading the scene. Pass the LevelInput, and the level will start with those values.
		m_levelInput=_levelInput;
		NutrientBucket.inst.Pause();
		NutrientSpawner.inst.Pause();
		NutrientSpawner.inst.SetSpawnerInfo(m_levelInput.m_spawnerInfo);
		ND_RobotHandler.inst.SetWantedFoods();
		ND_LevelTimer.inst.SetupTimer();

		//Start the intro dialogue if it exists and isn't set to be skipped, else start the drop game.
		if((FindSceneInfo()==null)||(!FindSceneInfo()[0].GetComponent<SceneInfo>().m_skipIntro)) {
			if(FindDialogue(DIALOGUETYPE.LEVELINTRO, false)!=null) {
				DialogueManager.inst.StartConversation(FindDialogue(DIALOGUETYPE.LEVELINTRO));
			}
			else StartDropGame();
		}			
		else StartDropGame();
	}
	public void StartDropGame() {
		//Start the food drop game after the intro dialogue.
		//Debug.Log("ND_GameController.StartDropGame.");

		NutrientSpawner.inst.Pause(false);
		NutrientBucket.inst.Pause(false);
	}
	public void EndLevelSuccess(){
		//The food catching part is completed.
		//Debug.Log("ND_GameController.EndLevelSuccess.");

		NutrientSpawner.inst.Pause();
		NutrientBucket.inst.Pause();

		//Start the success dialogue, else return to level select.
		if(FindDialogue(DIALOGUETYPE.LEVELWIN, false)!=null)
			DialogueManager.inst.StartConversation(FindDialogue(DIALOGUETYPE.LEVELWIN));
		else ReturnLevelSelect();
	}
	public void EndLevelRobotDead(){
		//Robot has eaten too many non-foods.
		//Debug.Log("ND_GameController.EndLevelRobotDead.");

		NutrientSpawner.inst.Pause();
		NutrientBucket.inst.Pause();

		//Start the dead robot dialogue, else reload the level.
		if(FindDialogue(DIALOGUETYPE.LEVELFAILROBOTDEATH, false)!=null)
			DialogueManager.inst.StartConversation(FindDialogue(DIALOGUETYPE.LEVELFAILROBOTDEATH));
		else ReloadScene(true);
	}
	public void EndLevelTimeLimit() {
		//Time limit has passed.
		//Debug.Log("ND_GameController.EndLevelTimeLimit.");
		
		NutrientSpawner.inst.Pause();
		NutrientBucket.inst.Pause();

		//Start the time out dialogue, else reload the level.
		if(FindDialogue(DIALOGUETYPE.LEVELFIALTIMELIMIT, false)!=null)
			DialogueManager.inst.StartConversation(FindDialogue(DIALOGUETYPE.LEVELFIALTIMELIMIT));
		else ReloadScene(true);
	}
	public void ReturnLevelSelect() {
		//Return to the LevelSelect scene.

		DestroyLevelInfo();
		DestroySceneInfo();

		SceneManager.LoadScene("LevelSelect");
	}
	public void SetTimeScale(float timeScale){
		//In case this is going to be used in gameplay for pausing. Note that this applies to Time.deltaTime but not Time.fixedDeltaTime.
		Time.timeScale = timeScale;
	}
	public void ReloadScene(bool _skipIntro=true) {
		//Reloads the Food Drop scene, skipping the intro by default.
		//Debug.Log("ND_GameController.ReloadScene("+_skipIntro+").");

		if(!_skipIntro) SceneManager.LoadScene("FoodDrop_RS");
		else {
			Instantiate(m_pre_SceneInfo);
			DontDestroyOnLoad(m_pre_SceneInfo);
			m_pre_SceneInfo.GetComponent<SceneInfo>().m_skipIntro=true;
			SceneManager.LoadScene("FoodDrop_RS");
		}
	}
	private void DestroyLevelInfo() {
		//Destroys any LevelInfo in the scene.
		GameObject[] _levelInfo=GameObject.FindGameObjectsWithTag("LevelInfo");

		if(_levelInfo.Length!=0) {
			for(int i=_levelInfo.Length-1; i>=0; i--) {
				DestroyImmediate(_levelInfo[i]);
			}
		}
	}
	private void DestroySceneInfo() {
		//Destroys any SceneInfo in the scene.
		GameObject[] _sceneInfos=GameObject.FindGameObjectsWithTag("SceneInfo");

		if(_sceneInfos.Length!=0) {
			for(int i=_sceneInfos.Length-1; i>=0; i--) {
				DestroyImmediate(_sceneInfos[i]);
			}
		}
	}

	public Conversation FindDialogue(DIALOGUETYPE _type, bool _logError=true) {
		//Finds a loaded dialogue to use by type.
		for(int i=0; i<m_levelInput.m_dialogues.Count; i++) {
			if(m_levelInput.m_dialogues[i].m_dialogueType==_type) return m_levelInput.m_dialogues[i];
		}

		if(_logError) Debug.Log("ERROR: ND_GameController.FindDialogue can't find dialogue of type \""+_type.ToString()+"\"");
		return null;
	}
}
