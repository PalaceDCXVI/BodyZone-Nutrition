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

	void OnTriggerEnter2D(Collider2D other)	
	{
		if (dropState.GetGameState() == GameplayController.GameState.STANDARD)
		{
			if (other.CompareTag("LogFood"))
			{
				log.CompareImage(other.GetComponent<Image>());
				Destroy(other.gameObject);
			}
			else if (other.CompareTag("NotFood"))
			{
				log.CompareImage(other.GetComponent<Image>());

				//Apply stun effect if the food acquired is not actually a food item.
				robotStunEffect.ApplyStun();
				Destroy(other.gameObject);
			}
		}
		else
		{

			if (other.CompareTag("LogFood"))
			{
				m_challengeFoodCount++;
				if(true)
				{
					m_goodChoices++;
				}

				Destroy(other.gameObject);
			}
			else if (other.CompareTag("NotFood"))
			{
				robotStunEffect.ApplyStun();
				Destroy(other.gameObject);
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
