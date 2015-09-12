using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ProximityDetector))]
public class DefensiveWall : MonoBehaviour, ITrigger {

	public ProximityDetector guardDetector;
	public ProximityDetector playerDetector;

	public bool IsTriggered { get; set; }

	private Renderer _renderer;
	private float _colorLerp;
	private Color _initialColor;

	void Start()
	{
		_renderer = GetComponent<Renderer> ();
		_initialColor = _renderer.material.GetColor("_TintColor");
		EventPool eventPool = GameObject.FindObjectOfType<EventPool> ();
		eventPool.AddTriggerToEvent ("Player Found", playerDetector.Trigger);
	}
	
	void Update () {
		//if there is a guard near by, update towards off values
		//else if not, update towards on values
		if (guardDetector.DetectTargets ()) {
			playerDetector.detectionDistance = 0;
			_colorLerp += Time.deltaTime;
			_colorLerp = Mathf.Clamp01 (_colorLerp);
			_renderer.material.SetColor("_TintColor", 
			                            Color.Lerp (_initialColor, new Color (0, 0, 0, 0), _colorLerp));

		} else if(_colorLerp > 0) {
			_colorLerp -= Time.deltaTime;
			_colorLerp = Mathf.Clamp01 (_colorLerp);
			_renderer.material.SetColor("_TintColor", 
			                            Color.Lerp (_initialColor, new Color (0, 0, 0, 0), _colorLerp));
			if(_colorLerp < 0.5f)
				playerDetector.detectionDistance = 1;
		}

		playerDetector.DetectTargets ();
	}
}
