using UnityEngine;
using System.Collections;
/*
 * Upon detection of the player's presence,
 * Clear this level and load another one.
 */
[RequireComponent(typeof(ProximityDetector))]
public class Exit : MonoBehaviour {
	private ProximityDetector _detector;

	void Start()
	{
		_detector = GetComponent<ProximityDetector> ();
	}

	void Update()
	{
        if (_detector.DetectTargets())
        {
            int levelToLoad = Application.loadedLevel + 1 < Application.levelCount ? Application.loadedLevel + 1 : 0;
            BackingTrackManager.Instance.EffectsLibrary.PlayClip(AudioEffects.Win);
            Application.LoadLevel(levelToLoad);
        }

	}
}
