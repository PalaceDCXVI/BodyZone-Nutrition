using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class LogRobotAnimationController : MonoBehaviour {

	public List<Image> imagesToSpit = null;
	private List<Vector3> originalPositions = new List<Vector3>();

	public RectTransform spitPointTransform;

	// Use this for initialization
	void Start () 
	{
		imagesToSpit = LogDatabase.itemsCollected;
	}

	public void PrepSpitAnimation()
	{
		for (int i = 0; i < imagesToSpit.Count; i++)
		{
			// find item in list of images.
			for (int j = 0, count = LogDatabase.inst.foodItemsInChildren.Count; j < count; j++)
			{
				if (LogDatabase.inst.foodItemsInChildren[j].logImage.sprite == imagesToSpit[i].sprite)
				{
					RectTransform logImageTransform = LogDatabase.inst.foodItemsInChildren[j].logImage.GetComponent<RectTransform>();
					originalPositions.Add(logImageTransform.position);
					logImageTransform.position = transform.position;
					logImageTransform.localScale = Vector3.zero;
				}
			}
		}
	}

	public void BeginSpitAnimation()
	{
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

	IEnumerator MoveImageBackToPosition(Image selectedImage, Vector3 originalPosition)
	{
		RectTransform imageTransform = selectedImage.GetComponent<RectTransform>();

		Vector3 startPosition = spitPointTransform.position - originalPosition;
		startPosition *= (1.0f / transform.root.localScale.x);

		Debug.Log(startPosition);
		Debug.Log(originalPosition);
		Debug.Log(spitPointTransform.position);
		AnimationClip movementClip = new AnimationClip();
		movementClip.legacy = true;
		movementClip.wrapMode = WrapMode.Once;

		AnimationCurve movementXCurve = AnimationCurve.EaseInOut(0.0f, startPosition.x, 1.4f, 0);
		AnimationCurve movementYCurve = AnimationCurve.EaseInOut(0.0f, startPosition.y, 1.4f, 0);
		AnimationCurve movementScaleCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.4f, 1.0f);

		Keyframe movementXStartKey = new Keyframe(0.0f, startPosition.x, 1.0f, 1.0f);
		Keyframe movementYStartKey = new Keyframe(0.0f, startPosition.y, 1.0f, 1.0f);
		Keyframe scaleStartKey = new Keyframe(0.0f, 0, 1.0f, 1.0f);

		movementXCurve.MoveKey(0, movementXStartKey);
		movementYCurve.MoveKey(0, movementYStartKey);
		movementScaleCurve.MoveKey(0, scaleStartKey);
		
		movementClip.SetCurve("", typeof(RectTransform), "m_AnchoredPosition.x", movementXCurve);
		movementClip.SetCurve("", typeof(RectTransform), "m_AnchoredPosition.y", movementYCurve);
		movementClip.SetCurve("", typeof(RectTransform), "localScale.x", movementScaleCurve);
		movementClip.SetCurve("", typeof(RectTransform), "localScale.y", movementScaleCurve);

		Animation imageAnimation = selectedImage.GetComponent<Animation>();
		imageAnimation.AddClip(movementClip, "movementClip");
		imageAnimation.Play("movementClip");

		yield return new WaitForSecondsRealtime(1.0f);
	}

	public void TriggerOutTransition()
	{
		GetComponent<Animator>().SetTrigger("T_Leave");
	}
}
