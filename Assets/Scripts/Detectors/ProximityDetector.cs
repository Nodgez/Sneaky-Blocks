using UnityEngine;
using System.Collections;

public class ProximityDetector : MonoBehaviour {

	public float distance;


	public virtual bool Detect(Vector3 position)
	{
		if (Vector3.Distance (position, this.transform.position) <= distance)
			return true;

		return false;
	}
}
