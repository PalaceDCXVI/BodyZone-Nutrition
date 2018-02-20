using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutrientBucket : MonoBehaviour {

	public bool Touch;
	private RectTransform rectTransform;
	private Rigidbody2D newRigidbody2D;

	public float moveSpeed = 1.0f;
	private float moveSpeedScale = 1.0f; public void SetMoveSpeedScale(float scale) { moveSpeedScale = scale; }

	// Use this for initialization
	void Start () 
	{
		rectTransform = GetComponent<RectTransform>();
		newRigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{

		// Robot's Movement, based on Rigid body velocity and which side of the screen is clicked
		if (Input.GetMouseButton(0))
 		{
     		// When clicking to the left of the robot
     		if (Input.mousePosition.x < rectTransform.position.x)
     		{
				//Debug.Log ("Clicked Left Side");
      			// Move robot left and slightly tilt
				newRigidbody2D.AddForce(new Vector2(-moveSpeed * moveSpeedScale, 0));
				newRigidbody2D.rotation = 20f;
				newRigidbody2D.drag = 0.0f;

     		}
			// When clicking to the right of the robot
     		else if (Input.mousePosition.x > rectTransform.position.x)
     		{
				//Debug.Log ("Clicked Right Side");
        		// Move Robot Right and slightly tilt
				newRigidbody2D.AddForce(new Vector2(moveSpeed * moveSpeedScale, 0));
				newRigidbody2D.rotation = -20f;
				newRigidbody2D.drag = 0.0f;
     		}
 		}
		else
		{
			// Slow Robot when there is no input, and reset rotation
			newRigidbody2D.drag = 2.0f;
			newRigidbody2D.rotation = 0f;
		}
	}

	public void StartInput()
	{
		if (newRigidbody2D != null)
		{
			newRigidbody2D.rotation = 0f;
			newRigidbody2D.drag = 0.0f;
		}
		enabled = true;
	}

	public void StopInput()
	{
		if (newRigidbody2D != null)
		{
			newRigidbody2D.rotation = 0f;
			newRigidbody2D.drag = 5.0f;
		}
		enabled = false;
	}
}
