using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the Score Screen. Showing/hiding, setting, and animating.
/// </summary>

public class ScoreCanvas:MonoBehaviour {
	public static ScoreCanvas inst;

	public GameObject	m_canvas;				//Canvas GameObject of the score screen.
	public Animator		m_animator;				//Score screen's animator.
	public Text			m_levelName;			//String of the level name.
	public Image		m_starRating;			//Image of the star rating.
	public GameObject	m_fedParent;			//Parent of the list of fed robots.
	public GameObject	m_destroyedParent;		//Parent of the list of fed robots.
	public List<Sprite>	m_levelStars;			//The sprites for star ratings. 0=0 stars, 1=1 star, etc.
	public GameObject	m_pre_FedRobot;			//Prefab for a fed robot.
	public GameObject	m_pre_DestroyedRobot;	//Prefab for a destroyed robot.

	private void Awake(){
		if(inst==null) inst=this;
		else {
			Debug.Log("ScoreCanvas destroyed on '"+gameObject.name+"'. Are there duplicates in the scene?");
			DestroyImmediate(this);
		}
	}
	void Start(){
		Reset();
	}
	
	void Update(){}

	public void UI_ButtonQuit() {
		//Return to the level select screen.
		FQ_GameController.inst.EndScene();
	}
	public void Reset(){
		//Applies a generic level name, 0 star rating, and empty robot lists.
		m_levelName.text="Level Title";
		m_starRating.sprite=m_levelStars[0];
		ResetRobotLists();
	}
	public void ResetRobotLists() {
		//Empties the two lists of robots fed/destroyed.
		while(m_fedParent.transform.childCount>0) DestroyImmediate(m_fedParent.transform.GetChild(0).gameObject);
		while(m_destroyedParent.transform.childCount>0) DestroyImmediate(m_destroyedParent.transform.GetChild(0).gameObject);
	}
	public void SetScoreScreen(string _levelTitle, int _starRating, int _robotsFed, int _robotsDestroyed) {
		//Sets the score screen's look.
		m_levelName.text=_levelTitle;
		if((_starRating>3)||(_starRating<0)) Debug.Log("ScoreCanvas.SetScoreScreen("+_levelTitle+", "+_starRating+", "+_robotsFed+", "+_robotsDestroyed+") can only set the star rating to 0-3.");
		else m_starRating.sprite=m_levelStars[_starRating];

		ResetRobotLists();
		for(int i=0; i<_robotsFed; i++) {
			GameObject _tempBot=Instantiate(m_pre_FedRobot, m_fedParent.transform);
		}
		for(int i=0; i<_robotsDestroyed; i++) {
			GameObject _tempBot=Instantiate(m_pre_DestroyedRobot, m_destroyedParent.transform);
		}
	}
	public void ShowScoreScreen() {
		//Animates the score screen in.
		if(m_canvas.GetComponent<CanvasGroup>().alpha==0) {
			m_canvas.GetComponent<CanvasGroup>().alpha=1;
			m_canvas.GetComponent<CanvasGroup>().interactable=true;
			m_canvas.GetComponent<CanvasGroup>().blocksRaycasts=true;
		}
		m_animator.SetTrigger("Go_InLeft");
	}
}
