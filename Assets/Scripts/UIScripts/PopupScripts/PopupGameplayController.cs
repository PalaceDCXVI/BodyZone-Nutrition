using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupGameplayController : MonoBehaviour {

	public MonoBehaviour[] gameplayComponents;

	public void StartGame()
	{
		foreach (MonoBehaviour item in gameplayComponents)
		{
			item.enabled = true;
		}
	}

	public void PauseGame()
	{
		foreach (MonoBehaviour item in gameplayComponents)
		{
			item.enabled = false;
		}
	}
}
