using UnityEngine;
using System.Collections;
/// <summary>
/// Absract class for Detector type objects, doubling as triggers
/// </summary>
public abstract class BaseDetector : MonoBehaviour, ITrigger
{
	public Transform[] targetTransforms;
	public float detectionDistance;
	public bool IsTriggered{ get; set; }

	public abstract bool DetectTargets();
	public abstract bool DetectTargets(out Transform targetTransform);

}

