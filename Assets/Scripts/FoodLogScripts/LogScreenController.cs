using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LogScreenController : MonoBehaviour {



	public UnityEvent logDialogue;


	// Use this for initialization
	void Start () {
		{
			logDialogue.Invoke();
		}
	}

}
