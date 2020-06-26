using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Globalization;
using System.Diagnostics;

public class TimeManagement : MonoBehaviour {

    public Text timeText;
    public Text levelText;

	private LevelIntro _levelIntro;
    private string _timeInLevel;
	private Stopwatch _stopwatch;

	void Start () {
		_stopwatch = new Stopwatch();
        levelText.text = SceneManager.GetActiveScene().buildIndex.ToString();

        _timeInLevel = "Level" + SceneManager.GetActiveScene().buildIndex + "Time";
		_levelIntro = FindObjectOfType<LevelIntro>();
		_levelIntro.OnIntroCycleComplete += StartTimer;
	}

	private void Update()
	{
		timeText.text = _stopwatch.Elapsed.ToString(@"s\.ff");
	}

	private void StartTimer()
	{
		_stopwatch.Restart();
	}

	void OnDestroy()
    {
		_stopwatch.Stop();
        float bestTime = PlayerPrefs.GetFloat(_timeInLevel, 0);
        if (bestTime < _stopwatch.Elapsed.TotalMilliseconds && bestTime != 0)
            return;
        PlayerPrefs.SetFloat(_timeInLevel, (float)_stopwatch.Elapsed.TotalMilliseconds);
        PlayerPrefs.Save();
    }
}
