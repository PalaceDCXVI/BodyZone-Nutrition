using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles setting the food's image and keeping track of what type it is.
/// </summary>

public class NutrientLife:MonoBehaviour{
	public FOODTYPE m_foodType;			//Which type of food this is.	
	public int		m_foodTypeIndex;	//Index into NutrientSpawner.mp_spawnerInfo lists of foods of exactly which food item this is.
	
	public float gravityCore = 8.0f;
	public float gravityVariance = 1.0f;

	void Start(){
		if(GetComponent<Rigidbody>()) GetComponent<Rigidbody2D>().gravityScale = gravityCore + Random.Range(-gravityVariance, +gravityVariance);
	}
	
	void Update(){}

	public void SetFoodType(FOODTYPE _type, int _typeIndex, Sprite _selectedSprite){
		//Set's the food's information.
		m_foodType=_type;
		m_foodTypeIndex=_typeIndex;

		switch(m_foodType){
			case FOODTYPE.LOGFOOD:
				tag="LogFood";
				break;

			case FOODTYPE.OTHERFOOD:
				tag="OtherFood";
				break;

			case FOODTYPE.NOTFOOD:
				tag="NotFood";
				break;

			default:
				break;
		}

		GetComponent<Image>().sprite=_selectedSprite; 
	}
}
