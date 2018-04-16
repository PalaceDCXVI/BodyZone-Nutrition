using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoading : MonoBehaviour {

	public GameObject LevelLoadingObject;

	private static LevelLoading singleton = null;

	public static string LoadingSceneName = "LevelLoading";

	public static LevelLoading Instance
	{
		get 
		{
			if (singleton == null) 
			{
				GameObject newObject = GameObject.Instantiate(GameObject.Instantiate(new GameObject(), Vector3.zero, Quaternion.identity));
				singleton = (LevelLoading)newObject.AddComponent(typeof(LevelLoading));
				DontDestroyOnLoad(newObject);
				newObject.name = "LevelLoading";

			}
			return singleton;
		}
	}

	public LevelLoading()
	{
		//Add a lambda to the active scene changed in order to destroy the 
		SceneManager.activeSceneChanged += OnSceneClosed;	 
	}

	public void OnSceneClosed(Scene previousScene, Scene newScene)
	{
		for (int i = 0, count = SceneManager.sceneCount; i < count; i++)
		{
			Scene currentScene = SceneManager.GetSceneByBuildIndex(i);
			if (currentScene.name == LoadingSceneName)
			{
				SceneManager.UnloadSceneAsync(LoadingSceneName);
			}
		}
	}

	//Will create a level loading scene immediately and load the chosen scene asynchronous
	public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
	{
		//Load loading scene immediately.
		SceneManager.LoadScene(LoadingSceneName, LoadSceneMode.Additive);

		StartCoroutine(LoadSceneRoutine(sceneName, mode));
	}

	private IEnumerator LoadSceneRoutine(string sceneName, LoadSceneMode mode)
	{
		//Then load the scene we want to load.
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, mode);
		yield return null;
	}

	public void Remove(string sceneName)
	{
		SceneManager.UnloadSceneAsync(sceneName);
	}
}
