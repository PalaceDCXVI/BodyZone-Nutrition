using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles playing a Conversation.
/// </summary>

[System.Serializable]
public class SpeechBubble {
	//Contains a single speech bubble.
	public enum SPEECHBUBBLETYPE {
		NONE,
		ASSISTANT,
		ROBOT
	}

	public SPEECHBUBBLETYPE	m_type;			//Which SpeechBubble this refers to.
	public Text				m_T_name;		//Text of the speaker name.
	public Text				m_T_dialogue;   //Text of the spoken sentence.
	public GameObject		m_speechBubble;	//The speech bubble to show/hide.
}

public class DialogueManager:MonoBehaviour{
	public static DialogueManager inst;
	[Tooltip("The different speech bubbles in the scene.")]
	public List<SpeechBubble> m_speechBubbles;			//All speech bubbles in the scene.

	private DIALOGUETYPE	mp_dlgType;					//The type of dialogue currently being played.
	private Conversation	mp_convo;					//Local copy of the conversation currently being played.
	private int				mp_dlgIndex;				//Index into the list of DialogueLines of which one is currently shown.
	private SpeechBubble.SPEECHBUBBLETYPE mp_bubble;	//Which speech bubble is being used.
		
	private void Awake(){
		if(inst==null) inst=this;
		else {
			Debug.Log("DialogueManager destroyed on '"+gameObject.name+"'. Are there duplicates in the scene?");
			DestroyImmediate(this);
		}
	}
	void Start(){
		mp_dlgIndex=0;	
	}

	public void Update(){}

	public void StartConversation(Conversation _convo, SpeechBubble.SPEECHBUBBLETYPE _type){
		//Begins a conversation using a speech bubble.
		//Debug.Log("DialogueManager.StartConversation("+_convo.m_dialogueType.ToString()+").");
		mp_convo=_convo;
		mp_dlgType=mp_convo.m_dialogueType;
		mp_bubble=_type;
		ToggleSpeechBubble(mp_bubble, true);
		GetSpeechBubble(mp_bubble).m_T_name.text=_convo.m_speaker;
		mp_dlgIndex=-1;
		Button speechBubbleButton = GetSpeechBubble(mp_bubble).m_speechBubble.GetComponentInChildren<Button>();
		if (speechBubbleButton)
		{
			speechBubbleButton.interactable = true;
		}

		DisplayNextSentence();
	}
	public void DisplayNextSentence(){
		//Show the next sentence of the conversation. End if necessary.

		//Check if any out flags need to be done.
		if((mp_dlgIndex>=0)&&(mp_convo.m_dialogue[mp_dlgIndex].m_outFlag!=LINEFLAG.NONE)){
			ResolveOutFlags();
		}

		//Increment index and play sentence.
		if(mp_dlgIndex+1<mp_convo.m_dialogue.Count) {
			mp_dlgIndex++;

			GetSpeechBubble(mp_bubble).m_T_dialogue.text=mp_convo.m_dialogue[mp_dlgIndex].m_sentence;

			//Take care of any in flags.
			if(mp_convo.m_dialogue[mp_dlgIndex].m_inFlag!=LINEFLAG.NONE){
				ResolveInFlags();
			}
		}
		else {
			EndConversation();
			return;
		}
				
		// Animate the sentence into the dialogue box
		// but stop the original corountine if the player clicks continue again
		StopAllCoroutines();
		StartCoroutine(AnimateSentence(GetSpeechBubble(mp_bubble).m_T_dialogue.text));
	}
	private void ResolveInFlags() {
		//Take care of any in flags.
		//Glow a piece of food.
		if(mp_convo.m_dialogue[mp_dlgIndex].m_inFlag==LINEFLAG.FQ_GLOWFOOD) {
			FQ_FoodLineupHandler.inst.ToggleFoodGlow(mp_convo.m_dialogue[mp_dlgIndex].m_inFlagValue);
		}
	}
	private void ResolveOutFlags() {
		//Take care of any out flags.

		//Level Select		////
		//Hide assistant to the right and hide speech bubble.
		if(mp_convo.m_dialogue[mp_dlgIndex].m_outFlag==LINEFLAG.LS_ANIM_HIDERIGHT) {
			LS_LevelSelectHandler.inst.PullWhiteboard(0);
			ToggleSpeechBubble();
		}
	}

	private IEnumerator AnimateSentence(string _sentence){
		//Type out each letter of the sentence over time.
		GetSpeechBubble(mp_bubble).m_T_dialogue.text=_sentence;
		string tempDialogue="";

		//This sets the color to black and moves a clear color flag through the text, giving the illusion of dialogue writing.
		for (int i=0; i<_sentence.Length+1; i++){
			GetSpeechBubble(mp_bubble).m_T_dialogue.text = _sentence;
			GetSpeechBubble(mp_bubble).m_T_dialogue.color = Color.black;
			tempDialogue = GetSpeechBubble(mp_bubble).m_T_dialogue.text.Insert(i, "<color=#00000000>");
			tempDialogue += "</color>";
			GetSpeechBubble(mp_bubble).m_T_dialogue.text = tempDialogue;			
			yield return null;
		}
	}

	public void EndConversation(){	
		//End a conversation and do whatever is next.	
		//Debug.Log("End of conversation: "+m_dlgType.ToString());
		Button speechBubbleButton = GetSpeechBubble(mp_bubble).m_speechBubble.GetComponentInChildren<Button>();
		if (speechBubbleButton)
		{
			speechBubbleButton.interactable = false;
		}
		//Level Select	////
		if(mp_dlgType==DIALOGUETYPE.LS_INTRO) {
			ToggleSpeechBubble();
			LS_LevelSelectHandler.inst.m_CG_LevelSelect.blocksRaycasts=true;
		}


		//Food Drop		////
		//End of intro. Start the nutrient drop game.
		else if(mp_dlgType==DIALOGUETYPE.FD_INTRO){
			ND_GameController.inst.StartDropGame();
			ND_GameController.inst.m_animDialogue.SetTrigger("Go_BottomOut");
		}

		//Level success.
		else if(mp_dlgType==DIALOGUETYPE.FD_WIN) ND_GameController.inst.EndLevelGoToDatabase();

		//Robot dead. Reload level.
		else if(mp_dlgType==DIALOGUETYPE.FD_FAILROBOTDEATH) ND_GameController.inst.ReloadScene(true);

		//Time ran out. Reload level.
		else if(mp_dlgType==DIALOGUETYPE.FD_FAILTIMELIMIT) ND_GameController.inst.ReloadScene(true);


		//Food Quiz		////
		//End of intro.
		else if(mp_dlgType==DIALOGUETYPE.FQ_INTRO){
			ToggleSpeechBubble(SpeechBubble.SPEECHBUBBLETYPE.ASSISTANT, false);
			FQ_GameController.inst.StartGame();
			FQ_FoodLineupHandler.inst.SetFoodGlow(false);
		}

		//Food fed to robot and explanatory dialogue over.
		if(mp_dlgType==DIALOGUETYPE.FQ_FOODITEMSUCCESS){
			ToggleSpeechBubble();
			FQ_RobotHandler.inst.AnimateRobotLeaving();
		}
		else if(mp_dlgType==DIALOGUETYPE.FQ_FOODITEMFAIL){
			ToggleSpeechBubble();
			FQ_RobotHandler.inst.AnimateNewRobotIn();
		}

		//End of outro.
		else if(mp_dlgType==DIALOGUETYPE.FQ_OUTRO){
			ToggleSpeechBubble(SpeechBubble.SPEECHBUBBLETYPE.ASSISTANT, false);
			FQ_GameController.inst.ShowScoreScreen();
			//Debug.Log("DialogueManager.EndConversation: End of FQ_OUTRO.");
		}
	}

	public SpeechBubble GetSpeechBubble(SpeechBubble.SPEECHBUBBLETYPE _type) {
		//Get one of the Speech Bubbles by type.
		for(int i=0; i<m_speechBubbles.Count; i++) {
			if(m_speechBubbles[i].m_type==_type) return m_speechBubbles[i];
		}

		Debug.Log("ERROR: DialogueManager.GetSpeechBubble can't find Speech Bubble of type \""+_type.ToString()+"\".");
		return null;
	}
	public void ToggleSpeechBubble() {
		//Toggles on/off the active speech bubble.
		GetSpeechBubble(mp_bubble).m_speechBubble.SetActive(!GetSpeechBubble(mp_bubble).m_speechBubble.activeSelf);
	}
	public void ToggleSpeechBubble(SpeechBubble.SPEECHBUBBLETYPE _type, bool _state) {
		//Toggles the visibility of a speech bubble.
		bool _found=false;
		for(int i=0; i<m_speechBubbles.Count; i++) {
			if(m_speechBubbles[i].m_type==_type) {
				m_speechBubbles[i].m_speechBubble.SetActive(_state);
				_found=true;
			}
		}

		if(!_found) Debug.Log("ERROR: DialogueManager.ToggleSpeechBubble("+_type.ToString()+", "+_state+") can't find speech bubble of that type in m_speechBubbles.");
	}
}
