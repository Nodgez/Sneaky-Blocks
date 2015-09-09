using UnityEngine;
using System.Collections;

public class BaseCheckpoint : MonoBehaviour {

	private float triggerDistance = 0.1f;

	public BaseCheckpoint AdjacentCheckpoint {
		get;
		set;
	}
	
	public Vector3 Position
	{
		get { return this.transform.position;}
		set { this.transform.position = value;}
	}
	
	public virtual bool Reached(Vector3 position)
	{
		if (Vector3.Distance (position, this.Position) > triggerDistance) {
			return false;
		}

		return true;
	}

	public virtual bool ApplyCheckpointAction(Transform transform)
	{
		return true;
	}
}
