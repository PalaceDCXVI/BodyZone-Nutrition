using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScreenResolution : MonoBehaviour {


	public int screenWidth;
	public int screenHeight;


	void Awake()
	{
		Debug.Log(Screen.width + " " + Screen.height);
		Screen.SetResolution(screenWidth, screenHeight, true);
	}
}
