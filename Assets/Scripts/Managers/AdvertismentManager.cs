using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;
using EasyMobile;

public class AdvertismentManager : MonoBehaviour {

	private static AdvertismentManager s_instance;
	public static AdvertismentManager Instance
	{
		get { return s_instance; }
	}

	//   public static AdvertismentManager Instance;
	//   const string AD_COUNT_KEY = "CountUntilAd";

	void Start()
	{
		if (s_instance == null)
		{
			if (!EasyMobile.RuntimeManager.IsInitialized())
				EasyMobile.RuntimeManager.Init();

			AdvertisingConsentManager.Instance.GrantDataPrivacyConsent();
			var admobClient = Advertising.AdMobClient;
			s_instance = this;
		}
		else if (this != Instance)
			Destroy(this.gameObject);

		DontDestroyOnLoad(this.gameObject);
	}

	public void ShowInterstitial()
	{
		StartCoroutine(CO_ShowInterstitial());
	}

	private IEnumerator CO_ShowInterstitial()
	{
		Advertising.LoadInterstitialAd();
		print("loading interstitial....");
		while (!Advertising.IsInterstitialAdReady())
			yield return null;
		print("showing interstitial....");
		Advertising.ShowInterstitialAd();
	}

	//   void Update () {
	//       int count =  PlayerPrefs.GetInt(AD_COUNT_KEY);

	//       if (Advertisement.IsReady() && !Advertisement.isShowing && count <= 0)
	//       {
	//           Advertisement.Show(null, new ShowOptions { resultCallback = delegate {
	//               Time.timeScale = 1;
	//           } });
	//           PlayerPrefs.SetInt(AD_COUNT_KEY, 20);
	//           PlayerPrefs.Save();
	//       }

	//       if (Advertisement.isShowing)
	//       {
	//           Time.timeScale = 0;
	//       }
	//}

	//   void OnLevelWasLoaded()
	//   {
	//       PlayerPrefs.SetInt(AD_COUNT_KEY, PlayerPrefs.GetInt(AD_COUNT_KEY) - 1);
	//   }
}
