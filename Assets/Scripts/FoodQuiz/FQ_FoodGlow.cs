using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles glowing a Food Lineup food.
/// </summary>

public class FQ_FoodGlow:MonoBehaviour{
	public Color		m_glowColor=Color.red;		//The color of the glow.
	public float		m_glowSpeed=1;				//Speed of the glow cycling on/off.
	public  bool		m_active=false;

	private Color		mp_normalColor;

	void Start(){
		mp_normalColor=GetComponent<Image>().color;
	}
	
	void Update(){
		if(m_active) {
			float _lerp=Mathf.Sin(Time.timeSinceLevelLoad*m_glowSpeed);
			if(_lerp<0) _lerp*=-1.0f;
			GetComponent<Image>().color=Color.Lerp(mp_normalColor,m_glowColor,_lerp);
		}
	}

	public bool Toggle() {
		//Toggle on/off.
		m_active=!m_active;

		if(!m_active) GetComponent<Image>().color=mp_normalColor;

		return m_active;
	}
	public void SetOnOff(bool _state) {
		//Set the active state explicitly.
		m_active=_state;
		GetComponent<Image>().color=mp_normalColor;
	}
}
