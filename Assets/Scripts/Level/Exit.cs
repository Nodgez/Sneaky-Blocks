using UnityEngine;
using UnityEngine.SceneManagement;
/*
* Upon detection of the player's presence,
* Clear this level and load another one.
*/
[RequireComponent(typeof(ProximityDetector))]
public class Exit : MonoBehaviour, ICameraMoveTo {
	private ProximityDetector _detector;
	AsyncOperation _nextLevelLoadOp;
	public int CameraPriority { get; set; }

    void Start()
	{
		CameraPriority = 1;
		_detector = GetComponent<ProximityDetector> ();
		int levelToLoad = SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings ? SceneManager.GetActiveScene().buildIndex + 1 : 0;
		_nextLevelLoadOp = SceneManager.LoadSceneAsync(levelToLoad);
		_nextLevelLoadOp.allowSceneActivation = false;
	}

	void Update()
	{
		if (!_detector.DetectTargets())
			return;
		BackingTrackManager.Instance.EffectsLibrary.PlayClip(AudioEffects.Win);
		_nextLevelLoadOp.allowSceneActivation = true;
		enabled = false;
		AdvertismentManager.Instance.ShowInterstitial();
	}
}
