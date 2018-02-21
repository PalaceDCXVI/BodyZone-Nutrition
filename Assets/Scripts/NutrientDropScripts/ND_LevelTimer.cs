using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles showing/hiding and updating the level timer.
/// </summary>

public class ND_LevelTimer:MonoBehaviour {
	public static ND_LevelTimer inst;
	public GameObject	m_timer;
	public bool			m_levelTimed;		//Whether the level is timed or not.
	public float		m_timeLimit;		//Total time limit in the level.

	private void Awake() {
		if(inst==null) inst=this;
		else {
			Debug.Log("ND_LevelTimer destroyed on '"+gameObject.name+"'. Are there duplicates in the scene?");
			DestroyImmediate(this);
		}
	}
	void Start(){}
	
	void Update() {
		if(m_levelTimed) {
			m_timer.GetComponent<Text>().text=ConvertTime(m_timeLimit-NutrientSpawner.inst.GetLevelTimer());
		}
	}

	public void SetupTimer() {
		//Shows the timer and activates the updating.
		m_timeLimit=ND_GameController.inst.m_levelInput.m_spawnerInfo.m_timeLimit;

		if(m_timeLimit<=0) {
			m_timer.SetActive(false);
			m_levelTimed=false;
		}
		else {
			m_timer.SetActive(true);
			m_levelTimed=true;
		}
	}

	private string ConvertTime(float _time) {
		//Converts a time in total seconds into MM:SS.
		string _result="";

		if(_time<=0) {
			_result="00:00";
			return _result;
		}

		int _minutes=Mathf.FloorToInt(_time/60.0f);
		int _seconds=Mathf.CeilToInt(_time%60.0f);
		if(_seconds==60){
			_seconds=0;
			_minutes++;
		}

		string _strMinutes=""+_minutes;
		string _strSeconds=""+_seconds;
		if(_strMinutes.Length<2) _strMinutes="0"+_strMinutes;
		if(_strSeconds.Length<2) _strSeconds="0"+_strSeconds;

		_result=_strMinutes+":"+_strSeconds;
		return _result;
	}
}
