using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WantedFood : MonoBehaviour {

	public NutrientSpawner nutrientSpawner;
	public List<Sprite> givenGoodFoods;
	public Image currentWantedFood;
	public int currentWantedFoodIndex;
	 

	// Use this for initialization
	void Start () 
	{
		givenGoodFoods = new List<Sprite>(nutrientSpawner.LogFoods);

		//Randomizes the list.
		for (int i = 0, count = givenGoodFoods.Count; i < count-1; i++)
		{
			int randomIndex = UnityEngine.Random.Range(i, count);
			Sprite temp = givenGoodFoods[i];
			givenGoodFoods[i] = givenGoodFoods[randomIndex];
			givenGoodFoods[randomIndex] = temp;
		}

		currentWantedFood = GetComponent<Image>();
		currentWantedFood.sprite = givenGoodFoods[0];
		currentWantedFoodIndex = 0;
	}

	public void NextFood()
	{
		Debug.Log("Now what");
		currentWantedFoodIndex += 1;
		if (currentWantedFoodIndex >= givenGoodFoods.Count)
		{
			currentWantedFoodIndex--;
			//End level;
		}
		currentWantedFood.sprite = givenGoodFoods[currentWantedFoodIndex];
	}
}