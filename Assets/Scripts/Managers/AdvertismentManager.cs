using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;
using EasyMobile;
using System;

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
		Advertising.InterstitialAdCompleted += Advertising.LoadInterstitialAd;
	}

	private void OnDisable()
	{
		Advertising.InterstitialAdCompleted -= Advertising.LoadInterstitialAd;
	}

	public void TriggerInterstitial(Action<InterstitialAdNetwork, AdPlacement> onInterstitialComplete)
	{
		if (!Advertising.IsInterstitialAdReady())
			return;

		Advertising.InterstitialAdCompleted += onInterstitialComplete;
		Advertising.ShowInterstitialAd();
	}

	private IEnumerator CO_ShowInterstitial()
	{
		while (!Advertising.IsInterstitialAdReady())
			yield return null;
		Advertising.ShowInterstitialAd();
	}

	public void ShowBanner()
	{
		Advertising.ShowBannerAd(BannerAdPosition.Bottom);
	}
}
