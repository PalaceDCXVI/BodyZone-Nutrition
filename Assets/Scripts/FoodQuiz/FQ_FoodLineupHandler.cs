using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles creating the lineup of different foods.
/// </summary>

public class FQ_FoodLineupHandler:MonoBehaviour{
	public static FQ_FoodLineupHandler inst;
	public GameObject m_foodLineup;				//Parent object for all the food slots.
	public GameObject m_pre_FoodSlot;			//A single slot for a food item.
	public List<GameObject>	m_foodSlots;		//The list of all the food slots.
	public List<GameObject>	m_foods;			//The list of the actual food.

	private void Awake() {
		if(inst==null) inst=this;
		else {
			Debug.Log("FQ_FoodLineupHandler destroyed on '"+gameObject.name+"'. Are there duplicates in the scene?");
			DestroyImmediate(this);
		}
	}
	void Start(){
		if(m_foodSlots==null) m_foodSlots=new List<GameObject>();
		if(m_foods==null) m_foods=new List<GameObject>();
	}
	
	void Update(){

	}

	public void PopulateFoodList(){
		//Remove old food slots.
		if(m_foodSlots==null) m_foodSlots=new List<GameObject>();
		if(m_foods==null) m_foods=new List<GameObject>();

		while(m_foodLineup.transform.childCount>0) DestroyImmediate(m_foodLineup.transform.GetChild(0).gameObject);

		//Add in food slots.
		for(int i=0; i<FQ_GameController.inst.m_levelInput.m_foodQuizLevelInput.m_foods.Count; i++) {
			GameObject _newSlot=Instantiate(m_pre_FoodSlot, m_foodLineup.transform);
			m_foodSlots.Add(_newSlot);
			m_foods.Add(_newSlot.transform.GetChild(0).gameObject);
			_newSlot.transform.GetChild(0).gameObject.GetComponent<NutrientLife>().SetFoodType(FOODTYPE.LOGFOOD, i, FQ_GameController.inst.m_levelInput.m_foodQuizLevelInput.m_foods[i]);
			//SetButton(_newButton, LS_Levels.inst.m_levels[i]);
		}
		
		//GameObject _newButton=Instantiate(m_pre_B_Level, m_levelGrid.transform);
		//SetButton(_newButton, LS_Levels.inst.m_levels[i]);

		////Add the click callback.
		//int _levelIndex=i;
		//_newButton.GetComponent<Button>().onClick.AddListener(()=>UI_LevelButton(_levelIndex));
	}

	public Sprite GetFood(int _index) {
		//Returns the sprite of one of the foods from the Level Input.
		if(_index<FQ_GameController.inst.m_levelInput.m_foodQuizLevelInput.m_foods.Count)
			return FQ_GameController.inst.m_levelInput.m_foodQuizLevelInput.m_foods[_index];
		else {
			Debug.Log("ERROR: FQ_FoodLineupHandler.GetFood("+_index+") is out of index. m_foods.Count="+FQ_GameController.inst.m_levelInput.m_foodQuizLevelInput.m_foods.Count+".");
			return null;
		}
	}
	public void ToggleFoodGlow(int _which) {
		//Toggle the food's glow on/off.
		if(m_foods==null) {
			Debug.Log("ERROR: FQ_FoodLineupHandler.GlowFood("+_which+") can't work because m_foods is null.");
			return;
		}
		if(_which<0) {		//Every food.
			SetFoodGlow(true);
		}
		else {              //Specific food.
			//Turn all other food glow off.
			SetFoodGlow(false);

			if(_which<m_foods.Count) m_foods[_which].GetComponent<FQ_FoodGlow>().Toggle();
			else
				Debug.Log("ERROR: FQ_FoodLineupHandler.GlowFood("+_which+") is out of index. m_foods.Count="+m_foods.Count+".");
		}
	}
	public void SetFoodGlow(bool _state) {
		//Set's the glow on all the food on/off.
		if(m_foods==null) {
			Debug.Log("ERROR: FQ_FoodLineupHandler.SetFoodGlow("+_state+") can't work because m_foods is null.");
			return;
		}

		for(int i=0; i<m_foods.Count; i++)
			m_foods[i].GetComponent<FQ_FoodGlow>().SetOnOff(_state);
	}
}
