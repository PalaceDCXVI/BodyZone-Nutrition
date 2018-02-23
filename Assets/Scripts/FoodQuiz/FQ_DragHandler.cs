﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FQ_DragHandler:MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{
	public static GameObject m_object;
	public Vector3 m_startPosition;
	public Transform m_startParent;

	public void OnBeginDrag(PointerEventData eventData) {
		m_object=gameObject;
		m_startPosition=transform.position;
		m_startParent=transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts=false;
	}
	public void OnDrag(PointerEventData eventData) {
		transform.position=eventData.position; 
	}
	public void OnEndDrag(PointerEventData eventData) {
		m_object=null;
		GetComponent<CanvasGroup>().blocksRaycasts=true;
		if(transform.parent==m_startParent)	transform.position=m_startPosition;
	}


	void Start() {

	}
	
	void Update() {

	}
}
