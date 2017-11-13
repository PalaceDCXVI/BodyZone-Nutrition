using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutrientLife : MonoBehaviour {

	public float lifeTime = 5.0f;
	private float currentLife;

	// Use this for initialization
	void Start () {
		currentLife = lifeTime;
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
