using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles creating and reacting to the shown grid of level buttons.
/// </summary>

public class LS_LevelButtonHandler:MonoBehaviour{
	public static LS_LevelButtonHandler inst;
	public GameObject	m_levelGrid;		//The parent object for the levels.
	public GameObject	m_pre_B_Level;		//Prefab level button.
	public List<Sprite> m_levelStars;		//Graphics for the level stars. 0=0 stars, 3=3 stars.
	public List<Sprite> m_levelBoxes;		//Graphics for the level button box. 0=unavailable, 1=available, 2=completed.
	public List<GameObject> m_moveButtons;	//[0]Back/[1]forward buttons.

	private int		mp_levelIndex;			//Index into the list of levels of which is the first of 6 currently shown.

	private void Awake(){
		if(inst==null) inst=this;
		else {
			Debug.Log("LS_LevelButtonHandler destroyed on '"+gameObject.name+"'. Are there duplicates in the scene?");
			DestroyImmediate(this);
		}
	}
	void Start(){
		while(m_levelGrid.transform.childCount>0) DestroyImmediate(m_levelGrid.transform.GetChild(0).gameObject);
		mp_levelIndex=0;
	}
	
	void Update(){}

	public void UI_Button(string _button) {
		//Handles different UI buttons.
		if(_button=="Back") {		//Cycle the display of 6 levels backward.
			if(mp_levelIndex-6>=0) {
				mp_levelIndex-=6;
				PopulateButtonList();
			}
		}
		if(_button=="Forward") {    //Cycle the display of 6 levels forward.
			if(mp_levelIndex+6<LS_Levels.inst.m_levels.Count) {
				mp_levelIndex+=6;
				PopulateButtonList();
			}
		}
	}
	public void UI_LevelButton(int _levelIndex) {
		//Player clicked a level button. Start the Food Drop level.
		//Debug.Log("LS_LevelButtonHandler.UI_LevelButton("+_levelIndex+").");
		LS_LevelSelectHandler.inst.StartFoodDrop(LS_Levels.inst.m_levels[_levelIndex]);
	}

	public void PopulateButtonList(){
		//Remove old buttons.
		while(m_levelGrid.transform.childCount>0) DestroyImmediate(m_levelGrid.transform.GetChild(0).gameObject);

		//Add in buttons following the list of levels and mp_levelIndex.
		for(int i=mp_levelIndex; i<mp_levelIndex+6; i++) {
			if(i<LS_Levels.inst.m_levels.Count) {
				GameObject _newButton=Instantiate(m_pre_B_Level, m_levelGrid.transform);
				SetButton(_newButton, LS_Levels.inst.m_levels[i]);

				//Add the click callback.
				int _levelIndex=i;
				_newButton.GetComponent<Button>().onClick.AddListener(()=>UI_LevelButton(_levelIndex));
			}
		}

		//Show/hide the back/forward buttons as needed.
		if(mp_levelIndex==0) m_moveButtons[0].SetActive(false);
		else m_moveButtons[0].SetActive(true);

		if(mp_levelIndex+6<LS_Levels.inst.m_levels.Count) m_moveButtons[1].SetActive(true);
		else m_moveButtons[1].SetActive(false);
	}
	public void SetButton(GameObject _button, ND_LevelInput _info) {
		//Set a button's name, image, and star rating.
		
		//Button's name.
		_button.transform.GetChild(0).gameObject.GetComponent<Text>().text=_info.m_levelName;

		//Button's image.
		if(_info.m_levelStatus==LEVELSTATUS.UNAVAILABLE) _button.GetComponent<Image>().sprite=m_levelBoxes[0];
		else if(_info.m_levelStatus==LEVELSTATUS.AVAILABLE) _button.GetComponent<Image>().sprite=m_levelBoxes[1];
		else if(_info.m_levelStatus==LEVELSTATUS.COMPLETE) _button.GetComponent<Image>().sprite=m_levelBoxes[2];

		//Stars.
		if(_info.m_levelRating==LEVELRATING.NONE) _button.transform.GetChild(1).gameObject.GetComponent<Image>().sprite=m_levelStars[0];
		else if(_info.m_levelRating==LEVELRATING.STAR1) _button.transform.GetChild(1).gameObject.GetComponent<Image>().sprite=m_levelStars[1];
		else if(_info.m_levelRating==LEVELRATING.STAR2) _button.transform.GetChild(1).gameObject.GetComponent<Image>().sprite=m_levelStars[2];
		else if(_info.m_levelRating==LEVELRATING.STAR3) _button.transform.GetChild(1).gameObject.GetComponent<Image>().sprite=m_levelStars[3];
	}
}
