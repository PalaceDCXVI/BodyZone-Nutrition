using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutrientSpawner : MonoBehaviour {

	public GameObject spawnedItem;
	public GameObject foodQueue;

	public float spawnRate = 5.0f;
	public float spawnRandomness = 0.0f;

	[Range(0.0f, 1.0f)]
	public float additionalSpawnChance = 0.0f;
	public float TimeUntilNextItemSpawn = 1.0f;
	public float SpawnTimeVariance = 0.3f;
	private float timer = 1.0f;


	[Range(0.0f, 1.0f)] public float LoggableFoodSelectionBarrier = 0.9f;
	[Range(0.0f, 1.0f)]public float OtherFoodSelectionBarrier = 0.33f;
	public Sprite[] LogFoods;
	public Sprite[] OtherFoods;
	public Sprite[] NotFoods;

	private RectTransform rectTransform;

	private RectTransform canvasRectTransform;

	// Use this for initialization
	void Start() 
	{
		rectTransform = GetComponent<RectTransform>();
		canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
		Debug.Log(canvasRectTransform.localScale.x);

		timer = spawnRate + Random.Range(-spawnRandomness, +spawnRandomness);	
	}
	
	// Update is called once per frame
	void Update() 
	{

		timer -= Time.deltaTime;
		if (timer < 0.0f)
		{
			// Spawns object based on time
			SpawnObject(transform.position);

			// Additional chance to spawn an item for variance
			if (Random.Range(0.0f, 1.0f) < additionalSpawnChance)
			{
				SpawnObject(transform.position);
			}

			timer = TimeUntilNextItemSpawn + Random.Range(-SpawnTimeVariance, SpawnTimeVariance);
		}
	}

	public void SpawnObject(Vector3 position)
	{
		// Spawns the food somewhere within the nutrient spawner
		GameObject newFoodItem = Instantiate(spawnedItem, transform);
		newFoodItem.transform.Translate((new Vector2(Random.Range(-rectTransform.rect.width, rectTransform.rect.width), Random.Range(-rectTransform.rect.height, rectTransform.rect.height)) / 2.0f + rectTransform.anchoredPosition) * canvasRectTransform.localScale.x);
		
		float foodSelection = Random.Range(0.0f, 1.0f);
		NutrientLife.FoodType selectedType = NutrientLife.FoodType.LogFood;
		Sprite selectedSprite;

		if (foodSelection >= LoggableFoodSelectionBarrier)
		{
			selectedType = NutrientLife.FoodType.LogFood;
			selectedSprite = LogFoods[Random.Range(0, LogFoods.Length)]; //int Random.Range is [inclusive, exclusive];

			newFoodItem.GetComponent<NutrientLife>().SetFoodType(selectedType, selectedSprite);
		}
		else if (foodSelection >= OtherFoodSelectionBarrier)
		{
			selectedType = NutrientLife.FoodType.NotFood;
			selectedSprite = NotFoods[Random.Range(0, NotFoods.Length)];

			newFoodItem.GetComponent<NutrientLife>().SetFoodType(selectedType, selectedSprite);
		}
		else
		{
			selectedType = NutrientLife.FoodType.OtherFood;
			selectedSprite = OtherFoods[Random.Range(0, OtherFoods.Length)];

			newFoodItem.GetComponent<NutrientLife>().SetFoodType(selectedType, selectedSprite);	
		}
	}
}
