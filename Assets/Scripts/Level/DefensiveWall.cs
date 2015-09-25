using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Trigger))]
public class DefensiveWall : MonoBehaviour {

	public ProximityDetector guardDetector;
	public ProximityDetector playerDetector;

	public bool IsTriggered { get; set; }

	private Renderer _renderer;
	private float _colorLerp;
	private Color _initialColor;
	private Trigger _trigger;

	void Start()
	{
		_trigger = GetComponent<Trigger> ();
		_renderer = GetComponent<Renderer> ();
		_initialColor = _renderer.material.GetColor("_TintColor");

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		playerDetector.targetTransforms = new Transform[] {players [0].transform};

		BaseGuard[] guards = GameObject.FindObjectsOfType<BaseGuard> ();
		guardDetector.targetTransforms = new Transform[guards.Length];
		for(int i = 0 ; i < guards.Length;i++)
		{
			guardDetector.targetTransforms[i] = guards[i].transform;
		}
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

		_trigger.IsTriggered = playerDetector.DetectTargets ();
	}
}
