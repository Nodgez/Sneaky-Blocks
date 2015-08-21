using UnityEngine;
using System.Collections;

public class NormalCheckpoint : BaseCheckpoint {

	public override bool ApplyCheckpointAction (Transform transform)
	{
		transform.position = this.Position;
		return true;
	}
}
