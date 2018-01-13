using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddScene : MonoBehaviour {

	public void Add(string sceneName)
	{
		SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
	}

	public void Remove(string sceneName)
	{
		SceneManager.UnloadSceneAsync(sceneName);
	}
}
