using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogRobotAnimationController : MonoBehaviour {

	public List<Image> imagesToSpit = null;
	private List<Vector3> originalPositions = new List<Vector3>();

	// Use this for initialization
	void Start () 
	{
		imagesToSpit = LogDatabase.itemsCollected;
	}

	public void BeginSpitAnimation()
	{
		for (int i = 0; i < imagesToSpit.Count; i++)
		{
			// find item in list of images.
			for (int j = 0, count = LogDatabase.inst.foodItemsInChildren.Count; j < count; j++)
			{
				if (LogDatabase.inst.foodItemsInChildren[j].logImage.sprite == imagesToSpit[i].sprite)
				{
					originalPositions.Add(LogDatabase.inst.foodItemsInChildren[j].logImage.GetComponent<RectTransform>().position);
					LogDatabase.inst.foodItemsInChildren[j].logImage.GetComponent<RectTransform>().position = transform.position;
				}
			}
		}

		StartCoroutine(SpitImagesAtLogScreen());
	}
	
	IEnumerator SpitImagesAtLogScreen()
	{
		for (int i = 0; i < imagesToSpit.Count; i++)
		{
			// find item in list of images.
			for (int j = 0, count = LogDatabase.inst.foodItemsInChildren.Count; j < count; j++)
			{
				if (LogDatabase.inst.foodItemsInChildren[j].logImage.sprite == imagesToSpit[i].sprite)
				{
					Image currentImage = LogDatabase.inst.foodItemsInChildren[j].logImage;
					yield return MoveImageBackToPosition(currentImage, originalPositions[i]);

					break;
				}

			//Move to next item.
			}
		}
		yield return null;
	}

	IEnumerator MoveImageBackToPosition(Image selectedImage, Vector3 startPosition)
	{
		RectTransform imageTransform = selectedImage.GetComponent<RectTransform>();
		Vector3 originalPosition = imageTransform.position;
		float deltaT = 0.0f;
		Debug.Log("Object moving.");
		while((imageTransform.position - startPosition).magnitude > 0.01)
		{
			// Move image along path to it's original position.
			imageTransform.position = Vector3.Lerp(originalPosition, startPosition, Mathf.Clamp(deltaT, 0.0f, 1.0f));
			deltaT += Time.deltaTime;
			Debug.Log(deltaT);
			yield return null;
		}
	}
}
