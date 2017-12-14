using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogManager : MonoBehaviour {

	Image[] logImages;

	//TODO: Add in tracking for level end here

	// Use this for initialization
	void Start () {
		logImages = GetComponentsInChildren<Image>();
	}
	
	public void CompareImage(Image image)
	{
		foreach (Image item in logImages)
		{
			if (item.sprite == image.sprite)
			{
				item.GetComponent<LogItem>().RevealItem();
				break;
			}
		}
	}
}
