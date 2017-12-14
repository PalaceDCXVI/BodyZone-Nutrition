using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour {

	public NutrientDropState dropState;
	public LogManager log;

	void OnTriggerEnter2D(Collider2D other)	
	{
		if (other.CompareTag("LogFood") || other.CompareTag("BadFood"))
		{
			log.CompareImage(other.GetComponent<Image>());
			Destroy(other.gameObject);
		}
	}
}
