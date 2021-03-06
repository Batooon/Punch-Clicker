﻿using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    public static AdsManager Instance;

    [SerializeField] private BannerPosition _bottomBannerPosition;

#if UNITY_EDITOR
    public bool _testMode = true;
#elif UNITY_ANDROID
    public bool _testMode = false;
#endif

#if UNITY_ANDROID
    private readonly string _storeId = "3445518";
#elif UNITY_IOS
        private readonly string _storeId = "3445519";
#endif

    private readonly string _rewardedVideoPlacement = "rewardedVideo";
    private readonly string _bottomBannerPlacement = "BottomBanner";
    private readonly string _adVideoPlacement = "video";
    private UnityEvent _rewardedAdFinished;

    public void Init()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Advertisement.AddListener(this);
#if UNITY_ANDROID
        Advertisement.Initialize(_storeId, _testMode);
#endif
    }

    private void Start()
    {
        StartCoroutine(ActivateBanner());
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady(_adVideoPlacement))
            Advertisement.Show(_adVideoPlacement);
    }

    public void ShowRewardedAd(UnityEvent adFinished)
    {
        if (Advertisement.IsReady(_rewardedVideoPlacement))
        {
            Instance._rewardedAdFinished = adFinished;
            Advertisement.Show(_rewardedVideoPlacement);
        }
    }

    public void ShowBanner()
    {
        if (Advertisement.IsReady(_bottomBannerPlacement)) 
            Advertisement.Banner.Show(_bottomBannerPlacement);
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == _rewardedVideoPlacement)
        {
            if (showResult == ShowResult.Finished)
            {
                _rewardedAdFinished?.Invoke();
            }
        }
    }

    public void OnUnityAdsReady(string placementId) { }
    public void OnUnityAdsDidStart(string placementId) { }
    public void OnUnityAdsDidError(string message) { }

    private IEnumerator ActivateBanner()
    {
        while (Advertisement.IsReady(_bottomBannerPlacement) == false)
            yield return new WaitForSeconds(.5f);

        Advertisement.Banner.SetPosition(_bottomBannerPosition);
        Advertisement.Banner.Hide();
    }
}
