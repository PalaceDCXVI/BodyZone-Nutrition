﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Input variables for the nutrient drop level to use. Feed this structure into ND_GameController.StartLevel.
/// </summary>

public enum LEVELSTATUS{
	NONE,
	UNAVAILABLE,
	AVAILABLE,
	COMPLETE
}

public enum LEVELRATING {
	NONE,
	STAR1,
	STAR2,
	STAR3
}

public enum LEVELTYPE {
	NONE,
	FOODDROP,
	FOODQUIZ,
	BOTH
}

[System.Serializable]
public class ND_RobotInfo {
	//Gameplay information related to the robot.
	[Tooltip("Number of times the robot can eat a non-food before dying. Set to <=0 for infinite life.")]
	public int		m_robotHealth=0;
}

[System.Serializable]
public class ND_SpawnerInfo{
	//Gameplay information used by the spawner to set how difficult the level is.
	[Tooltip("Spawner will randomize food to spawn in groups of this size.")]
	[Range(3.0f, 100.0f)]
	public int		m_spawnGroupSize=10;
	[Tooltip("Average time between the spawn of each new item.")]
	public float	m_spawnRate=1;
	[Tooltip("Adds a random range to the average Spawn Rate.")]
	[Range(0.0f, 2.0f)]
	public float	m_spawnRandomness=0.1f;
	[Tooltip("Percent chance to spawn an additional random item every time a regular item spawn happens.")]
	[Range(0.0f, 1.0f)]
	public float	m_additionalSpawnChance=0.2f;
	[Tooltip("Percent of each Spawn Group of items to be Log Foods.")]
	[Range(0.0f, 1.0f)]
	public float	m_logFoodPercent=0.3f;
	[Tooltip("Percent chance that the spawned Log Food is what the robot currently needs.")]
	[Range(0.0f, 1.0f)]
	public float	m_neededFoodChance=0.5f;
	[Tooltip("Percent of each Spawn Group of items to be Not Foods.")]
	[Range(0.0f, 1.0f)]
	public float	m_notFoodPercent=0.4f;
	[Tooltip("Time limit for the level. Leave to 0 for no limit.")]
	public float	m_timeLimit=0;
	[Tooltip("Number of Log Foods needed to be eaten to complete the level.")]
	public int		m_neededItems=5;
	[Tooltip("Whether or not the player must eat at least one of each Log Food.")]
	public bool		m_needAllLogFoods=true;
	[Tooltip("Different foods dropped. LogFoods complete the level.")]
	public List<Sprite>		m_logFoods;
	[Tooltip("Different foods dropped. OtherFoods don't affect the level.")]
	public List<Sprite>		m_otherFoods;
	[Tooltip("Different foods dropped. NotFoods destroy the robot.")]
	public List<Sprite>		m_notFoods;
}

[System.Serializable]
public class FoodDrop_LevelInput {
	//Information for a Food Drop level.
	[Tooltip("The different dialogues needed by the level.")]
	public List<Conversation>	m_dialogues;
	[Tooltip("Gameplay information used by the spawner to set how difficult the level is.")]
	public ND_SpawnerInfo	m_spawnerInfo;
	[Tooltip("Gameplay information related to the robot.")]
	public ND_RobotInfo		m_robotInfo;
}

[System.Serializable]
public class FoodQuiz_LevelInput {
	//Information for a Food Quiz level.
	[Tooltip("Whether to use the Food Drop Level Input's LogFoods as the Foods.")]
	public bool				m_useFoodDropLogFoods=true;
	[Tooltip("The different foods to choose from.")]
	public List<Sprite>		m_foods;
	[Tooltip("Indices into Foods of which foods the robot wants. In order.")]
	public List<int>		m_foodOrder;
	[Tooltip("The different dialogues needed by the level.")]
	public List<Conversation>	m_dialogues;
}

public class LevelInput:MonoBehaviour{
	//Structure that defines an entire level.
	[Tooltip("What type of level this is.")]
	public LEVELTYPE			m_levelType;
	[Tooltip("Information for a Food Drop level.")]
	public FoodDrop_LevelInput	m_foodDropLevelInput;
	[Tooltip("Information for a Food Quiz level.")]
	public FoodQuiz_LevelInput	m_foodQuizLevelInput;
	[Tooltip("Whether the level is (un)available/completed.")]
	public LEVELSTATUS			m_levelStatus=LEVELSTATUS.UNAVAILABLE;
	[Tooltip("What performance rating a completed level has.")]
	public LEVELRATING			m_levelRating;
	[Tooltip("The name of the level.")]
	public string				m_levelName="";

	void Start(){}
	
	void Update(){}

	public void Copy(LevelInput _base) {
		//Copy over level input settings.
		m_levelType=_base.m_levelType;
		m_foodDropLevelInput=_base.m_foodDropLevelInput;
		m_foodQuizLevelInput=_base.m_foodQuizLevelInput;
		m_levelStatus=_base.m_levelStatus;
		m_levelRating=_base.m_levelRating;
		m_levelName=_base.m_levelName;
	}
}
