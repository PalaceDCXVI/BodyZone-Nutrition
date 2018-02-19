using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Handles lifetime for the nutrients, along with the type of food that it is.
public class NutrientLife : MonoBehaviour {

	public enum FoodType
	{
		LogFood,
		OtherFood,
		NotFood
	}

	public FoodType foodType;

	public float lifeTime = 5.0f;
	private float currentLife;

	public float gravityCore = 8.0f;
	public float gravityVariance = 1.0f;

	void Start ()
	{
		currentLife = lifeTime;

		GetComponent<Rigidbody2D>().gravityScale = gravityCore + Random.Range(-gravityVariance, +gravityVariance);
	}
	
	void Update () 
	{
		currentLife -= Time.deltaTime;
		if (currentLife <= 0.0f)
		{
			DestroyImmediate(gameObject);
		}	
	}

	public void SetFoodType(FoodType type, Sprite selectedSprite)
	{
		foodType = type;

		switch (foodType)
		{
			case FoodType.LogFood:
			tag = "LogFood";
			break;

			case FoodType.NotFood:
			tag = "OtherFood";
			break;

			case FoodType.OtherFood:
			tag = "NotFood";
			break;

			default:
			break;
		}

		GetComponent<Image>().sprite = selectedSprite; 
	}
}
