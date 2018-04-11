using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {

	// Use this for initialization

	public void RestartGame()
	{
		LevelLoading.Instance.LoadScene(SceneManager.GetActiveScene().name);//Loads the current scene
	}
}
