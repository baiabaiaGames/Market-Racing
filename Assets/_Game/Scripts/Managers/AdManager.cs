using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdManager : MonoBehaviour {

	[SerializeField] private AdID appID;
	[SerializeField] private AdID bannerViewID;
	[SerializeField] private AdID interstitialID;
	[SerializeField] private AdID rewardedVideoID;

	private BannerView bannerView;
	private InterstitialAd interstitial;
	private RewardBasedVideoAd rewardBasedVideo;

	#region Singleton
	private static AdManager instance;
	public static AdManager _instance;

	private void Awake () {
		if (_instance != null && _instance != this) {
			Destroy (this.gameObject);
			return;
		}

		_instance = this;
		DontDestroyOnLoad (this.gameObject);
	}
	#endregion

	public void Start () {

#if UNITY_ANDROID
		string appId = appID.value;
#else
		string appId = "unexpected_platform";
#endif

		// Initialize the Google Mobile Ads SDK.
		MobileAds.Initialize (appId);

		RequestBanner ();
		RequestInterstitial ();
	}

	private void Update () {
		ShowInterstitial ();
	}

	// Returns an ad request with custom ad targeting.
	private AdRequest CreateAdRequest () {
		return new AdRequest.Builder ()
			.Build ();
	}

	public void DestroyBanner () {
		// Clean up banner ad before creating a new one.
		if (this.bannerView != null) {
			this.bannerView.Destroy ();
		}
	}

	public void RequestBanner () {
		// These ad units are configured to always serve test ads.
#if UNITY_EDITOR
		string adUnitId = "unused";
#elif UNITY_ANDROID
		string adUnitId = bannerViewID.value;
#else
		string adUnitId = "unexpected_platform";
#endif

		// Clean up banner ad before creating a new one.
		if (this.bannerView != null) {
			this.bannerView.Destroy ();
		}

		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView (adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

		// Register for ad events.
		this.bannerView.OnAdLoaded += this.HandleAdLoaded;
		this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
		this.bannerView.OnAdOpening += this.HandleAdOpened;
		this.bannerView.OnAdClosed += this.HandleAdClosed;
		this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

		// Load a banner ad.
		this.bannerView.LoadAd (this.CreateAdRequest ());
	}

	public void RequestInterstitial () {
		// These ad units are configured to always serve test ads.
#if UNITY_EDITOR
		string adUnitId = "unused";
#elif UNITY_ANDROID
		string adUnitId = interstitialID.value;
#else
		string adUnitId = "unexpected_platform";
#endif

		// Clean up interstitial ad before creating a new one.
		if (this.interstitial != null) {
			this.interstitial.Destroy ();
		}

		// Create an interstitial.
		interstitial = new InterstitialAd (adUnitId);

		// Register for ad events.
		this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
		this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
		this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
		this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
		this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

		// Load an interstitial ad.
		this.interstitial.LoadAd (this.CreateAdRequest ());
	}

	private void RequestRewardBasedVideo () {
#if UNITY_EDITOR
		string adUnitId = "unused";
#elif UNITY_ANDROID
		string adUnitId = rewardedVideoID.value;
#else
		string adUnitId = "unexpected_platform";
#endif

		this.rewardBasedVideo.LoadAd (this.CreateAdRequest (), adUnitId);
	}
	private void ShowRewardBasedVideo () {
		if (this.rewardBasedVideo.IsLoaded ()) {
			this.rewardBasedVideo.Show ();
		} else {
			MonoBehaviour.print ("Reward based video ad is not ready yet");
		}
	}

	public void ShowInterstitial () {
		if (this.interstitial.IsLoaded ()) {
			this.interstitial.Show ();
		} else {
			MonoBehaviour.print ("Interstitial is not ready yet");
		}
	}

	#region Banner callback handlers

	public void HandleAdLoaded (object sender, EventArgs args) {
		MonoBehaviour.print ("HandleAdLoaded event received");
	}

	public void HandleAdFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
		MonoBehaviour.print ("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	public void HandleAdOpened (object sender, EventArgs args) {
		MonoBehaviour.print ("HandleAdOpened event received");
	}

	public void HandleAdClosed (object sender, EventArgs args) {
		MonoBehaviour.print ("HandleAdClosed event received");
	}

	public void HandleAdLeftApplication (object sender, EventArgs args) {
		MonoBehaviour.print ("HandleAdLeftApplication event received");
	}

	#endregion

	#region Interstitial callback handlers

	public void HandleInterstitialLoaded (object sender, EventArgs args) {
		MonoBehaviour.print ("HandleInterstitialLoaded event received");
	}

	public void HandleInterstitialFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
		MonoBehaviour.print (
			"HandleInterstitialFailedToLoad event received with message: " + args.Message);
	}

	public void HandleInterstitialOpened (object sender, EventArgs args) {
		MonoBehaviour.print ("HandleInterstitialOpened event received");
	}

	public void HandleInterstitialClosed (object sender, EventArgs args) {
		MonoBehaviour.print ("HandleInterstitialClosed event received");
	}

	public void HandleInterstitialLeftApplication (object sender, EventArgs args) {
		MonoBehaviour.print ("HandleInterstitialLeftApplication event received");
	}

	#endregion

	#region RewardBasedVideo callback handlers

	public void HandleRewardBasedVideoLoaded (object sender, EventArgs args) {
		MonoBehaviour.print ("HandleRewardBasedVideoLoaded event received");
	}

	public void HandleRewardBasedVideoFailedToLoad (object sender, AdFailedToLoadEventArgs args) {
		MonoBehaviour.print (
			"HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
	}

	public void HandleRewardBasedVideoOpened (object sender, EventArgs args) {
		MonoBehaviour.print ("HandleRewardBasedVideoOpened event received");
	}

	public void HandleRewardBasedVideoStarted (object sender, EventArgs args) {
		MonoBehaviour.print ("HandleRewardBasedVideoStarted event received");
	}

	public void HandleRewardBasedVideoClosed (object sender, EventArgs args) {
		MonoBehaviour.print ("HandleRewardBasedVideoClosed event received");
	}

	public void HandleRewardBasedVideoRewarded (object sender, Reward args) {
		string type = args.Type;
		double amount = args.Amount;
		MonoBehaviour.print (
			"HandleRewardBasedVideoRewarded event received for " + amount.ToString () + " " + type);
	}

	public void HandleRewardBasedVideoLeftApplication (object sender, EventArgs args) {
		MonoBehaviour.print ("HandleRewardBasedVideoLeftApplication event received");
	}

	#endregion

}