using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Input variables for the nutrient drop level to use. Feed this structure into ND_GameController.StartLevel.
/// </summary>

public class ND_LevelInput:MonoBehaviour{
	[Tooltip("Different foods dropped. LogFoods complete the level.")]
	public List<Sprite>	m_logFoods;
	[Tooltip("Different foods dropped. OtherFoods don't affect the level.")]
	public List<Sprite>	m_otherFoods;
	[Tooltip("Different foods dropped. NotFoods destroy the robot.")]
	public List<Sprite>	m_notFoods;
	[Tooltip("The different dialogues needed by the level.")]
	public List<Dialogue> m_dialogues;

	void Start(){
		
	}
	
	void Update(){
		
	}
}
