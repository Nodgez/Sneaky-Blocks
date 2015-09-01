using UnityEngine;
using System.Collections;

public class ProximityDetector : BaseDetector {
	
	public override bool DetectTargets ()
	{
		if (targetTransforms == null)
			return false;

		foreach (Transform t in targetTransforms) {
			if (Vector3.Distance (t.position, this.transform.position) <= detectionDistance)
			{
				IsTriggered = true;
				return true;
			}
		}
		return 
			false;
	}

	void OnDrawGizmos()
	{
		for (int i = 0; i < 360; i++) {


			Vector3 pt1 = new Vector3(transform.position.x + detectionDistance * Mathf.Cos(i * Mathf.Deg2Rad),
			                          transform.position.y,
			                          transform.position.z + detectionDistance * Mathf.Sin(i * Mathf.Deg2Rad));
			int i2 = i < 360 ? i + 1 : 0;

			Vector3 pt2 = new Vector3(transform.position.x + detectionDistance * Mathf.Cos(i2 * Mathf.Deg2Rad),
			                          transform.position.y,
			                          transform.position.z + detectionDistance * Mathf.Sin(i2 * Mathf.Deg2Rad));
			Gizmos.color = Color.red;
			Gizmos.DrawLine (pt1,pt2);
		}

	}

	public override bool DetectTargets (out Transform targetTransform)
	{
		targetTransform = null;
		if (targetTransforms == null)
			return false;
		
		foreach (Transform t in targetTransforms) {
			if (Vector3.Distance (t.position, this.transform.position) <= detectionDistance)
			{
				IsTriggered = true;
				targetTransform = t;
				return true;
			}
		}
		return 
			false;
	}
}
