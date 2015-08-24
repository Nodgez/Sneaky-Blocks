using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointGuard : BaseGuard {

	public float rotationSpeed = 2.5f;
	public float movementSpeed = 2.5f;

	private BaseCheckpoint targetCheckpoint;
	private int currentCheckpointIndex;
	private bool loopingBack;
	private Vector3 direction;
	private Quaternion rotationAtLastCheckpoint;
	private float slerpValue = 0;

	void Start()
	{
		rotationAtLastCheckpoint = transform.rotation;
	}

	public override void Seek()
	{
		direction = targetCheckpoint.Position - this.transform.position;

		if (!targetCheckpoint.Reached (this.transform.position)) {
			slerpValue += Time.deltaTime * movementSpeed;
			slerpValue = Mathf.Clamp01(slerpValue);
			this.transform.position += direction.normalized * Time.deltaTime * rotationSpeed; //change to speed
			float angle = Mathf.Atan2(-direction.z,direction.x) * Mathf.Rad2Deg;
			Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.up);
			transform.rotation = Quaternion.Slerp(rotationAtLastCheckpoint, rotation,slerpValue);
			return;
		}

		slerpValue = 0;
		if (!targetCheckpoint.ApplyCheckpointAction (this.transform))
			return;

		rotationAtLastCheckpoint = transform.rotation;
		targetCheckpoint = targetCheckpoint.AdjacentCheckpoint;
		direction = targetCheckpoint.Position - this.transform.position;
	}

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
				if(distanceFromUnit < detectionRadius && dotProduct > 0.75f)
					return true;
			}
		}
		return false;
	}

	public BaseCheckpoint TargetCheckpoint
	{
		get { return targetCheckpoint;}
		set { targetCheckpoint = value;}
	}
}
