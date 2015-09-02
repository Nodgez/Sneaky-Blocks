using UnityEngine;
using System.Collections;
/*
 * Upon detection of the player's presence,
 * Clear this level and load another one.
 */
[RequireComponent(typeof(ProximityDetector))]
public class Exit : MonoBehaviour {
	private ProximityDetector _detector;
	public int levelToLoad;

	void Start()
	{
		_detector = GetComponent<ProximityDetector> ();
	}

	void Update()
	{
		if (_detector.DetectTargets ())
			Application.LoadLevel (levelToLoad);

	}
}
