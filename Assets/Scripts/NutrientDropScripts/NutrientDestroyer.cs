using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destoys food as they fall off the screen.
/// </summary>

public class NutrientDestroyer:MonoBehaviour {

	
	void Start(){}

	void Update(){}

	void OnTriggerEnter2D(Collider2D other)	
	{
		NutrientLife nutrientLife = other.GetComponent<NutrientLife>();
		if (nutrientLife == null)
		{
			return;
		}

		if((other.gameObject.tag=="LogFood")||(other.gameObject.tag=="OtherFood")||(other.gameObject.tag=="NotFood")) {
			Destroy(other.gameObject);
		}
	}
}
