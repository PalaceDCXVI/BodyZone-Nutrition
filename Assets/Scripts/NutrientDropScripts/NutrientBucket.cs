using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutrientBucket : MonoBehaviour {

	public bool Touch;
	private RectTransform rectTransform;

	public NutrientDropState dropState;

	public float moveSpeed = 1.0f;

	// Use this for initialization
	void Start () 
	{
		rectTransform = GetComponent<RectTransform>();
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
				GetComponent<Rigidbody2D>().AddForce(new Vector2(-moveSpeed, 0));
				GetComponent<Rigidbody2D>().rotation = 20f;

     		}
			// When clicking to the right of the robot
     		else if (Input.mousePosition.x > rectTransform.position.x)
     		{
				Debug.Log ("Clicked Right Side");
        		// Move Robot Right and slightly tilt
				GetComponent<Rigidbody2D>().AddForce(new Vector2(moveSpeed, 0));
				GetComponent<Rigidbody2D>().rotation = -20f;
     		}
 		}
		else
		{
			// Slow Robot when there is no input, and reset rotation
			GetComponent<Rigidbody2D>().AddForce(new Vector2 (-GetComponent<Rigidbody2D>().velocity.x * 2,0));
			GetComponent<Rigidbody2D>().rotation = 0f;
		}
	}

	void OnTriggerEnter2D(Collider2D other)	
	{
		NutrientLife lifeComponent = other.gameObject.GetComponent<NutrientLife>();
		if (lifeComponent != null)
		{
			//Add in score stuff here.
			lifeComponent.AddToFoodQueue();
			dropState.AddFood(lifeComponent.isGoodFood);
			Destroy(other.gameObject);
		}
	}
}
