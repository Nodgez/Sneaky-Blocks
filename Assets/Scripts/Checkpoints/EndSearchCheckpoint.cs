using UnityEngine;
using System.Collections;

public class EndSearchCheckpoint : BaseCheckpoint {

	public override bool Reached (Vector3 position)
	{
		position = Position;
		return base.Reached (position);
	}
}
