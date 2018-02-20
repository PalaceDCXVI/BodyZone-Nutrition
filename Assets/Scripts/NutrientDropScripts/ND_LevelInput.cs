using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Input variables for the nutrient drop level to use. Feed this structure into ND_GameController.StartLevel.
/// </summary>

[System.Serializable]
public class ND_RobotInfo {
	//Gameplay information related to the robot.
	[Tooltip("Number of times the robot can eat a non-food before dying. Set to <=0 for infinite life.")]
	public int		m_robotHealth;
}

[System.Serializable]
public class ND_SpawnerInfo {
	//Gameplay information used by the spawner to set how difficult the level is.
	[Tooltip("Spawner will randomize food to spawn in groups of this size.")]
	[Range(3.0f, 100.0f)]
	public int		m_spawnGroupSize;
	[Tooltip("Average time between the spawn of each new item.")]
	public float	m_spawnRate;
	[Tooltip("Adds a random range to the average Spawn Rate.")]
	[Range(0.0f, 2.0f)]
	public float	m_spawnRandomness;
	[Tooltip("Percent chance to spawn an additional random item every time a regular item spawn happens.")]
	[Range(0.0f, 1.0f)]
	public float	m_additionalSpawnChance;
	[Tooltip("Percent of each Spawn Group of items to be Log Foods.")]
	[Range(0.0f, 1.0f)]
	public float	m_logFoodPercent;
	[Tooltip("Percent chance that the spawned Log Food is what the robot currently needs.")]
	[Range(0.0f, 1.0f)]
	public float	m_neededFoodChance;
	[Tooltip("Percent of each Spawn Group of items to be Not Foods.")]
	[Range(0.0f, 1.0f)]
	public float	m_notFoodPercent;
	[Tooltip("Time limit for the level. Leave to 0 for no limit.")]
	public float	m_timeLimit;
	[Tooltip("Number of Log Foods needed to be eaten to complete the level.")]
	public int		m_neededItems;
	[Tooltip("Whether or not the player must eat at least one of each Log Food.")]
	public bool		m_needAllLogFoods;
	[Tooltip("Different foods dropped. LogFoods complete the level.")]
	public List<Sprite>		m_logFoods;
	[Tooltip("Different foods dropped. OtherFoods don't affect the level.")]
	public List<Sprite>		m_otherFoods;
	[Tooltip("Different foods dropped. NotFoods destroy the robot.")]
	public List<Sprite>		m_notFoods;
}

public class ND_LevelInput:MonoBehaviour{
	[Tooltip("The different dialogues needed by the level.")]
	public List<Dialogue>	m_dialogues;
	[Tooltip("Gameplay information used by the spawner to set how difficult the level is.")]
	public ND_SpawnerInfo	m_spawnerInfo;
	[Tooltip("Gameplay information related to the robot.")]
	public ND_RobotInfo		m_robotInfo;

	void Start(){}
	
	void Update(){}
}
