using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	public static DialogueManager inst;
	public DIALOGUETYPE		m_dlgType;		//The type of dialogue currently being played.
	
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
		//Debug.Log("DialogueManager.StartDialogue: "+dialogue.m_dialogueType.ToString());

		m_dlgType=dialogue.m_dialogueType;
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
