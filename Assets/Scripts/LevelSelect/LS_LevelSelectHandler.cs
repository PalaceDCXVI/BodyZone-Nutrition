using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles general flow in the the Level Select screen.
/// </summary>

public class LS_LevelSelectHandler:MonoBehaviour{
	public static LS_LevelSelectHandler inst;
	public GameObject	m_levelInputObject;		//Scene ND_LevelInput object to be passed on to the Food Drop level.
	
	public CanvasGroup	m_CG_LevelSelect;		//CanvasGroup for the level select.
	public Animator		m_animAssistant;		//Assistant's animator.
	public Animator		m_animWhiteboard;		//Whiteboard's animator.
	public Conversation	m_introConversation;	//The scene's intro conversation.

	private void Awake() {
		if(inst==null) inst=this;
		else {
			Debug.Log("LS_LevelSelectHandler destroyed on '"+gameObject.name+"'. Are there duplicates in the scene?");
			DestroyImmediate(this);
		}

		DontDestroyOnLoad(m_levelInputObject);
	}
	void Start(){
		SetupLevelSelect();
	}

	void Update(){}

	private void SetupLevelSelect() {
		//Populates the list of level buttons.
		FindCompletedLevelInfo();
		LS_LevelButtonHandler.inst.PopulateButtonList();
		DialogueManager.inst.StartConversation(m_introConversation, SpeechBubble.SPEECHBUBBLETYPE.ASSISTANT);
	}
	private void FindCompletedLevelInfo() {
		//Find any PassedLevelInfo from previous scenes and apply the information to the level list.
		GameObject[] _levelInfos=GameObject.FindGameObjectsWithTag("LevelInfo");

		if(_levelInfos.Length==0) {
			Debug.Log("ERROR: ND_GameController.FindLevelInfo can't find any objects in the scene tagged \"LevelInfo\"");
		}
		else {
			for(int i=0; i<_levelInfos.Length; i++) {
				if((_levelInfos[i].name=="PassedLevelInfo")&&(_levelInfos[i].GetComponent<LevelInput>()!=null)) {
					//Debug.Log("LS_LevelSelectHandler.FindCompletedLevelInfo has found PassedLevelInfo.");
					LS_Levels.inst.UpdateLevel(_levelInfos[i].GetComponent<LevelInput>());
				}
			}
		}
	}
	public void StartLevel(LevelInput _levelInput) {
		//Move on to the Food Drop game with the _levelInput.
		m_levelInputObject.AddComponent<LevelInput>();
		m_levelInputObject.GetComponent<LevelInput>().Copy(_levelInput);
		
		LEVELTYPE _levelType=m_levelInputObject.GetComponent<LevelInput>().m_levelType;
		if((_levelType==LEVELTYPE.FOODDROP)||(_levelType==LEVELTYPE.BOTH))
			SceneManager.LoadScene("FoodDrop_RS");
		else if(_levelType==LEVELTYPE.FOODQUIZ)
			SceneManager.LoadScene("FoodQuiz");
	}

	//Animator functions.
	public void PullWhiteboard(int _step) {
		//Hide the assistant to the right.
		if(_step==0) m_animAssistant.SetTrigger("Go_HideRight");

		//Move assistant back out and pull whiteboard out.
		if(_step==1) m_animWhiteboard.SetTrigger("WB_PullOut");

		//Show speech bubble again.
		if(_step==2) DialogueManager.inst.ToggleSpeechBubble();
	}
}
