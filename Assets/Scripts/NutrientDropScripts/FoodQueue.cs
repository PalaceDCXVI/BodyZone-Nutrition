using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodQueue : MonoBehaviour {

	public GameObject QueueBackground;
	public GameObject eatenFoodPrefab;

	public List<GameObject> eatenFood {get; private set;}
	public float foodSpeed = 50.0f;

	// Use this for initialization
	void Start () 
	{
		eatenFood = new List<GameObject>(10);
	}
	
	// Update is called once per frame
	void Update () 
	{
			foreach (GameObject item in eatenFood)
			{
				item.transform.Translate(foodSpeed * Time.deltaTime, 0, 0);
			}
	}

	public void AddEatenFood(GameObject eatenObject)
	{
		GameObject newObject = Instantiate(eatenFoodPrefab, transform);
		newObject.GetComponent<Image>().sprite = eatenObject.GetComponent<Image>().sprite;
		Vector3 newPosition = new Vector3(((-QueueBackground.GetComponent<RectTransform>().rect.width / 2.0f) - (eatenObject.GetComponent<RectTransform>().rect.width / 2.0f)), QueueBackground.GetComponent<RectTransform>().anchoredPosition.y, 0);
		newObject.GetComponent<RectTransform>().anchoredPosition = newPosition;
		eatenFood.Add(newObject);
	}
}
