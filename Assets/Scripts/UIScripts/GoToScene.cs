using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour {

	public string sceneName;

	public void LoadScene()
	{
		SceneManager.LoadScene(sceneName);
	}

	public void LoadScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}

	public void LoadSceneAsync()
	{
		SceneManager.LoadSceneAsync(sceneName);
	}

	public void LoadSceneAsync(string scene)
	{
		SceneManager.LoadSceneAsync(scene);
	}
}
