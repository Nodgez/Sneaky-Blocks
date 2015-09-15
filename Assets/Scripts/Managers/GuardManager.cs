using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///Updates all guards and acts as a trigger for events 
/// </summary>
public class GuardManager : MonoBehaviour {

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
	}
	
	// Update is called once per frame
	void Update () {
		if (_guards == null)
			return;

		foreach (BaseGuard guard in _guards) {
			if(guard != null)
				guard.Seek ();
		}
	}
}
