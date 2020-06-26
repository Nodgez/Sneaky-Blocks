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

	[SerializeField]
	private int _cameraPriority = 0;
	public int CameraPriority
	{
		get
		{
			return _cameraPriority;
		}
	}

	public T ConvertToComponent<T>() where T : MonoBehaviour
	{
		var component = this.GetComponent<T>();
		return component;
	}

	void Start()
	{
		_detector = GetComponent<ProximityDetector> ();
		int levelToLoad = SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings ? SceneManager.GetActiveScene().buildIndex + 1 : 0;
		_nextLevelLoadOp = SceneManager.LoadSceneAsync(levelToLoad);
		_nextLevelLoadOp.allowSceneActivation = false;
	}

	void Update()
	{
		if (!_detector.DetectTargets())
			return;
			
		enabled = false;
		if (AdvertismentManager.Instance != null)
		{
			AdvertismentManager.Instance.TriggerInterstitial((adnetwork, placement) =>
			{
				_nextLevelLoadOp.allowSceneActivation = true;
			});
		}
		else
			_nextLevelLoadOp.allowSceneActivation = true;
	}
}
