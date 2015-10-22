using UnityEngine;
using System.Collections;

public class DestinationCamera : MonoBehaviour {

	public FindPathBehavior findPathBehavior;
	public float lerpFactor;

	private Vector3 _destination;
	private float _lerpValue = 1;
	private Vector3 _start;

	void Start () {
	
	}
	
	void Update () {
	
		Vector3 frameDestination = findPathBehavior.Destination + (Vector3.up * 10);

		if (frameDestination != _destination) {
			_lerpValue = 0;
			_start = transform.position;
			_destination = frameDestination;
		}

		if (_lerpValue >= 1)
			return;

		_lerpValue += Time.deltaTime * lerpFactor;
		_lerpValue = Mathf.Clamp01 (_lerpValue);
		transform.position = Vector3.Lerp (_start, _destination, _lerpValue);
	}
}
