using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuardManager : MonoBehaviour, ITrigger {

	private List<BaseGuard> _guards = new List<BaseGuard>();
	public Transform guardTarget;

	void Start () {
		GameObject[] guardObjects = GameObject.FindGameObjectsWithTag ("Guard");
		foreach (GameObject go in guardObjects) {
			BaseGuard guard = go.GetComponent<BaseGuard>();
			guard.targetName = guardTarget.name;
			_guards.Add(guard);
		}

		if (guardTarget == null) {
			guardTarget = this.transform;
			Debug.Log("No Target Available");
		}

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
