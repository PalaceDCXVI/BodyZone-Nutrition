using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour {

	public NutrientDropState dropState;
	public LogManager log;

	void OnTriggerEnter2D(Collider2D other)	
	{
		NutrientLife lifeComponent = other.gameObject.GetComponent<NutrientLife>();
		if (lifeComponent != null)
		{
			//Add in score stuff here.
			lifeComponent.AddToFoodQueue();
			dropState.AddFood(lifeComponent.isGoodFood);
			log.CompareImage(other.GetComponent<Image>());
			Destroy(other.gameObject);
		}
	}
}
