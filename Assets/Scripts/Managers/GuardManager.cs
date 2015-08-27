using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuardManager : MonoBehaviour, ITrigger {

	private List<BaseGuard> guards = new List<BaseGuard>();
	public Transform guardTarget;

	void Start () {
		GameObject[] guardObjects = GameObject.FindGameObjectsWithTag ("Guard");
		foreach (GameObject go in guardObjects) {
			BaseGuard guard = go.GetComponent<BaseGuard>();
			guard.targetName = guardTarget.name;
			guards.Add(guard);
		}

		if (guardTarget == null) {
			guardTarget = this.transform;
			Debug.Log("No Target Available");
		}

		GameEvent playerFound = new GameEvent ();
		playerFound.AddEventTrigger (this);

		EventPool eventPool = GameObject.FindObjectOfType<EventPool> ();
		eventPool.AddEventToPool ("Player Found",ref playerFound);
	}
	
	// Update is called once per frame
	void Update () {
		if (guards == null || IsTriggered)
			return;

		foreach (BaseGuard guard in guards) {
			guard.Seek ();
			IsTriggered = guard.DetectUnit(guardTarget.position);
		}
	}

	public List<BaseGuard> Guards {
		get{ return guards;}
		set{ guards = value;}
	}

	public bool IsTriggered {
		get;
		set;
	}
}
