using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour {

	public Conversation dialogue;
	public UnityEvent dialogueEnd;
	
	private GameObject trigger;


	void Start(){
		trigger = this.gameObject;
	}
	
	public void TriggerDialogue()
	{
		//DialogueManager.inst.StartDialogue(dialogue, trigger);
	}

	public void EndDialogue()
	{
		{
			dialogueEnd.Invoke();
		}
	}
}
