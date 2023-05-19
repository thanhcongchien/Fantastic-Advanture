using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System;

public class TestLogic : MonoBehaviour
{
    [SerializeField] Button btnShowBannerAd;
    [SerializeField] Button btnLoadBannerAd;

    [SerializeField] Button btnShowFullAd;
    [SerializeField] Button btnLoadFullAd;

    [SerializeField] Button btnShowRewardedAd;
    [SerializeField] Button btnLoadRewardedAd;

    [SerializeField] Button btnShowLeaderBoard;
    [SerializeField] Button btnPlusScore;
    [SerializeField] Button btnReportScore;
    [SerializeField] Button btnLoginGooglePlayGames;

    [SerializeField] BannerAdStat bannerAdStat;
    [SerializeField] InterstitialAdStat interstitialAdStat;
    [SerializeField] RewardedAdStat rewardedAdStat;

    private int currentScore;


    private void Awake()
    {
        btnShowBannerAd?.onClick.AddListener(OnButtonShowBannerAdClicked);
        btnLoadBannerAd?.onClick.AddListener(OnButtonLoadBannerAdClicked) ;

        btnShowFullAd?.onClick.AddListener(OnButtonShowFullAdClicked);
        btnLoadFullAd?.onClick.AddListener(OnButtonLoadFullAdClicked);

        btnShowRewardedAd?.onClick.AddListener(OnButtonShowRewardedAdClicked);
        btnLoadRewardedAd?.onClick.AddListener(OnButtonLoadRewardedAdClicked);

        btnShowLeaderBoard?.onClick.AddListener(OnButtonShowLeaderBoardClicked);
        btnPlusScore?.onClick.AddListener(OnButtonPlusScoreClicked);
        btnReportScore?.onClick.AddListener(OnButtonReportScoreClicked);
        btnLoginGooglePlayGames?.onClick.AddListener(OnButtonLoginGooglePlayGamesClicked);
    }

    private void OnButtonLoginGooglePlayGamesClicked()
    {
        Social.localUser.Authenticate(ProcessAuthentication);
    }

    private void OnButtonReportScoreClicked()
    {
        if (isAuthenticatied)
        {
            Social.ReportScore(currentScore, GPGSIds.leaderboard_alice_adventure_leader_board, (bool success) =>
            {
                if (success)
                {

                }
                else
                {

                }
            });
        }     
    }

    private void OnButtonPlusScoreClicked()
    {
        currentScore++;
    }

    private void OnButtonShowLeaderBoardClicked()
    {
        if (isAuthenticatied)
        {
            Social.ShowLeaderboardUI();
        }
    }

    private void OnButtonLoadRewardedAdClicked()
    {
        AdController.LoadRewardedAd(rewardedAdStat);
    }

    private void OnButtonShowRewardedAdClicked()
    {
        AdController.ShowRewardedAd(rewardedAdStat);
    }

    private void OnButtonLoadFullAdClicked()
    {
        AdController.LoadInterstitialAd(interstitialAdStat);
    }

    private void OnButtonShowFullAdClicked()
    {
        AdController.ShowInterstitialAd(interstitialAdStat);
    }

    private void OnButtonLoadBannerAdClicked()
    {
        AdController.LoadBannerAd(bannerAdStat);
    }

    private void OnButtonShowBannerAdClicked()
    {
        AdController.ShowBannerAd(bannerAdStat);
    }

    private void Start()
    {
#if UNITY_ANDROID || UNITY_IOS
        //MobileAds.Initialize(initStatus => { });


        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(ProcessAuthentication); 
#endif
    }


    bool isAuthenticatied = false;

    private void ProcessAuthentication(bool success)
    {
        if (success)
        {
            isAuthenticatied = true;
        }
        else
        {
            isAuthenticatied = false;
        }
    }

    
}

[System.Serializable]
public static class AdController
{
    #region Banner Ad Control

    public static void LoadBannerAd(BannerAdStat bannerAdStat)
    {
        if (bannerAdStat == null)
        {
            return;
        }

        AdRequest adRequest = new AdRequest.Builder().Build();
        bannerAdStat.bannerAd = new BannerView(bannerAdStat.AdId, AdSize.Banner, AdPosition.Top);
        bannerAdStat.bannerAd.LoadAd(adRequest);
    }

    public static void ShowBannerAd(BannerAdStat bannerAdStat)
    {
        if (bannerAdStat == null)
            return;

        if (bannerAdStat.bannerAd == null)
            LoadBannerAd(bannerAdStat);

        bannerAdStat.bannerAd.Show();
    }

    #endregion

    #region Interstitial Ad Control

    public static void LoadInterstitialAd(InterstitialAdStat interstitialAdStat)
    {
        if (interstitialAdStat == null)
        {
            return;
        }

        if (interstitialAdStat.interstitialAd != null)
        {
            interstitialAdStat.interstitialAd.Destroy();
            interstitialAdStat.interstitialAd = null;
        }

        AdRequest adRequest = new AdRequest.Builder().Build();

        InterstitialAd.Load(interstitialAdStat.AdId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("interstitial ad failed to load an ad " +
                               "with error : " + error);
                return;
            }

            Debug.Log("Interstitial ad loaded with response : "
                      + ad.GetResponseInfo());

            interstitialAdStat.interstitialAd = ad;
        });
    }

    public static void ShowInterstitialAd(InterstitialAdStat interstitialAdStat)
    {
        if (interstitialAdStat == null)
            return;

        if (interstitialAdStat.interstitialAd == null)
            LoadInterstitialAd(interstitialAdStat);

        if (interstitialAdStat.interstitialAd.CanShowAd())
        {
            interstitialAdStat.interstitialAd.Show();
        }
    }

    #endregion

    #region Rewarded Ad Control

    public static void LoadRewardedAd(RewardedAdStat rewardedAdStat)
    {
        if (rewardedAdStat == null)
        {
            return;
        }

        AdRequest adRequest = new AdRequest.Builder().Build();
        RewardedAd.Load(rewardedAdStat.AdId, adRequest, (RewardedAd rewardedAd, LoadAdError error) =>
        {
            if (error != null || rewardedAd == null)
            {
                Debug.LogError("rewarded ad failed to load an ad " +
                               "with error : " + error);
                return;
            }

            Debug.Log("rewarded ad loaded with response : "
                      + rewardedAd.GetResponseInfo());

            rewardedAdStat.rewardedAd = rewardedAd;
        });
    }

    public static void ShowRewardedAd(RewardedAdStat rewardedAdStat)
    {
        if (rewardedAdStat == null)
        {
            return;
        }

        if (rewardedAdStat.rewardedAd == null)
        {
            LoadRewardedAd(rewardedAdStat);
            return;
        }

        if (rewardedAdStat.rewardedAd.CanShowAd())
        {
            rewardedAdStat.rewardedAd.Show((Reward reward) =>
            {
                Debug.Log("Get Reward At Here");
            });
        }
    }

    #endregion
}

[System.Serializable]
public class AdStat
{
    [SerializeField] protected bool isTesting;
    [SerializeField] protected string adIdTesting;
    [SerializeField] protected string adIdReal;

    public string AdId
    {
        get
        {
            return isTesting ? adIdTesting : adIdReal;
        }
    }
}

[System.Serializable]
public class BannerAdStat : AdStat
{


    public BannerView bannerAd;

    public bool IsDestroyed()
    {
        if (bannerAd == null)
            return true;

        return bannerAd.IsDestroyed;
    }
}

[System.Serializable]
public class InterstitialAdStat : AdStat
{
    public InterstitialAd interstitialAd;
}

[System.Serializable]
public class RewardedAdStat : AdStat
{
    public RewardedAd rewardedAd;
}
