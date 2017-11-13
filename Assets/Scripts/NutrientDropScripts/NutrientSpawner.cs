using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutrientSpawner : MonoBehaviour {

	public GameObject spawnedItem;

	public float spawnRate = 5.0f;
	public float timer = 0.0f;

	private BoxCollider2D boxCollider;

	// Use this for initialization
	void Start () 
	{
		boxCollider = GetComponent<BoxCollider2D>();

		timer = spawnRate;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer -= Time.deltaTime;
		if (timer < 0.0f)
		{
			Instantiate(spawnedItem, transform)
				.transform.Translate(new Vector3(Random.Range(-boxCollider.bounds.extents.x, boxCollider.bounds.extents.x), Random.Range(-boxCollider.bounds.extents.y, boxCollider.bounds.extents.y), 0.0f));
			timer = spawnRate;
		}
	}

	public void SpawnObject(Vector3 position)
	{
		Instantiate(spawnedItem, transform)
			.transform.Translate(new Vector3(Random.Range(-boxCollider.bounds.extents.x, boxCollider.bounds.extents.x), Random.Range(-boxCollider.bounds.extents.y, boxCollider.bounds.extents.y), 0.0f));
		
	}
}
