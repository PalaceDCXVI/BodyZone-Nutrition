using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the game state within the Food Quiz levels.
/// </summary>

public class FQ_GameController:MonoBehaviour {
	public static FQ_GameController inst;
	public LevelInput				m_levelInput;		//Variables needed for the level. What types of food to choose from, what dialogue is spoken, etc.
	public Animator					m_animDialogue;		//Animator for the dialogue canvas.
	public bool						m_allowDragging;	//Allow player to drag the foods.

	private bool					mp_firstFrameStart=true;
	private int						mp_currentFood;		//Index into the LevelInput of the currently wanted food.
	private int						mp_correctAnswers;	//Number of correct quiz answers by the player.
	private GameObject				mp_passedLevelInfo;

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
		m_allowDragging=false;
		mp_firstFrameStart=false;
		mp_currentFood=-1;
		mp_correctAnswers=0;
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
					mp_passedLevelInfo=_levelInfos[i];
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
			if(FindDialogue(DIALOGUETYPE.FQ_INTRO, -1, false)!=null) {
				DialogueManager.inst.ToggleSpeechBubble(SpeechBubble.SPEECHBUBBLETYPE.ROBOT, false);
				DialogueManager.inst.StartConversation(FindDialogue(DIALOGUETYPE.FQ_INTRO), SpeechBubble.SPEECHBUBBLETYPE.ASSISTANT);
			}
			else StartGame();
		}			
		else StartGame();
	}
	public void StartGame() {
		//Start the Food Quiz game after the intro dialogue.
		//Debug.Log("FQ_GameController.StartDropGame.");
		StartNextWantedFood();
	}
	public void StartNextWantedFood() {
		//Resets the robot, starts the dialogue for the next wanted food.
		mp_currentFood++;

		if(mp_currentFood>=m_levelInput.m_foodQuizLevelInput.m_foodOrder.Count) {	//Quiz over.
			Debug.Log("FQ_GameController.StartNextWantedFood: Quiz over.");
			return;
		}

		m_allowDragging=true;
		DialogueManager.inst.StartConversation(FindDialogue(DIALOGUETYPE.FQ_ROBOTFOODITEM, mp_currentFood), SpeechBubble.SPEECHBUBBLETYPE.ROBOT);
	}
	public void RobotFed(int _foodIndex) {
		//Robot fed a food.
		m_allowDragging=false;
		DialogueManager.inst.ToggleSpeechBubble(SpeechBubble.SPEECHBUBBLETYPE.ROBOT, false);

		if(_foodIndex==m_levelInput.m_foodQuizLevelInput.m_foodOrder[mp_currentFood]) {	//Correct food.
			//Debug.Log("FQ_GameController.RobotFed("+_foodIndex+"): Fed correct food.");
			mp_correctAnswers++;
			DialogueManager.inst.StartConversation(FindDialogue(DIALOGUETYPE.FQ_FOODITEMSUCCESS, mp_currentFood), SpeechBubble.SPEECHBUBBLETYPE.ASSISTANT);
		}
		else {	//Fed incorrect food.
			//Debug.Log("FQ_GameController.RobotFed("+_foodIndex+"): Fed incorrect food.");
			FQ_RobotHandler.inst.AnimateRobotExplode();
			DialogueManager.inst.StartConversation(FindDialogue(DIALOGUETYPE.FQ_FOODITEMFAIL, mp_currentFood), SpeechBubble.SPEECHBUBBLETYPE.ASSISTANT);
		}
	}
	public void StartOutro(){
		//Quiz is over, start the outro dialogue if it exists and bring in the score clipboard.
		Conversation _outro=FindDialogue(DIALOGUETYPE.FQ_OUTRO);

		if(_outro!=null) DialogueManager.inst.StartConversation(_outro, SpeechBubble.SPEECHBUBBLETYPE.ASSISTANT);
		else {
			ShowScoreScreen();
		}
	}
	public void ShowScoreScreen(){
		//Animate the dialogue canvas out, set the score screen, and animate it in.
		m_animDialogue.SetTrigger("Go_OutRight");

		//Calculate the score.
		float _percentScore=(float)mp_correctAnswers/(float)m_levelInput.m_foodQuizLevelInput.m_foodOrder.Count;
		int _score=0;
		if(_percentScore<=.33f) _score=0;
		else if(_percentScore<=.66f) _score=1;
		else if(_percentScore<1) _score=2;
		else _score=3;

		//Set the score screen.
		ScoreCanvas.inst.SetScoreScreen(m_levelInput.m_levelName, _score, mp_correctAnswers, m_levelInput.m_foodQuizLevelInput.m_foodOrder.Count-mp_correctAnswers);

		//ScoreCanvas.inst.ShowScoreScreen(); The score screen is shown from the Dialogue Canvas' animator in FQ_B_OutRight.
	}
	public void EndScene() {
		//Food quiz game over. Return to the Level Select.
		
		//Set the passed level info's rating and completion.
		if(mp_passedLevelInfo!=null) {
			mp_passedLevelInfo.GetComponent<LevelInput>().m_levelRating=LEVELRATING.STAR3;
			mp_passedLevelInfo.GetComponent<LevelInput>().m_levelStatus=LEVELSTATUS.COMPLETE;
		}

		LevelLoading.Instance.LoadScene("LevelSelect");
	}

	public Conversation FindDialogue(DIALOGUETYPE _type, int _value=-1, bool _logError=true) {
		//Finds a loaded dialogue to use by type.
		for(int i=0; i<m_levelInput.m_foodQuizLevelInput.m_dialogues.Count; i++) {
			if((m_levelInput.m_foodQuizLevelInput.m_dialogues[i].m_dialogueType==_type)&&(_value==-1))
				return m_levelInput.m_foodQuizLevelInput.m_dialogues[i];
			else if((m_levelInput.m_foodQuizLevelInput.m_dialogues[i].m_dialogueType==_type)&&(m_levelInput.m_foodQuizLevelInput.m_dialogues[i].m_dialogueValue==_value))
				return m_levelInput.m_foodQuizLevelInput.m_dialogues[i];
		}

		if(_logError) Debug.Log("ERROR: FQ_GameController.FindDialogue can't find dialogue of type \""+_type.ToString()+"\"");
		return null;
	}
	public bool	QuizOver(){
		//Returns true if the last wanted food has been fed.
		bool _result=(mp_currentFood==m_levelInput.m_foodQuizLevelInput.m_foodOrder.Count-1);
		return _result;
	}
}
