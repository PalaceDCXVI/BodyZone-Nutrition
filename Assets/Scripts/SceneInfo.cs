using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to carry over information from one scene to the next, or in a reloaded scene.
/// </summary>

public class SceneInfo:MonoBehaviour {
	public bool		m_skipIntro;		//Skip the intro of a food drop level.
	
	void Start(){
		DontDestroyOnLoad(gameObject);
	}
	
	void Update(){}
}
