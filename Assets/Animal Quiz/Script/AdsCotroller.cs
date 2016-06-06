using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
public class AdsCotroller : MonoBehaviour {
	public string AndroidBannerID = "INSERT_ANDROID_BANNER_AD_UNIT_ID_HERE";
	public string AndroidInterstitialID = "INSERT_ANDROID_INTERSTITIAL_AD_UNIT_ID_HERE";
	public string IOSBannerID = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
	public string IOSInterstitialID = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";

	public int b4showFullAds = 3;

	private static AdsCotroller instance;
	private int counter = 0;
	private BannerView bannerView;
	private InterstitialAd interstitial;
	private AdRequest request;
	private bool isShowing = false;

	void Awake()
	{
		if(AdsCotroller.instance != null)
		{
			Destroy(gameObject);	//just allow one adscontroller on scene over gameplay, even when you restart this level
		}
	}
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		instance = this;
		RequestBanner ();
		RequestInterstitial ();
	}

	private void RequestBanner()
	{
		#if UNITY_ANDROID
		string adUnitId = AndroidBannerID;
		#elif UNITY_IPHONE
		string adUnitId = IOSBannerID;
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
		//
		bannerView.OnAdLoaded+=HandleEventHandler;
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);
	}

	void HandleEventHandler (object sender, System.EventArgs e)
	{
		bannerView.Hide ();
	}

	private void RequestInterstitial()
	{
		#if UNITY_ANDROID
		string adUnitId = AndroidInterstitialID;
		#elif UNITY_IPHONE
		string adUnitId = IOSInterstitialID;
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd(adUnitId);
		interstitial.OnAdClosed+=HandleEventHandler1;
		// Create an empty ad request.
		request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		interstitial.LoadAd(request);
	}

	void HandleEventHandler1 (object sender, System.EventArgs e)
	{
		interstitial.Destroy ();
		interstitial.LoadAd (request);
	}

	//Global call
	public static void ShowAds(){
		if (instance != null && !instance.isShowing) {
			instance.isShowing = true;
			instance.ShowBanner ();
			instance.ShowFullAds ();
		}
	}

		public static void HideAds(){
		if (instance != null) {
			instance.isShowing = false;
			instance.HideBanner ();
		}
	}

		public static void DestroyAds(){
		if (instance != null) {
			instance.Destroy ();
		}
	}

	private void ShowFullAds(){
		counter++;
		if (counter >= b4showFullAds) {
			counter = 0;
			
			if (interstitial.IsLoaded ()) {
				interstitial.Show ();
			}

		}
	}

		private void ShowBanner(){
		bannerView.Show ();
	}
		private void HideBanner(){
		bannerView.Hide ();
	}
		//destroy all ads when exit game
		private void Destroy(){
		bannerView.Destroy ();
		interstitial.Destroy ();
		}
}
