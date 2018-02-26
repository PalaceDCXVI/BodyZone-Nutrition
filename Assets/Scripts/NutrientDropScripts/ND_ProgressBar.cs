using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the Progress Bar.
/// </summary>

public class ND_ProgressBar:MonoBehaviour{
	public static ND_ProgressBar inst;
	public Image	m_fill;			//Fill image.

	[Tooltip("Max/min height of the animation wave.")]
	[Range(0.0f, 0.5f)]
	public float	m_waveHeight;
	[Tooltip("Speed of the animation wave.")]
	[Range(0.0f, 10.0f)]
	public float	m_waveSpeed;
	[Tooltip("Seconds it takes the animation to change transition.")]
	[Range(0.0f, 3.0f)]
	public float	m_transitionTime;

	private float	m_oldPercent;		//Shown fill percent.
	private float	m_newPercent;		//Fill percent animating to.
	private float	m_startTime;		//Start timestamp of when the transition started.
	private bool	m_transitioning;

	private void Awake(){
		if(inst==null) inst=this;
		else{
			Debug.Log("ND_ProgressBar destroyed on '"+gameObject.name+"'. Are there duplicates in the scene?");
			DestroyImmediate(this);
		}
	}
	void Start(){
		m_fill.fillAmount=0;
		m_oldPercent=0;
		m_newPercent=0;
		m_startTime=0;
		m_transitioning=false;
	}
	
	void Update(){
		//Animate the progress bar.
		float _shownPercent=ND_RobotHandler.inst.GetCompletionPercentage();

		//If the completion percent changes, animate the transition.
		if(m_oldPercent!=ND_RobotHandler.inst.GetCompletionPercentage()){
			if(!m_transitioning) {
				m_transitioning=true;
				m_startTime=Time.timeSinceLevelLoad;
			}
			m_newPercent=ND_RobotHandler.inst.GetCompletionPercentage();

			float _percentComplete=(Time.timeSinceLevelLoad-m_startTime)/m_transitionTime;

			if(_percentComplete>=1) {
				_shownPercent=m_newPercent;
				m_oldPercent=_shownPercent;
				m_transitioning=false;
			}
			_shownPercent=Mathf.Lerp(m_oldPercent, m_newPercent, _percentComplete);
		}

		//Add floating.
		_shownPercent=_shownPercent+(Mathf.Sin(Time.timeSinceLevelLoad*m_waveSpeed)*m_waveHeight);

		m_fill.fillAmount=_shownPercent;
	}
}
