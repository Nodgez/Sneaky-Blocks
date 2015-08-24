using UnityEngine;
using System.Collections;
/*
 * Upon detection of the player's presence,
 * Clear this level and load another one.
 */
public class Exit : ProximityDetector {

	public override bool Detect (Vector3 position)
	{
		if (base.Detect (position))
			Application.LoadLevel (0);

		return false;
	}
	public string TargetName {
		get;
		set;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawCube (transform.position, Vector3.one);
	}
}
