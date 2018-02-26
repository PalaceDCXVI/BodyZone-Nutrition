using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all the levels of the game, used by the level select screen and passed on individually to the Food Drop scene.
/// </summary>

public class LS_Levels:MonoBehaviour{
	public static LS_Levels inst;
	[Tooltip("Automatically fill the level list based on the children objects. Child order determines level order.")]
	public bool					m_autoPopulate;
	public List<LevelInput>		m_levels;   //All the levels of the game.
	
	private void Awake() {
		if(inst==null) inst=this;
		else {
			Debug.Log("LS_Levels destroyed on '"+gameObject.name+"'. Are there duplicates in the scene?");
			DestroyImmediate(this);
		}

		//Auto populate the level list if needed.
		if(m_autoPopulate) {
			if(m_levels==null) m_levels=new List<LevelInput>();
			m_levels.Clear();

			for(int i=0; i<transform.childCount; i++) {
				if(transform.GetChild(i).GetComponent<LevelInput>()!=null) {
					m_levels.Add(transform.GetChild(i).GetComponent<LevelInput>());
				}
			}
		}
	}
	void Start(){}
	
	void Update(){}

	public void UpdateLevel(LevelInput _levelInput) {
		//Update an existing level's information based on PassedLevelInput from a previous scene.
		//Debug.Log("LS_Levels.UpdateLevel for level with name \""+_levelInput.m_levelName+"\".");

		for(int i=0; i<m_levels.Count; i++) {
			if(m_levels[i].m_levelName==_levelInput.m_levelName) {
				m_levels[i].m_levelRating=_levelInput.m_levelRating;
				m_levels[i].m_levelStatus=_levelInput.m_levelStatus;
			}
		}

		//Destroy the PassedLevelInfo.
		DestroyImmediate(_levelInput.gameObject);
	}
}
