using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NutrientLife : MonoBehaviour {

	public bool isGoodFood {get; private set; }
	public Sprite[] GoodFoods;
	public Sprite[] BadFoods;

	public float lifeTime = 5.0f;
	private float currentLife;

	// Use this for initialization
	void Start ()
	{
		currentLife = lifeTime;

		if (Random.Range(0.0f, 1.0f) > 0.5f)
		{
			isGoodFood = true;
		}
		else
		{
			isGoodFood = false;
		}
		if (isGoodFood)
		{
			GetComponent<Image>().overrideSprite = GoodFoods[Random.Range(0, GoodFoods.Length)]; //int Random.Range is [inclusive, exclusive];
		}
		else
		{
			GetComponent<Image>().overrideSprite = BadFoods[Random.Range(0, BadFoods.Length)];	//int Random.Range is [inclusive, exclusive];
		}
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
}
