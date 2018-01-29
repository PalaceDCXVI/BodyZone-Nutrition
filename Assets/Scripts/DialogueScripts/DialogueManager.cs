using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour {

	
	public Text nameText;
	public Text dialogueText;
	public Animator animator;
	public GameObject gameCanvas;
	private Queue<string> sentences;

	private GameObject currentTrigger;
	
	
	
	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();		
	}

	public void StartDialogue (Dialogue dialogue, GameObject trigger)
	{		
		gameCanvas.GetComponent<GameplayController>().StartDialogue();
		currentTrigger = trigger;

		animator.SetBool("IsOpen", true);
		
		nameText.text = dialogue.name;
		
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
		StopAllCoroutines();
		StartCoroutine(AnimateSentence(sentence));
	}

	IEnumerator AnimateSentence (string sentence)
	{
		dialogueText.text = "";
		
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{	
		currentTrigger.GetComponent<DialogueTrigger>().EndDialogue();
		
		Debug.Log("End of convertstion.");
		animator.SetBool("IsOpen", false);
	}
	
}
