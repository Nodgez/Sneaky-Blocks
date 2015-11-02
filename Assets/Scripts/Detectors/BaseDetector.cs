using UnityEngine;
using System.Collections;
/// <summary>
/// Absract class for Detector type objects, doubling as triggers
/// </summary>
public abstract class BaseDetector : MonoBehaviour
{
	public Transform[] targetTransforms;
	public float detectionDistance;

	public abstract bool DetectTargets();
	public abstract bool DetectTargets(out Transform targetTransform);

    public virtual void RunTests()
    {
        Debug.Assert(targetTransforms.Length > 0, "No Targets found on " + this.ToString());
    }
}

