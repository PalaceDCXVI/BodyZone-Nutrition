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
		//FQ_DragHandler.m_object.transform.SetParent(transform);
		Debug.Log("Fed robot food #: "+FQ_DragHandler.m_object.GetComponent<NutrientLife>().m_foodTypeIndex);
		m_wantedFood.sprite=FQ_FoodLineupHandler.inst.GetFood(FQ_DragHandler.m_object.GetComponent<NutrientLife>().m_foodTypeIndex);
	}
}
