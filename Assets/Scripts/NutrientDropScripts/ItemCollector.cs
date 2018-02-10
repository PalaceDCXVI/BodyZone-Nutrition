using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Collection component for the robot.
//Handles detection and response when the robot comes into contact with a food.
public class ItemCollector : MonoBehaviour {

	public NutrientDropState dropState;
	public LogManager log;
	public StunEffect robotStunEffect;

	public GameObject goodOutroDialogueTrigger;
	public GameObject badOutroDialogueTrigger;

	public int m_FoodCountGoal = 10;
	public int m_goodChoices = 0;
	public int m_challengeFoodCount = 0;


	public void resetFoodCounters()
	{
		m_goodChoices = 0;
		m_challengeFoodCount = 0;
	}

	//Collect the tiems that collide with the hit box and respond to them.
	//This includes applying effects to the robot and tallying scores.
	void OnTriggerEnter2D(Collider2D other)	
	{
		NutrientLife nutrientLife = other.GetComponent<NutrientLife>();
		if (nutrientLife == null)
		{
			return;
		}

		NutrientLife.FoodType collidedFoodType = other.GetComponent<NutrientLife>().foodType;
		if (dropState.GetGameState() == GameplayController.GameState.STANDARD)
		{
			switch (collidedFoodType)
			{
				case NutrientLife.FoodType.LogFood:
					log.CompareImage(other.GetComponent<Image>());
					Destroy(other.gameObject);
				break;

				case NutrientLife.FoodType.NotFood:
					log.CompareImage(other.GetComponent<Image>());

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
		else
		{
			switch (collidedFoodType)
			{
				case NutrientLife.FoodType.LogFood:
					m_challengeFoodCount++;
					if(true)
					{
						m_goodChoices++;
					}

					Destroy(other.gameObject);
				break;

				case NutrientLife.FoodType.NotFood:
					robotStunEffect.ApplyStun();
					Destroy(other.gameObject);
				break;

				case NutrientLife.FoodType.OtherFood:
				break;

				default:
				break;				
			}

			if(m_challengeFoodCount >= m_FoodCountGoal)
			{
				if(m_goodChoices > 7)
				{
					goodOutroDialogueTrigger.GetComponent<DialogueTrigger>().TriggerDialogue();
				}
				else
				{
					badOutroDialogueTrigger.GetComponent<DialogueTrigger>().TriggerDialogue();
				}
				
			}			
		}
	}
}
