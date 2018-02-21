using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The different use cases of dialogue.
public enum DIALOGUETYPE {
	NONE,
	LEVELINTRO,
	LEVELWIN,
	LEVELFAILROBOTDEATH,
	LEVELFIALTIMELIMIT
}

[System.Serializable]
public class Dialogue {
	[Tooltip("This dialogue's purpose use case.")]
	public DIALOGUETYPE m_dialogueType;
	[Tooltip("The name of the speaker.")]
	public string m_speaker;
	
	[TextArea(3, 10)]
	public string[] sentences;
}
