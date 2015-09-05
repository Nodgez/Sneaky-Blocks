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
		_direction = _targetCheckpoint.Position - this.transform.position;

		if (!_targetCheckpoint.Reached (this.transform.position)) {
			_slerpValue += Time.deltaTime * movementSpeed;
			_slerpValue = Mathf.Clamp01(_slerpValue);
			this.transform.position += _direction.normalized * Time.deltaTime * rotationSpeed; //change to speed
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

	//Detects Unity using Line casting and view angle
	public override bool DetectUnit(Vector3 position)
	{
		Vector3 directionToPlayer = position - transform.position;
		float distanceFromUnit = Vector3.Distance (position, transform.position);
		directionToPlayer = directionToPlayer.normalized;
		float dotProduct = 0;
		dotProduct = Vector3.Dot(directionToPlayer,transform.right);
		RaycastHit hit;
		Debug.DrawRay (transform.position, transform.right * detectionRadius, Color.red);
		
		if(Physics.Linecast(transform.position,position,out hit))
		{
			Debug.DrawLine(transform.position,position,Color.yellow);
			if(hit.collider.name == targetName)
			{
				float angleToDot = Mathf.Cos (viewAngle * Mathf.Deg2Rad);
				if(distanceFromUnit < detectionRadius && dotProduct > angleToDot)
				{
					return true;
				}
			}
		}
		return false;
	}

	public BaseCheckpoint TargetCheckpoint
	{
		get { return _targetCheckpoint;}
		set { _targetCheckpoint = value;}
	}
}
