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

	public bool WaitingOnInterstitial
	{
		get;
		private set;
	}

	void Start()
	{
		if (s_instance == null)
		{
			AdvertisingConsentManager.Instance.GrantDataPrivacyConsent();
			if (!EasyMobile.RuntimeManager.IsInitialized())
				EasyMobile.RuntimeManager.Init();
			
			s_instance = this;
		}
		else if (this != Instance)
			Destroy(this.gameObject);

		DontDestroyOnLoad(this.gameObject);
	}

	private void OnEnable()
	{
		Advertising.InterstitialAdCompleted += Advertising_InterstitialAdCompleted;
	}

	private void OnDisable()
	{
		Advertising.InterstitialAdCompleted -= Advertising_InterstitialAdCompleted;
	}

	public void TriggerInterstitial()
	{
		StartCoroutine(CO_ShowInterstitial());
	}

	private IEnumerator CO_ShowInterstitial()
	{
		Advertising.LoadInterstitialAd();
		WaitingOnInterstitial = true;
		while (!Advertising.IsInterstitialAdReady())
			yield return null;
		Advertising.ShowInterstitialAd();
	}

	private void Advertising_InterstitialAdCompleted(InterstitialAdNetwork arg1, AdPlacement arg2)
	{
		WaitingOnInterstitial = false;
	}

	public void ShowBanner()
	{
		Advertising.ShowBannerAd(BannerAdPosition.Bottom);
	}
}
