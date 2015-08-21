using UnityEngine;
using System.Collections;

public class TeleporterCheckpoint : BaseCheckpoint {

	public override bool ApplyCheckpointAction (Transform transform)
	{
		transform.position = AdjacentCheckpoint.Position;
		return true;
	}
}
