using UnityEngine;
using System.Collections;

public class InterpolationCamera : MonoBehaviour {

	public FindPathBehavior findPathBehavior;
	public float cameraExtentDistance;

	private Vector3 _destination;
	private Vector3 _start;
	private Vector3 _cameraExtent;
	private Vector3 _cameraOffset;
	private float _lerpValue;
	
	void Start () {
		_start = Vector3.up * 10;
	}
	
	void Update () {

		if (_destination != findPathBehavior.Destination) {
			_destination = findPathBehavior.Destination;
			_lerpValue = 0;
		}

        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z)
                              , new Vector2(_destination.x, _destination.z)) < 0.25f)
        {
            return;
        }

		_lerpValue += Time.deltaTime;
		_lerpValue = Mathf.Clamp01 (_lerpValue);

		Vector3 direction = findPathBehavior.MoveDirection * cameraExtentDistance;
		Debug.Log (direction);
		_cameraExtent = _start + direction;
		_cameraOffset = Vector3.Lerp (_start, _cameraExtent, _lerpValue);

		transform.position = findPathBehavior.transform.position + _cameraOffset;
	}
}
