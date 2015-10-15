using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointGuard : BaseGuard {

	public float rotationSpeed = 2.5f;
	public float movementSpeed = 2.5f;

	private BaseCheckpoint _targetCheckpoint;
	private int _currentCheckpointIndex;
	private bool _loopingBack;
	private Vector3 _direction;
	private Quaternion _rotationAtLastCheckpoint;
	private float _slerpValue = 0;

	void Start()
	{
		_rotationAtLastCheckpoint = transform.rotation;
	}

	//Seeks around the map via following checkpoints
	public override void Seek()
	{			
		if (_targetCheckpoint == null)
			return;
		trigger_.IsTriggered = losDetector_.DetectTargets ();
		_direction = _targetCheckpoint.Position - this.transform.position;

		if (!_targetCheckpoint.Reached (this.transform.position)) {
			_slerpValue += Time.deltaTime * rotationSpeed;
			_slerpValue = Mathf.Clamp01(_slerpValue);
			this.transform.position += _direction.normalized * Time.deltaTime * movementSpeed; //change to speed
			float angle = Mathf.Atan2(-_direction.z,_direction.x) * Mathf.Rad2Deg;
			Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.up);
			transform.rotation = Quaternion.Slerp(_rotationAtLastCheckpoint, rotation,_slerpValue);
			return;
		}

		_slerpValue = 0;
		//if action at checpoint isn't complete then leave
		if (!_targetCheckpoint.ApplyCheckpointAction (this.transform))
			return;

		//Store new values fro moving to next checkpoint
		_rotationAtLastCheckpoint = transform.rotation;
		_targetCheckpoint = _targetCheckpoint.AdjacentCheckpoint;
		_direction = _targetCheckpoint.Position - this.transform.position;
	}

	public BaseCheckpoint TargetCheckpoint
	{
		get { return _targetCheckpoint;}
		set { _targetCheckpoint = value;}
	}
}
