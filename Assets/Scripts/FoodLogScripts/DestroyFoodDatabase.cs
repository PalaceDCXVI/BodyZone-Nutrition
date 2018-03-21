using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DestroyFoodDatabase : MonoBehaviour {

	void OnApplicationQuit()
	{
		//TODO: DELETE THIS BEFORE RELEASE
		File.Delete(Application.persistentDataPath + "/FoodItems.xml");
	}
}
