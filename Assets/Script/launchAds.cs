using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public class launchAds : MonoBehaviour
{
	[Header("AD google")]
	public UnityEvent OnAdLoadedEvent;

	private void Awake()
	{
		if (!GameObject.Find("NewAds"))
			DontDestroyOnLoad(transform.gameObject);
		else
			Destroy(this.gameObject);

		gameObject.name = "NewAds";
	}

	#region HELPER METHODS

	private AdRequest CreateAdRequest()
	{
		return new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
			.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
			.AddKeyword("unity-admob-sample")
			.TagForChildDirectedTreatment(false)
			.AddExtra("color_bg", "9B30FF")
			.Build();
	}

	#endregion

	private InterstitialAd interstitial;

	public void ShowInterstitialAd()
	{

		string adUnitId = "ca-app-pub-2807814307137078/3167740816";

		// Clean up interstitial before using it
		if (interstitial != null)
		{
			interstitial.Destroy();
		}

		interstitial = new InterstitialAd(adUnitId);

		interstitial.OnAdLoaded += (sender, args) => OnAdLoadedEvent.Invoke();

		interstitial.LoadAd(CreateAdRequest());

	}

	// Start is called before the first frame update
	void Start()
    {
		// Initialize the Google Mobile Ads SDK.
		//MobileAds.Initialize(initStatus => { });
		List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };

		deviceIds.Add("75EF8D155528C04DACBBA6F36F433035");

		RequestConfiguration requestConfiguration =
			new RequestConfiguration.Builder()
			.SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.True)
			.SetTestDeviceIds(deviceIds).build();

		MobileAds.SetRequestConfiguration(requestConfiguration);
		MobileAds.Initialize(HandleInitCompleteAction);
	}

	private void HandleInitCompleteAction(InitializationStatus initstatus)
	{
		// Callbacks from GoogleMobileAds are not guaranteed to be called on
		// main thread.
		// In this example we use MobileAdsEventExecutor to schedule these calls on
		// the next Update() loop.
		MobileAdsEventExecutor.ExecuteInUpdate(() => {
			Debug.Log("AdRun");
		});
	}

	public bool showAd = false;

	public void ShowMyIntersitial()
	{
		showAd = true;
		/*if (interstitial != null)
		{
			if (interstitial.IsLoaded())
			{
				interstitial.Show();
				PlayerPrefs.SetInt("countAds", 0);
			}
			else
			{
				Debug.Log("not loaded");
			}
		}
		else
		{
			Debug.Log("testtest");
		}*/
	}

	public void AdGestion()
	{

		//ShowInterstitialAd();
		if (PlayerPrefs.GetInt("countAds", 0) == 3)
		{
			ShowInterstitialAd();
			PlayerPrefs.SetInt("countAds", 0);
			//this.interstitial.Destroy();
		}
		else
		{
			PlayerPrefs.SetInt("countAds", PlayerPrefs.GetInt("countAds", 0) + 1);
		}

		if (showAd)
		{
			if (interstitial != null)
			{
				if (interstitial.IsLoaded())
				{
					interstitial.Show();
					PlayerPrefs.SetInt("countAds", 0);
					showAd = false;
				}
				else
				{
					Debug.Log("not loaded");
				}
			}
			else
			{
				Debug.Log("testtest");
			}
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
