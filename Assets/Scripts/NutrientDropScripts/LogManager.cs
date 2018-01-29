using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogManager : MonoBehaviour {

	public List<Image> logImages;

	public NutrientDropState dropState;

	public GameObject dialogueTrigger;
	public int FoundItems = 0;

	// Use this for initialization
	void Start () {
		LogItem[] logItems = GetComponentsInChildren<LogItem>();

		foreach (LogItem item in logItems)
		{
			logImages.Add(item.GetComponent<Image>());
		}
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
		if (FoundItems >= logImages.Count && dropState.GetGameState() == GameplayController.GameState.STANDARD)
		{
			NutrientDatabaseInterface.itemsCollected = new List<Image>(logImages);
			//dropState.EndGame();
			dialogueTrigger.GetComponent<DialogueTrigger>().TriggerDialogue();

		}
	}
}
