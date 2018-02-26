using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Handles the robot's wanted food and dropping a food onto the robot.
/// </summary>

public class FQ_RobotHandler:MonoBehaviour, IDropHandler{
	public static FQ_RobotHandler inst;
	public Image				m_wantedFood;		//Food icon.
	public Sprite				m_S_unknownFood;	//Image for an unknown food.
	public Animator				m_animator;			//The robot's animator.

	private void Awake(){
		if(inst==null) inst=this;
		else {
			Debug.Log("FQ_RobotHandler destroyed on '"+gameObject.name+"'. Are there duplicates in the scene?");
			DestroyImmediate(this);
		}
	}
	void Start(){}

	void Update(){}

	public void OnDrop(PointerEventData eventData){
		//Handles a food being dropped onto the robot, feeding it.
		//Debug.Log("Fed robot food #: "+FQ_DragHandler.m_object.GetComponent<NutrientLife>().m_foodTypeIndex);

		//Set the sprite in the robot's stomach.
		m_wantedFood.sprite=FQ_FoodLineupHandler.inst.GetFood(FQ_DragHandler.m_object.GetComponent<NutrientLife>().m_foodTypeIndex);
		//React to the fed food.
		FQ_GameController.inst.RobotFed(FQ_DragHandler.m_object.GetComponent<NutrientLife>().m_foodTypeIndex);
	}

	public void AnimateRobotLeaving() {
		//The robot leaves and a new one comes in.
		if(!FQ_GameController.inst.QuizOver()) {
			m_animator.SetTrigger("Go_Robot_Out");
			m_animator.SetTrigger("Go_Robot_AnimateOut");
		}
		else {	//If the quiz is over, the robot leaves and a new one doesn't come in.
			m_animator.SetTrigger("Go_Robot_LeaveRight");
			m_animator.SetTrigger("Go_Robot_AnimateOut");
		}
	}
	public void AnimateNewRobotIn() {
		//The robot has died. Animate a new one coming in.
		if(!FQ_GameController.inst.QuizOver()) {
			m_animator.SetTrigger("Go_Robot_MoveInFromLeft");
			m_animator.SetTrigger("Go_Robot_AnimateMoveRight");
		}
		else {	//The quiz is over. Instead of bringing in a new robot, start the outro conversation.
			FQ_GameController.inst.StartOutro();
		}
	}
	public void AnimateRobotExplode() {
		//Robot fed the wrong food, it explodes.
		m_animator.SetTrigger("Go_Robot_AnimateExplode");
	}
	public void SetWantedFoodUnknown() {
		//Set's the robot's wanted food to be unknown.
		m_wantedFood.sprite=m_S_unknownFood;
	}
}
