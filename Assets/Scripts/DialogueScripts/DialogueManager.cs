using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles playing a Conversation.
/// </summary>

public class DialogueManager:MonoBehaviour{
	public static DialogueManager inst;
	public DIALOGUETYPE		m_dlgType;		//The type of dialogue currently being played.
	public Conversation		m_convo;        //Local copy of the conversation currently being played.
	private int				mp_dlgIndex;		//Index into the list of DialogueLines of which one is currently shown.

	//Shown information.
	public Text				m_T_name;		//Text of the speaker name.
	public Text				m_T_dialogue;   //Text of the spoken sentence.

	public Animator animator;
	
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

	public void Update() {
		if(Input.GetKeyDown(KeyCode.Alpha3)) animator.SetTrigger("Go_HideRight");
		if(Input.GetKeyDown(KeyCode.Alpha4)) animator.SetTrigger("Go_PullWhiteboard");
	}

	public void StartConversation(Conversation _convo){
		//Debug.Log("DialogueManager.StartConversation("+_convo.m_dialogueType.ToString()+").");
		m_convo=_convo;
		m_dlgType=m_convo.m_dialogueType;
		m_T_name.text=_convo.m_speaker;
		mp_dlgIndex=-1;

		animator.SetBool("IsOpen", true);

		DisplayNextSentence();
	}
	public void DisplayNextSentence(){
		//Check if any out flags need to be done.
		if((mp_dlgIndex>=0)&&(m_convo.m_dialogue[mp_dlgIndex].m_outFlag!=LINEFLAG.NONE)){

		}

		//Increment index and play sentence.
		if(mp_dlgIndex+1<m_convo.m_dialogue.Count) {
			mp_dlgIndex++;

			m_T_dialogue.text=m_convo.m_dialogue[mp_dlgIndex].m_sentence;

			//Take care of any in flags.
			if(m_convo.m_dialogue[mp_dlgIndex].m_inFlag!=LINEFLAG.NONE){

			}
		}
		else {
			EndConversation();
			return;
		}
				
		// Animate the sentence into the dialogue box
		// but stop the original corountine if the player clicks continue again
		StopAllCoroutines();
		StartCoroutine(AnimateSentence(m_T_dialogue.text));
	}

	private IEnumerator AnimateSentence(string _sentence){
		//Type out each letter of the sentence over time.
		m_T_dialogue.text=_sentence;
		string tempDialogue="";

		//This sets the color to black and moves a clear color flag through the text, giving the illusion of dialogue writing.
		for (int i=0; i<_sentence.Length+1; i++){
			m_T_dialogue.text = _sentence;
			m_T_dialogue.color = Color.black;
			tempDialogue = m_T_dialogue.text.Insert(i, "<color=#00000000>");
			tempDialogue += "</color>";
			m_T_dialogue.text = tempDialogue;			
			yield return null;
		}
	}

	public void EndConversation(){		
		//Debug.Log("End of conversation: "+m_dlgType.ToString());
		animator.SetBool("IsOpen", false);

		//End of intro. Start the nutrient drop game.
		if(m_dlgType==DIALOGUETYPE.LEVELINTRO) ND_GameController.inst.StartDropGame();

		//Level success.
		if(m_dlgType==DIALOGUETYPE.LEVELWIN) ND_GameController.inst.ReturnLevelSelect();

		//Robot dead. Reload level.
		if(m_dlgType==DIALOGUETYPE.LEVELFAILROBOTDEATH) ND_GameController.inst.ReloadScene(true);

		//Time ran out. Reload level.
		if(m_dlgType==DIALOGUETYPE.LEVELFIALTIMELIMIT) ND_GameController.inst.ReloadScene(true);
	}

	
}
