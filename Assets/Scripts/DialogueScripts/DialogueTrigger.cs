using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
	public UnityEvent dialogueEnd;
	
	private GameObject trigger;


	void Start(){
		trigger = this.gameObject;
	}
	
	public void TriggerDialogue()
	{
		// Better way would being using a singleton but this is faster for the demo
		
		
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue, trigger);
	}

	public void EndDialogue()
	{
		{
			dialogueEnd.Invoke();
		}
	}
}
