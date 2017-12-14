using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour {

	public NutrientDropState dropState;
	public LogManager log;

	void OnTriggerEnter2D(Collider2D other)	
	{
		if (other.CompareTag("LogFood"))
		{
			log.CompareImage(other.GetComponent<Image>());
			Destroy(other.gameObject);
		}
		else if (other.CompareTag("NotFood"))
		{
			log.CompareImage(other.GetComponent<Image>());
			// TODO: Robot gets hurt or something here
		}
	}
}
