using UnityEngine;
using System.Collections;

public class LineOfSightDetector : BaseDetector {

	private RaycastHit hit;

	public float angle;

	public override void Start ()
	{
		FieldOfView fov = GetComponentInChildren<FieldOfView> ();
		fov.radius = detectionDistance;
		fov.viewAngle = Mathf.RoundToInt(angle);

		//convert the angle to radians
		angle = Mathf.Cos (angle * Mathf.Deg2Rad);

		base.Start ();
	}

	public override bool DetectTargets ()
	{
		Transform targetTransform = targetTransforms [0];
		Vector3 directionToPlayer = targetTransform.position - transform.position;
		float distanceFromUnit = Vector3.Distance (targetTransform.position, transform.position);
		directionToPlayer = directionToPlayer.normalized;
		float dotProduct = 0;
		dotProduct = Vector3.Dot(directionToPlayer,transform.right);
		Debug.DrawRay (transform.position, transform.right * detectionDistance, Color.red);
		
		if(Physics.Linecast(transform.position,targetTransform.position,out hit))
		{
			Debug.DrawLine(transform.position,targetTransform.position,Color.yellow);
			if(hit.collider.name == targetTransform.name)
			{
				if(distanceFromUnit < detectionDistance && dotProduct > angle)
				{
					trigger_.IsTriggered = true;
					return true;
				}
			}
		}
		return false;
	}

	public override bool DetectTargets (out Transform targettransform)
	{
		targettransform = null;
		if(DetectTargets ())
		{
			targettransform = hit.transform;
			trigger_.IsTriggered = true;
			return true;
		}
		return false;
	}
}
