using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnimationController : MonoBehaviour {

	Animator animator;

	public bool isStunned;

	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		isStunned = GetComponent<StunEffect>().isStunned;
		animator.SetBool("IsStunned", isStunned);
	}
}
