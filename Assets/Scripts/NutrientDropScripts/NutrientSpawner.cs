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
	public float timer = 0.0f;

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
			Instantiate(spawnedItem, transform)
				.transform.Translate((new Vector2(Random.Range(-rectTransform.rect.width, rectTransform.rect.width), Random.Range(-rectTransform.rect.height, rectTransform.rect.height)) / 2.0f + rectTransform.anchoredPosition) * canvasRectTransform.localScale.x);
			timer = spawnRate + Random.Range(-spawnRandomness, +spawnRandomness);

			if (Random.Range(0.0f, 1.0f) < additionalSpawnChance)
			{
				Instantiate(spawnedItem, transform)
				.transform.Translate((new Vector2(Random.Range(-rectTransform.rect.width, rectTransform.rect.width), Random.Range(-rectTransform.rect.height, rectTransform.rect.height)) / 2.0f + rectTransform.anchoredPosition) * canvasRectTransform.localScale.x);
			}
		}
	}

	public void SpawnObject(Vector3 position)
	{
		Instantiate(spawnedItem, transform)
			.transform.Translate((new Vector2(Random.Range(-rectTransform.rect.width, rectTransform.rect.width), Random.Range(-rectTransform.rect.height, rectTransform.rect.height)) / 2.0f + rectTransform.anchoredPosition) * canvasRectTransform.localScale.x);
	}
}
