using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Collection component for the robot.
//Handles detection and response when the robot comes into contact with a food.
public class ItemCollector : MonoBehaviour {

	public NutrientDropState dropState;

	public WantedFood wantedFood;
	public StunEffect robotStunEffect;

	public GameObject goodOutroDialogueTrigger;
	public GameObject badOutroDialogueTrigger;

	//Collect the tiems that collide with the hit box and respond to them.
	//This includes applying effects to the robot and tallying scores.
	void OnTriggerEnter2D(Collider2D other)	
	{
		NutrientLife nutrientLife = other.GetComponent<NutrientLife>();
		if (nutrientLife == null)
		{
			return;
		}

		Image otherImage = other.GetComponent<Image>();
		if (otherImage == null)
		{
			return;
		}

		NutrientLife.FoodType collidedFoodType = other.GetComponent<NutrientLife>().foodType;
		switch (collidedFoodType)
		{
			case NutrientLife.FoodType.LogFood:

				if (otherImage.sprite == wantedFood.currentWantedFood.sprite)
				{
					//Set the wanted food as the next food.
					wantedFood.NextFood();
					
					//Destoy collided object.
					//Should be moved to trigger an animation where the robot 'eats' the food.
					Destroy(other.gameObject);
				}
			break;

			case NutrientLife.FoodType.NotFood:
			//Apply stun effect if the food acquired is not actually a food item.
				robotStunEffect.ApplyStun();

				Destroy(other.gameObject);
			break;

			case NutrientLife.FoodType.OtherFood:
			break;

			default:
			break;
		}
	}
}
