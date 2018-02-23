using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LS_LevelSelectHandler:MonoBehaviour{
	public static LS_LevelSelectHandler inst;
	public GameObject	m_levelInputObject;		//Scene ND_LevelInput object to be passed on to the Food Drop level.
	
	public CanvasGroup	m_CG_LevelSelect;		//CanvasGroup for the level select.
	public Animator		m_animAssistant;
	public Animator		m_animWhiteboard;
	public Conversation	m_introConversation;

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
		LS_LevelButtonHandler.inst.PopulateButtonList();
		DialogueManager.inst.StartConversation(m_introConversation, SpeechBubble.SPEECHBUBBLETYPE.ASSISTANT);
	}
	public void StartFoodDrop(LevelInput _levelInput) {
		//Move on to the Food Drop game with the _levelInput.
		m_levelInputObject.AddComponent<LevelInput>();
		m_levelInputObject.GetComponent<LevelInput>().Copy(_levelInput);

		SceneManager.LoadScene("FoodDrop_RS");
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
