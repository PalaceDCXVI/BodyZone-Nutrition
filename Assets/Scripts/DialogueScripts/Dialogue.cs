using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classes for holding dialogue information. A Conversation holds many DialogueLines.
/// </summary>

//The different use cases of dialogue.
public enum DIALOGUETYPE {
	NONE,
	LS_INTRO,			//Level Select intro
	FD_INTRO,			//Food Drop intro
	FD_WIN,
	FD_FAILROBOTDEATH,	//Failing Food Drop by robot death.
	FD_FAILTIMELIMIT,
	FQ_INTRO,			//Food Quiz intro
	FQ_FOODITEMSUCCESS,	//Assistant's dialogue on feeding the correct food.
	FQ_FOODITEMFAIL,
	FQ_ROBOTFOODITEM,	//Robot's dialogue of wanting a food.
	FQ_OUTRO
}

//Different events to happen as a new DialogueLine is shown.
public enum LINEFLAG {
	NONE,
	LS_ANIM_HIDERIGHT,		//Level Select: Assistant moves off screen right to grab the whiteboard.
	FQ_GLOWFOOD				//Food Quiz: Glow one of the foods by index, using FlagValue.
}

[System.Serializable]
public class Conversation{
	//An entire conversation.
	[Tooltip("A name to show in the Unity Editor.")]
	public string	m_name;
	[Tooltip("This dialogue's purpose use case.")]
	public DIALOGUETYPE			m_dialogueType;
	[Tooltip("Value to use for some Dialogue Types. -1 is null.")]
	public int					m_dialogueValue=-1;
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
	[Tooltip("Value to use for some In Flags. -1 is null.")]
	public int		m_inFlagValue=-1;
	[Tooltip("An action taken when this DialogueLine is moved out of.")]
	public LINEFLAG m_outFlag;
	[Tooltip("Value to use for some Out Flags. -1 is null.")]
	public int		m_outFlagValue=-1;
	[Tooltip("The sentence spoken.")]
	[TextArea(3, 10)]
	public string	m_sentence;
}
