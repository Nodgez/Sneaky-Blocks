using UnityEngine;
using System.Collections;
/*
 * Upon detection of the player's presence,
 * Clear this level and load another one.
 */
[RequireComponent(typeof(ProximityDetector))]
public class Exit : MonoBehaviour {
	private ProximityDetector _detector;
    float t = 0;

    void Start()
	{
		_detector = GetComponent<ProximityDetector> ();
	}

	void Update()
	{
        if (!_detector.DetectTargets())
            return;

        int levelToLoad = Application.loadedLevel + 1 < Application.levelCount ? Application.loadedLevel + 1 : 0;
        BackingTrackManager.Instance.EffectsLibrary.PlayClip(AudioEffects.Win);
        Time.timeScale = 0;
        t += Time.unscaledDeltaTime;
        if (t >= 2.2f)
        {
            Application.LoadLevel(levelToLoad);
            Time.timeScale = 1;
        }
	}
}
