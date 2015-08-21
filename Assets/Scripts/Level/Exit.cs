using UnityEngine;
using System.Collections;
/*
 * Upon detection of the player's presence,
 * Clear this level and load another one.
 */
public class Exit : MonoBehaviour, IDetect {

	public int nextLevelIndex;
	public float detectionRadius;

	public bool DetectUnit(Vector3 position)
	{
		if (Vector3.Distance (this.transform.position, position) < DetectionRadius) {
			Application.LoadLevel(nextLevelIndex);
			Debug.Log("Detected");
		}

		return false;
	}

	public string TargetName {
		get;
		set;
	}

	public float DetectionRadius {
		get{ return detectionRadius;}
		set{ detectionRadius = value;}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawCube (transform.position, Vector3.one);
	}
}
