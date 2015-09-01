using UnityEngine;
using System.Collections;

public abstract class BaseDetector : MonoBehaviour, ITrigger
{
	public Transform[] targetTransforms;
	public float detectionDistance;
	public bool IsTriggered{ get; set; }

	public abstract bool DetectTargets();
	public abstract bool DetectTargets(out Transform targetTransform);

}

