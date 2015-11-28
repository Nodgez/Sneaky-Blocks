using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class AdvertismentManager : MonoBehaviour {

    public static AdvertismentManager Instance;
    const string AD_COUNT_KEY = "CountUntilAd";

    void Start () {

        if (Instance == null)
        {
            Instance = this;
            if(Advertisement.isSupported)
                Advertisement.Initialize("85995");

            if (!PlayerPrefs.HasKey(AD_COUNT_KEY))
                PlayerPrefs.SetInt(AD_COUNT_KEY, 0);           
        }
        else if (this != Instance)
            Destroy(this.gameObject);

        PlayerPrefs.SetInt(AD_COUNT_KEY, 0);

        DontDestroyOnLoad(this.gameObject);
    }

    void Update () {
        int count =  PlayerPrefs.GetInt(AD_COUNT_KEY);

        if (Advertisement.IsReady() && !Advertisement.isShowing && count <= 0)
        {
            Advertisement.Show(null, new ShowOptions { resultCallback = delegate {
                Time.timeScale = 1;
            } });
            PlayerPrefs.SetInt(AD_COUNT_KEY, 20);
            PlayerPrefs.Save();
        }

        if (Advertisement.isShowing)
        {
            Time.timeScale = 0;
        }
	}

    void OnLevelWasLoaded()
    {
        PlayerPrefs.SetInt(AD_COUNT_KEY, PlayerPrefs.GetInt(AD_COUNT_KEY) - 1);
    }
}
