using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeManagement : MonoBehaviour {

    public Text timeText;
    public Text levelText;

    private string _timeInLevel;

	void Start () {
        if (levelText != null)
            levelText.text = Application.loadedLevel.ToString();

        _timeInLevel = "Level" + Application.loadedLevel.ToString() + "Time";
	}
	
	void Update () {
        if (timeText != null)
            timeText.text = Time.timeSinceLevelLoad.ToString("0.0");
	}

    void OnDestroy()
    {
        float bestTime = PlayerPrefs.GetFloat(_timeInLevel, 0);
        if (bestTime < Time.timeSinceLevelLoad && bestTime != 0)
            return;
        PlayerPrefs.SetFloat(_timeInLevel, Time.timeSinceLevelLoad);
        PlayerPrefs.Save();
    }
}
