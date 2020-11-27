using UnityEngine;
using System.Collections;

public class DestinationCamera : MonoBehaviour {

	public FindPathBehavior findPathBehavior;
	public float speed;

	private Vector3 _destination;
	private float _lerpValue = 1;
	private Vector3 _start;
    private Vector3 _direction;

	void Start () {
	
	}
	
	void Update () {
	
		Vector3 frameDestination = findPathBehavior.Destination + (Vector3.up * 10);

        //If the destination has bee changed reset
		if (frameDestination != _destination) {
			_lerpValue = 0;
			_start = transform.position;
			_destination = frameDestination;
            _direction = _destination - _start;
            _direction = new Vector3(_direction.x, 0, _direction.z);
		}

        if (Vector3.Distance(_destination, transform.position) < .5f)
            return;

        transform.position += _direction * speed * Time.deltaTime;
	}
}
