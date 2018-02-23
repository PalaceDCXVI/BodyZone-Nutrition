using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classes for holding dialogue information. A Conversation holds many DialogueLines.
/// </summary>

//The different use cases of dialogue.
public enum DIALOGUETYPE {
	NONE,
	LEVELINTRO,
	LEVELWIN,
	LEVELFAILROBOTDEATH,
	LEVELFIALTIMELIMIT
}

//Different events to happen as a new DialogueLine is shown.
public enum LINEFLAG {
	NONE,
	LS_ANIM_HIDERIGHT
}

[System.Serializable]
public class Conversation{
	//An entire conversation.
	[Tooltip("This dialogue's purpose use case.")]
	public DIALOGUETYPE			m_dialogueType;
	[Tooltip("The name of the speaker.")]
	public string				m_speaker;
	[Tooltip("The lines of dialogue in the conversation.")]
	public List<DialogueLine>	m_dialogue;
}

[System.Serializable]
public class DialogueLine {
	//A single line of dialogue.
	[Tooltip("An action taken when this DialogueLine is introduced.")]
	public LINEFLAG	m_inFlag;
	[Tooltip("An action taken when this DialogueLine is moved out of.")]
	public LINEFLAG m_outFlag;
	[Tooltip("The sentence spoken.")]
	[TextArea(3, 10)]
	public string	m_sentence;
}
