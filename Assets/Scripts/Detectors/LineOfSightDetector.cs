using UnityEngine;
using System.Collections;

public class LineOfSightDetector : BaseDetector {

	public override bool DetectTargets ()
	{
		Transform targetTransform = targetTransforms [0];
		Vector3 directionToPlayer = targetTransform.position - transform.position;
		float distanceFromUnit = Vector3.Distance (targetTransform.position, transform.position);
		directionToPlayer = directionToPlayer.normalized;
		float dotProduct = 0;
		dotProduct = Vector3.Dot(directionToPlayer,transform.right);
		RaycastHit hit;
		Debug.DrawRay (transform.position, transform.right * detectionDistance, Color.red);
		
		if(Physics.Linecast(transform.position,targetTransform.position,out hit))
		{
			Debug.DrawLine(transform.position,targetTransform.position,Color.yellow);
			if(hit.collider.name == targetTransform.name)
			{
				if(distanceFromUnit < detectionDistance && dotProduct > 0.75f)
				{
					return true;
				}
			}
		}
		return false;
	}

	public override bool DetectTargets (out Transform targettransform)
	{
		Transform targetTransform = targetTransforms [0];
		targettransform = targetTransform;
		Vector3 directionToPlayer = targetTransform.position - transform.position;
		float distanceFromUnit = Vector3.Distance (targetTransform.position, transform.position);
		directionToPlayer = directionToPlayer.normalized;
		float dotProduct = 0;
		dotProduct = Vector3.Dot(directionToPlayer,transform.right);
		RaycastHit hit;
		Debug.DrawRay (transform.position, transform.right * detectionDistance, Color.red);
		
		if(Physics.Linecast(transform.position,targetTransform.position,out hit))
		{
			Debug.DrawLine(transform.position,targetTransform.position,Color.yellow);
			if(hit.collider.name == targetTransform.name)
			{
				if(distanceFromUnit < detectionDistance && dotProduct > 0.75f)
				{
					targettransform = hit.transform;
					return true;
				}
			}
		}
		return false;
	}
}
