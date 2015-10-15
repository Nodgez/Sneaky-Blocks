using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointPath : MonoBehaviour {

	private List<BaseCheckpoint> checkpoints = new List<BaseCheckpoint>();
	public CheckpointGuard guardOnPath;

	void Start()
	{
		for (int i = 1; i < transform.childCount; i++)
			checkpoints.Add (transform.GetChild(i).GetComponent<BaseCheckpoint>());

		ApplyNeightbours ();

		if (!guardOnPath)
			return;
		guardOnPath.transform.position = checkpoints [0].Position;
		guardOnPath.TargetCheckpoint = checkpoints[0];
	}

	public void ApplyNeightbours()
	{
		for (int i = 0; i < checkpoints.Count; i++) {
			int nextIndex = i < checkpoints.Count - 1 ? i + 1 : 0;
			BaseCheckpoint cp = checkpoints[i];
			if(cp.GetType() == typeof(EndSearchCheckpoint))
				cp.AdjacentCheckpoint = cp;
			else
				cp.AdjacentCheckpoint = checkpoints[nextIndex];
		}
	}

	public void AddCheckpointToPath(BaseCheckpoint newCheckpoint)
	{
		checkpoints.Add (newCheckpoint);
	}
}
