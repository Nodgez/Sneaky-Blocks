using UnityEngine;
using System.Collections;
/// <summary>
/// Abstract class for guard type objects
/// </summary>
[RequireComponent(typeof(LineOfSightDetector))]
public abstract class BaseGuard : MonoBehaviour {

	public float detectionRadius = 5f;
	public int viewAngle;
	public string targetName;
	public LayerMask detectionLayers;

	protected LineOfSightDetector losDetector_;


	protected virtual void Awake()
	{
		losDetector_ = GetComponent<LineOfSightDetector> ();
	}

	public abstract void Seek ();
}
