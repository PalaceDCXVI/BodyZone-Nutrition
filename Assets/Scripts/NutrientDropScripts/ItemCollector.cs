﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Collection component for the robot.
//Handles detection and response when the robot comes into contact with a food.
public class ItemCollector : MonoBehaviour {

	public NutrientDropState dropState;
	public LogManager log;
	public StunEffect robotStunEffect;

	void OnTriggerEnter2D(Collider2D other)	
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
		}
	}
}
