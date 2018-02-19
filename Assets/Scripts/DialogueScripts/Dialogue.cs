using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue {
	[Tooltip("Some identifier for this dialogue's use case.")]
	public string m_dialogueID;
	[Tooltip("The name of the speaker.")]
	public string m_speaker;
	
	[TextArea(3, 10)]
	public string[] sentences;

}
