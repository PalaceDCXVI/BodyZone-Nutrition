using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Collection component of the robot.
/// Handles detection and response when the robot comes into contact with food.
/// </summary>

public class ItemCollector:MonoBehaviour {
	public StunEffect robotStunEffect;
	
	void OnTriggerEnter2D(Collider2D other){
		if(!NutrientSpawner.inst.m_active) return;

		NutrientLife _nutrientLife=other.GetComponent<NutrientLife>();
		if(_nutrientLife==null){
			return;
		}

		FOODTYPE _foodType=other.GetComponent<NutrientLife>().m_foodType;
		int _foodIndex=other.GetComponent<NutrientLife>().m_foodTypeIndex;

		switch(_foodType){
			case FOODTYPE.LOGFOOD:
				if(_foodIndex==ND_RobotHandler.inst.m_wantedFoods[ND_RobotHandler.inst.m_wantedFoodIndex]) {
					//The wanted food was eaten.
					ND_RobotHandler.inst.NextFood();

					//Apply a robot eating animation.

					//Destroy incoming food.
					Destroy(other.gameObject);
				}
				break;

			case FOODTYPE.NOTFOOD:
				//Apply stun effect if the food acquired is not actually a food item.
				robotStunEffect.ApplyStun();
				ND_RobotHandler.inst.EatBadFood();

				Destroy(other.gameObject);
				break;

			case FOODTYPE.OTHERFOOD:
				break;

			default:
				break;
		}
	}
}
