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
	public List<ND_LevelInput>	m_levels;   //All the levels of the game.
	
	private void Awake() {
		if(inst==null) inst=this;
		else {
			Debug.Log("LS_Levels destroyed on '"+gameObject.name+"'. Are there duplicates in the scene?");
			DestroyImmediate(this);
		}

		//Auto populate the level list if needed.
		if(m_autoPopulate) {
			if(m_levels==null) m_levels=new List<ND_LevelInput>();
			m_levels.Clear();

			for(int i=0; i<transform.childCount; i++) {
				if(transform.GetChild(i).GetComponent<ND_LevelInput>()!=null) {
					m_levels.Add(transform.GetChild(i).GetComponent<ND_LevelInput>());
				}
			}
		}
	}
	void Start(){}
	
	void Update(){}
}
