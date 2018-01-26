using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NutrientDropLevelTimer : MonoBehaviour {

	public Text timerDisplay;

	public float maxTime;
	float currentTime;

	public UnityEvent TimerEvents;

	// Use this for initialization
	void OnEnable()
	{
		currentTime = maxTime;
		timerDisplay.text = currentTime.ToString("0:00");
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentTime -= Time.deltaTime;
		timerDisplay.text = currentTime.ToString("0:00");
		if (currentTime <= 0)
		{
			TimerEvents.Invoke();
		}
	}

	public void ResetTimer()
	{
		currentTime = maxTime;
	}
}
