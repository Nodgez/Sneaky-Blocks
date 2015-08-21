using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuardManager : MonoBehaviour {

	private List<BaseGuard> guards = new List<BaseGuard>();
	public Transform guardTarget;

	void Start () {
		GameObject[] guardObjects = GameObject.FindGameObjectsWithTag ("Guard");
		foreach (GameObject go in guardObjects) {
			BaseGuard guard = go.GetComponent<BaseGuard>();
			guard.TargetName = guardTarget.name;
			guards.Add(guard);
		}

		if (guardTarget == null) {
			guardTarget = this.transform;
			Debug.Log("No Target Available");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (guards == null)
			return;

		foreach (BaseGuard guard in guards) {
			guard.Seek ();

			if(guard.DetectUnit(guardTarget.position))
			{
				Debug.Log("Player Found");
				Time.timeScale = 0;
				//zoom camera
			}
		}
	}

	public List<BaseGuard> Guards {
		get{ return guards;}
		set{ guards = value;}
	}
}
