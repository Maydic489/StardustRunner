using UnityEngine;
using UnityEngine.Advertisements;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;


namespace MoreMountains.InfiniteRunnerEngine
{
    public class AdManager : MMSingleton<AdManager>, IUnityAdsListener
    {
        private string appStoreID = "4083128";
        private string playStoreID = "4083129";

        private string interstitialAd = "video";
        private string rewardedVideoAd = "rewardedVideo";
        private string bannerAd = "banner";

        public bool isTargetPlayStore;
        public bool isTestAd = true;

        public enum AdsAction {Stay, Restart,MainMenu};
        public AdsAction nextAction;
        public static bool getFreeHelmet;
        private bool showBanner;

        private void Start()
        {
            Advertisement.AddListener(this);
            InitializeAdvertisement();
            showBanner = false;
            if (SceneManager.GetActiveScene().name == "MainMenuScene")
            {
                Debug.Log("Load Main Menu");
                AdManager.Instance.PlayBannerAd();
            }
        }

        private void Update()
        {
            if (SceneManager.GetActiveScene().name == "MainMenuScene" && !Advertisement.Banner.isLoaded && !showBanner)
            {
                Debug.Log("Load Banner if not load");
                showBanner = true;
                AdManager.Instance.PlayBannerAd();
            }
        }

        private void InitializeAdvertisement()
        {
            if (isTargetPlayStore)
            {
                Advertisement.Initialize(playStoreID, isTestAd);
                return;
            }
            else
            {
                Advertisement.Initialize(appStoreID, isTestAd);
                return;
            }
        }

        public void PlayInterstitialAd()
        {
            if (!Advertisement.IsReady(interstitialAd))
            {
                return;
            }
            else
            {
                Advertisement.Show(interstitialAd);
            }
        }

        public void PlayRewaredAd()
        {
            if (!Advertisement.IsReady(rewardedVideoAd))
            {
                return;
            }
            else
            {
                Advertisement.Show(rewardedVideoAd);
            }
        }

        public void PlayBannerAd()
        {
            if (!Advertisement.IsReady(bannerAd))
            {
                return;
            }
            else
            {
                Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
                Advertisement.Show(bannerAd);
                Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
            }
        }

        public void OnUnityAdsReady(string placementId)
        {
            //throw new System.NotImplementedException();
        }

        public void OnUnityAdsDidError(string message)
        {
            //throw new System.NotImplementedException();
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            //useful for mute in-game sound during ad
            //throw new System.NotImplementedException();
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            Debug.Log("AD finished");
            Advertisement.RemoveListener(this); //so no double reward next time
            switch (showResult)
            {
                case ShowResult.Failed:
                    DoAdsAction();
                    break;
                case ShowResult.Skipped:
                    DoAdsAction();
                    break;
                case ShowResult.Finished:
                    if (placementId == rewardedVideoAd)
                    {
                        Debug.Log("give helmet");
                        getFreeHelmet = true;
                    }
                    DoAdsAction();
                    break;
            }
            Advertisement.AddListener(this);
            //throw new System.NotImplementedException();
        }

        public void SetAdsAction(string action)
        {
            switch(action)
            {
                case "stay":
                    AdManager.Instance.nextAction = AdsAction.Stay;
                    break;
                case "restart":
                    AdManager.Instance.nextAction = AdsAction.Restart;
                    break;
                case "mainmenu":
                    AdManager.Instance.nextAction = AdsAction.MainMenu;
                    break;
            }
        }

        private void DoAdsAction()
        {
            switch(AdManager.Instance.nextAction)
            {
                case AdsAction.Stay:
                    Debug.Log("stay");
                    break;
                case AdsAction.Restart:
                    Debug.Log("restart");
                    AdManager.Instance.GetComponent<LevelSelector>().RestartLevel();
                    break;
                case AdsAction.MainMenu:
                    Debug.Log("mainmenu");
                    AdManager.Instance.GetComponent<LevelSelector>().GoToLevel();
                    break;
            }
        }
    }
}
