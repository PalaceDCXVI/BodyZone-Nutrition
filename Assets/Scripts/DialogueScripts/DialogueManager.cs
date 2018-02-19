using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour {
	public static DialogueManager inst;
	
	public Text nameText;
	public Text dialogueText;
	public Animator animator;
	public GameObject gameCanvas;
	private Queue<string> sentences;

	private GameObject currentTrigger;



	private void Awake() {
		if(inst==null) inst=this;
		else {
			Debug.Log("DialogueManager destroyed on '"+gameObject.name+"'. Are there duplicates in the scene?");
			DestroyImmediate(this);
		}
	}
	void Start () {
		sentences = new Queue<string>();		
	}

	public void StartDialogue(Dialogue _dialogue) {
		StartDialogue(_dialogue, null);
	}
	public void StartDialogue (Dialogue dialogue, GameObject trigger)
	{		
		GameplayController.inst.StartDialogue();
		if(trigger!=null) currentTrigger = trigger;

		animator.SetBool("IsOpen", true);
		
		nameText.text = dialogue.m_speaker;
		
		sentences.Clear();
		
		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);			
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		dialogueText.text = sentence;
		
		// Animate the sentence into the dialogue box
		// but stop the original corountine if the player clicks continue again
		//StopCoroutine("AnimateSentence");
		StopAllCoroutines();
		StartCoroutine(AnimateSentence(sentence));
	}

	IEnumerator AnimateSentence (string sentence)
	{
		dialogueText.text = sentence;
		string tempDialogue = "";

		for (int i = 0; i < sentence.Length + 1; i++) //This sets the color to black and moves a clear color flag through the text, giving the illusion of dialogue writing.
		{
			dialogueText.text = sentence;
			dialogueText.color = Color.black;
			tempDialogue = dialogueText.text.Insert(i, "<color=#00000000>");
			tempDialogue += "</color>";
			dialogueText.text = tempDialogue;			
			yield return null;
		}

	}

	void EndDialogue()
	{
		if(currentTrigger!=null) currentTrigger.GetComponent<DialogueTrigger>().EndDialogue();
		
		Debug.Log("End of convertstion.");
		animator.SetBool("IsOpen", false);
	}
	
}
