using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ProximityDetector))]
[RequireComponent(typeof(Trigger))]
public class SwitchLever : MonoBehaviour {

	private ProximityDetector _proxDetector;
	private Trigger _switchTrigger;

	void Start () {
		_proxDetector = GetComponent<ProximityDetector> ();
		_switchTrigger = GetComponent<Trigger> ();
	}
	
	// Update is called once per frame
	void Update () {
		_switchTrigger.IsTriggered = _proxDetector.DetectTargets ();
	}
}
