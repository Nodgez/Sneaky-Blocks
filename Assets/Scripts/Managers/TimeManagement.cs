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
        PlayerPrefs.SetFloat(_timeInLevel, Time.timeSinceLevelLoad);
        PlayerPrefs.Save();
    }
}
