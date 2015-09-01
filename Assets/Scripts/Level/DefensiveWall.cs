using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ProximityDetector))]
public class DefensiveWall : MonoBehaviour, ITrigger {

	public ProximityDetector guardDetector;
	public ProximityDetector playerDetector;

	public bool IsTriggered { get; set; }

	private Renderer _renderer;
	private Animator _animator;
	
	void Start()
	{
		_renderer = GetComponent<Renderer> ();
		_animator = GetComponent<Animator> ();
		EventPool eventPool = GameObject.FindObjectOfType<EventPool> ();
		eventPool.AddTriggerToEvent ("Player Found", playerDetector);
	}
	
	void Update () {
		Transform detectedTransform;
		if (guardDetector.DetectTargets ()) {
			_animator.SetBool ("Fading", true);
		} else if (_animator.GetBool ("Fading")) {
			_animator.SetBool ("Fading", false);
			_animator.SetTrigger("FadeIn");
		}

		if (playerDetector.DetectTargets ()) {

		}
	}
}
