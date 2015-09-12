using UnityEngine;
using System.Collections;
/// <summary>
/// Absract class for Detector type objects, doubling as triggers
/// </summary>
[RequireComponent(typeof(Trigger))]
public abstract class BaseDetector : MonoBehaviour
{
	public Transform[] targetTransforms;
	public float detectionDistance;

	protected Trigger trigger_;

	public Trigger Trigger
	{
		get{ return trigger_; }
		private set{ trigger_ = value;}
	}

	public abstract bool DetectTargets();
	public abstract bool DetectTargets(out Transform targetTransform);

	public virtual void Start()
	{
		trigger_ = GetComponent<Trigger> ();
		if (trigger_ == null)
			return;
		//Find the event pool and add this to the collection of triggers for "Player Found" event
		EventPool eventPool = GameObject.FindObjectOfType<EventPool> ();
		eventPool.AddTriggerToEvent ("Player Found", trigger_);
	}

}

