using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Handles lifetime for the nutrients, along with the type of food that it is.
public class NutrientLife : MonoBehaviour {

	private FoodQueue foodQueue;

	public enum FoodType
	{
		LogFood,
		OtherFood,
		NotFood
	}

	[Range(0.0f, 1.0f)] public float LoggableFoodBarrier = 0.66f;
	[Range(0.0f, 1.0f)]public float OtherFoodBarrier = 0.33f;
	public FoodType foodType {get; private set; }
	public Sprite[] LogFoods;
	public Sprite[] OtherFoods;
	public Sprite[] NotFoods;

	public float lifeTime = 5.0f;
	private float currentLife;

	public float gravityCore = 8.0f;
	public float gravityVariance = 1.0f;

	// Use this for initialization
	void Start ()
	{
		currentLife = lifeTime;

		float foodSelection = Random.Range(0.0f, 1.0f);
		if (foodSelection >= LoggableFoodBarrier)
		{
			foodType = FoodType.LogFood;
			GetComponent<Image>().sprite = LogFoods[Random.Range(0, LogFoods.Length)]; //int Random.Range is [inclusive, exclusive];
			tag = "LogFood";
		}
		else if (foodSelection >= OtherFoodBarrier)
		{
			foodType = FoodType.OtherFood;
			GetComponent<Image>().sprite = OtherFoods[Random.Range(0, OtherFoods.Length)]; //int Random.Range is [inclusive, exclusive];
			tag = "OtherFood";
		}
		else
		{
			foodType = FoodType.NotFood;
			GetComponent<Image>().sprite = NotFoods[Random.Range(0, NotFoods.Length)];	//int Random.Range is [inclusive, exclusive];
			tag = "NotFood";
		}

		foodQueue = transform.parent.GetComponent<NutrientSpawner>().foodQueue.GetComponent<FoodQueue>();

		GetComponent<Rigidbody2D>().gravityScale = gravityCore + Random.Range(-gravityVariance, +gravityVariance);
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentLife -= Time.deltaTime;
		if (currentLife <= 0.0f)
		{
			Destroy(gameObject);
		}	
	}

	public void AddToFoodQueue()
	{
		foodQueue.AddEatenFood(gameObject);
	}
}
