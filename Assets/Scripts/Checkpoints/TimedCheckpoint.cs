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
		if (waitTimer < waitTime) {
			waitTimer += Time.deltaTime;
			transform.position = this.Position;
			return false;
		}

		waitTimer = 0;
		return true;
	}
}
