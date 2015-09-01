using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///Updates all guards and acts as a trigger for events 
/// </summary>
public class GuardManager : MonoBehaviour, ITrigger {

	private List<BaseGuard> _guards = new List<BaseGuard>();
	public Transform guardTarget;

	void Start () {
		//Find all teh guards in the scene and add them to the collection
		GameObject[] guardObjects = GameObject.FindGameObjectsWithTag ("Guard");
		foreach (GameObject go in guardObjects) {
			BaseGuard guard = go.GetComponent<BaseGuard>();
			guard.targetName = guardTarget.name;
			_guards.Add(guard);
		}

		//alert no target
		if (guardTarget == null) {
			guardTarget = this.transform;
			Debug.Log("No Target Available");
		}
		//Find the event pool and add this to the collection of triggers for "Player Found" event
		EventPool eventPool = GameObject.FindObjectOfType<EventPool> ();
		eventPool.AddTriggerToEvent ("Player Found", this);
	}
	
	// Update is called once per frame
	void Update () {
		if (_guards == null || IsTriggered)
			return;

		foreach (BaseGuard guard in _guards) {
			guard.Seek ();
			IsTriggered = guard.DetectUnit(guardTarget.position);
		}
	}

	public bool IsTriggered {
		get;
		set;
	}
}
