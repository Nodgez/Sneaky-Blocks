using UnityEngine;
using System.Collections;
/// <summary>
/// Timed checkpoint.
/// </summary>
public class TimedCheckpoint : BaseCheckpoint {

	public float waitTime  = 1;
	private float waitTimer = 0;

	public override bool ApplyCheckpointAction (Transform transform)
	{
		if (waitTime < waitTimer) {
			transform.position = this.Position;
			return false;
		}

		return true;
	}
}
