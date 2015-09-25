﻿using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {

	public bool fireOnce = true;
	public string eventKey;

	private bool _isTriggered = false;
	private bool _triggerUsed = false;

	public bool IsTriggered { 
		get{
			if(fireOnce && _triggerUsed)
				return false;

			else if(!_triggerUsed && _isTriggered)
				_triggerUsed = true;
				
			return _isTriggered;
		}
		set{
			_isTriggered = value;
		}
	}

	void Awake()
	{
		EventPool eventPool = GameObject.FindObjectOfType<EventPool> ();
		eventPool.AddTriggerToEvent (eventKey, this);
	}
}
