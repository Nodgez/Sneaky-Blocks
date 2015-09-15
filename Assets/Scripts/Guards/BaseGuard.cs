using UnityEngine;
using System.Collections;
/// <summary>
/// Abstract class for guard type objects
/// </summary>
[RequireComponent(typeof(LineOfSightDetector))]
[RequireComponent(typeof(Trigger))]
public abstract class BaseGuard : MonoBehaviour {

	public float detectionRadius = 5f;
	public int viewAngle;
	public string targetName;
	public LayerMask detectionLayers;

	protected LineOfSightDetector losDetector_;
	protected Trigger trigger_;


	protected virtual void Awake()
	{
		losDetector_ = GetComponent<LineOfSightDetector> ();
		trigger_ = GetComponent<Trigger> ();
	}

	public abstract void Seek ();
}
