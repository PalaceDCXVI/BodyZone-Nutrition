using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogTab : MonoBehaviour {

	List<LogItem> ItemsOnPage;
	// Use this for initialization
	void Start () 
	{
		GetComponentsInChildren<LogItem>(ItemsOnPage);	
	}

}
