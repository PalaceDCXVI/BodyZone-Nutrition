using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutrientBucket : MonoBehaviour {

	public bool Touch;
	private RectTransform rectTransform;
	private Rigidbody2D rigidbody2D;

	public float moveSpeed = 1.0f;

	// Use this for initialization
	void Start () 
	{
		rectTransform = GetComponent<RectTransform>();
		rigidbody2D = GetComponent<Rigidbody2D>();
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
				 Debug.Log ("Clicked Left Side");
      			// Move robot left and slightly tilt
				rigidbody2D.AddForce(new Vector2(-moveSpeed, 0));
				rigidbody2D.rotation = 20f;

     		}
			// When clicking to the right of the robot
     		else if (Input.mousePosition.x > rectTransform.position.x)
     		{
				Debug.Log ("Clicked Right Side");
        		// Move Robot Right and slightly tilt
				rigidbody2D.AddForce(new Vector2(moveSpeed, 0));
				rigidbody2D.rotation = -20f;
     		}
 		}
		else
		{
			// Slow Robot when there is no input, and reset rotation
			rigidbody2D.AddForce(new Vector2 (-rigidbody2D.velocity.x * 2,0));
			rigidbody2D.rotation = 0f;
		}
	}

	public void StartInput()
	{
		if (rigidbody2D != null)
		{
			rigidbody2D.rotation = 0f;
			rigidbody2D.drag = 0.0f;
		}
		enabled = true;
	}

	public void StopInput()
	{
		if (rigidbody2D != null)
		{
			rigidbody2D.rotation = 0f;
			rigidbody2D.drag = 5.0f;
		}
		enabled = false;
	}
}
