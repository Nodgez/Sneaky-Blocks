using UnityEngine;
using System.Collections;

public interface IDetect {
	bool DetectUnit(Vector3 position);
	float DetectionRadius{ get; set; }
	string TargetName{ get; set; }
}
