using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {

	public bool fireOnce = true;

	private bool _isTriggered = false;
	private bool _triggerUsed = false;

	public bool IsTriggered { 
		get{
			if(fireOnce && _triggerUsed)
				return false;
			return _isTriggered;
		}
		set{
			_isTriggered = value;
			_triggerUsed = value;
		}
	}
}
