using UnityEngine;
using System.Collections;

public interface ICheckpoint {
	bool Reached(IDetect guard);
	Vector3 Position{ get; set; }
	ICheckpoint AdjacentCheckpoint { get; set; }
}
