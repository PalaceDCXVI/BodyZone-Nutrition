using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogManager : MonoBehaviour {

	public List<Image> logImages;

	public NutrientDropState dropState;
	public int FoundItems = 0;

	// Use this for initialization
	void Start () {
		GetComponentsInChildren<Image>(logImages);
		logImages.RemoveAt(0);
	}
	
	public void CompareImage(Image image)
	{
		foreach (Image item in logImages)
		{
			if (item.sprite == image.sprite && item.color != Color.white)
			{
				item.GetComponent<LogItem>().RevealItem();
				FoundItems++;
				break;
			}
		}
		if (FoundItems >= logImages.Count)
		{
			NutrientDatabaseInterface.itemsCollected = new List<Image>(logImages);
			dropState.EndGame();
		}
	}
}
