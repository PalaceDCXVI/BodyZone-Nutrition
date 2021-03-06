﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StunEffect:MonoBehaviour{
	public Color		m_stunColor;
	public UnityEvent StunEffects;
	public UnityEvent UnStunEffects;

	[Range(0.1f, 3.0f)]
	public float stunLength = 1.0f;
	public bool isStunned = false;

	public void ApplyStun()
	{
		if(!isStunned)
		{
			StartCoroutine(InvokeStunEffects());
		}
	}

	public IEnumerator InvokeStunEffects()
	{
		StunEffects.Invoke();
		isStunned = true;
		GetComponent<Image>().color=m_stunColor;

		yield return new WaitForSecondsRealtime(stunLength);

		UnStunEffects.Invoke();
		isStunned = false;
		GetComponent<Image>().color=Color.white;
	}
}
