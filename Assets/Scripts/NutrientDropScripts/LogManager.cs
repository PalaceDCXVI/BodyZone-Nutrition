using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LogManager : MonoBehaviour {

	private List<Image> logImages = new List<Image>();

	public NutrientDropState dropState;

	public GameObject dialogueTrigger;


	// Use this for initialization
	void Start () {
		LogItem[] logItems = GetComponentsInChildren<LogItem>();

		foreach (LogItem item in logItems)
		{
			logImages.Add(item.GetComponent<Image>());
		}
	}
}
