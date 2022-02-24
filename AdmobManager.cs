using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobManager : MonoBehaviour
{
    public static AdmobManager instance;
    public int deaths;

    private BannerView bannerView;
    private InterstitialAd interstitial;

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this){
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_ANDROID
            string appId = "ca-app-pub-3218905455641158~5382999677";
        #elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
        #else
            string appId = "unexpected_platform";
        #endif

        MobileAds.Initialize(initStatus => {});

        this.RequestBanner();
        this.RequestInterstitial();
    }

    
    public void RequestBanner()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3218905455641158/6373481865";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    public void DestroyBanner(){
        bannerView.Destroy();
    }


    public void RequestInterstitial()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3218905455641158/5060400191";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded()) {
            this.interstitial.Show();
        }
    }

    void HandleOnAdClosed(object sender, System.EventArgs args){
        this.RequestInterstitial();
    }
}
