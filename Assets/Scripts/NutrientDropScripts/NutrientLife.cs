using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NutrientLife : MonoBehaviour {

	private FoodQueue foodQueue;

	public enum FoodType
	{
		LogFood,
		Neutral,
		BadFood
	}

	[Range(0.0f, 1.0f)] public float LoggableFoodBarrier = 0.66f;
	[Range(0.0f, 1.0f)]public float NeutralFoodBarrier = 0.33f;
	public FoodType foodType {get; private set; }
	public Sprite[] LogFoods;
	public Sprite[] NeutralFoods;
	public Sprite[] BadFoods;

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
		else if (foodSelection >= NeutralFoodBarrier)
		{
			foodType = FoodType.Neutral;
			GetComponent<Image>().sprite = NeutralFoods[Random.Range(0, NeutralFoods.Length)]; //int Random.Range is [inclusive, exclusive];
			tag = "NeutralFood";
		}
		else
		{
			foodType = FoodType.BadFood;
			GetComponent<Image>().sprite = BadFoods[Random.Range(0, BadFoods.Length)];	//int Random.Range is [inclusive, exclusive];
			tag = "BadFood";
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
