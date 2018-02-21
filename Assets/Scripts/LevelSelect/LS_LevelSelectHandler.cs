using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LS_LevelSelectHandler:MonoBehaviour{
	public static LS_LevelSelectHandler inst;
	public GameObject m_levelInputObject;       //Scene ND_LevelInput object to be passed on to the Food Drop level.

	private void Awake() {
		if(inst==null) inst=this;
		else {
			Debug.Log("LS_LevelSelectHandler destroyed on '"+gameObject.name+"'. Are there duplicates in the scene?");
			DestroyImmediate(this);
		}

		DontDestroyOnLoad(m_levelInputObject);
	}
	void Start(){
		SetupLevelSelect();
	}

	void Update(){}

	private void SetupLevelSelect() {
		//Populates the list of level buttons.
		LS_LevelButtonHandler.inst.PopulateButtonList();
	}
	public void StartFoodDrop(ND_LevelInput _levelInput) {
		//Move on to the Food Drop game with the _levelInput.
		m_levelInputObject.AddComponent<ND_LevelInput>();
		m_levelInputObject.GetComponent<ND_LevelInput>().Copy(_levelInput);

		SceneManager.LoadScene("FoodDrop_RS");
	}
}
