using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutrientBucket : MonoBehaviour {

	public bool Touch;
	private RectTransform rectTransform;

	// Use this for initialization
	void Start () 
	{
		rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		rectTransform.anchoredPosition = new Vector3(Input.mousePosition.x * (1.0f / transform.parent.localScale.x), rectTransform.anchoredPosition.y, 0);
	}

	void OnTriggerEnter2D(Collider2D other)	
	{
		NutrientLife lifeComponent = other.gameObject.GetComponent<NutrientLife>();
		if (lifeComponent != null)
		{
			//Add in score stuff here.
			lifeComponent.AddToFoodQueue();
			Destroy(other.gameObject);
		}
	}
}
