using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
/*
* Upon detection of the player's presence,
* Clear this level and load another one.
*/
[RequireComponent(typeof(ProximityDetector))]
public class Exit : MonoBehaviour, ICameraMoveTo {
	private ProximityDetector _detector;
    float t = 0;

	public int CameraPriority { get; set; }

    void Start()
	{
		CameraPriority = 1;
		_detector = GetComponent<ProximityDetector> ();
	}

	void Update()
	{
        if (!_detector.DetectTargets())
            return;

        int levelToLoad = SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCount ? SceneManager.GetActiveScene().buildIndex + 1 : 0;
        BackingTrackManager.Instance.EffectsLibrary.PlayClip(AudioEffects.Win);
        Time.timeScale = 0;
        t += Time.unscaledDeltaTime;
        if (t >= 2.2f)
        {
            SceneManager.LoadScene(levelToLoad);
            Time.timeScale = 1;
        }
	}
}
