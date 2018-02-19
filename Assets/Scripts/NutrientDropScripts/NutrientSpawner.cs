using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutrientSpawner : MonoBehaviour {
	public	GameObject	m_spawnZone;        //Zone where food spawns.

	private RectTransform	mp_SZ_rectTransform;
	private RectTransform	mp_canvasRectTransform;


	public GameObject spawnedItem;
	public GameObject foodQueue;


	[Tooltip("Average time between the spawn of each new item")]
	public float spawnRate = 5.0f;
	public float spawnRandomness = 0.0f;

	[Range(0.0f, 1.0f)]
	public float additionalSpawnChance = 0.0f;
	public float TimeUntilNextItemSpawn = 1.0f;
	public float SpawnTimeVariance = 0.3f;
	private float timer = 1.0f;

	[Tooltip("Time until the currently wanted food will spawn;")]
	[Range(2.0f, 10.0f)]
	public float WantedFoodTimer = 0.0f;
	private float currentWantedFoodTimer = 0.0f;
	public WantedFood wantedFood;


	[Range(0.0f, 1.0f)] public float LoggableFoodSelectionBarrier = 0.9f;
	[Range(0.0f, 1.0f)]public float OtherFoodSelectionBarrier = 0.33f;
	public Sprite[] LogFoods;
	public Sprite[] OtherFoods;
	public Sprite[] NotFoods;

	

	void Start()
	{
		mp_SZ_rectTransform = m_spawnZone.GetComponent<RectTransform>();
		mp_canvasRectTransform = m_spawnZone.GetComponentInParent<Canvas>().GetComponent<RectTransform>();

		timer = spawnRate + Random.Range(-spawnRandomness, +spawnRandomness);
		currentWantedFoodTimer = WantedFoodTimer;
	}
	
	void Update()
	{

		timer -= Time.deltaTime;
		currentWantedFoodTimer -= Time.deltaTime;
		if (timer < 0.0f)
		{
			// Spawns object based on time
			SpawnObject(mp_SZ_rectTransform.position);

			// Additional chance to spawn an item for variance
			if (Random.Range(0.0f, 1.0f) < additionalSpawnChance)
			{
				SpawnObject(mp_SZ_rectTransform.position);
			}

			timer = TimeUntilNextItemSpawn + Random.Range(-SpawnTimeVariance, SpawnTimeVariance);
		}

		if (currentWantedFoodTimer < 0.0f)
		{
			SpawnWantedObject();
			timer = TimeUntilNextItemSpawn + Random.Range(-SpawnTimeVariance, SpawnTimeVariance);
			currentWantedFoodTimer = WantedFoodTimer;
		}
	}

	public void SpawnObject(Vector3 position)
	{
		// Spawns the food somewhere within the nutrient spawner
		GameObject newFoodItem = Instantiate(spawnedItem, mp_SZ_rectTransform);
		newFoodItem.transform.Translate((new Vector2(Random.Range(-mp_SZ_rectTransform.rect.width, mp_SZ_rectTransform.rect.width), Random.Range(-mp_SZ_rectTransform.rect.height, mp_SZ_rectTransform.rect.height)) / 2.0f + mp_SZ_rectTransform.anchoredPosition) * mp_canvasRectTransform.localScale.x);
		
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

		if (selectedSprite == wantedFood.currentWantedFood.sprite)
		{
			currentWantedFoodTimer = WantedFoodTimer;
		}
	}

	void SpawnWantedObject()
	{
		GameObject newFoodItem = Instantiate(spawnedItem, mp_SZ_rectTransform);
		newFoodItem.transform.Translate((new Vector2(Random.Range(-mp_SZ_rectTransform.rect.width, mp_SZ_rectTransform.rect.width), Random.Range(-mp_SZ_rectTransform.rect.height, mp_SZ_rectTransform.rect.height)) / 2.0f + mp_SZ_rectTransform.anchoredPosition) * mp_canvasRectTransform.localScale.x);
		newFoodItem.GetComponent<NutrientLife>().SetFoodType(NutrientLife.FoodType.LogFood, wantedFood.currentWantedFood.sprite);
	}
}
