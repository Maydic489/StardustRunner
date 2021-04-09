using UnityEngine;
using UnityEngine.Advertisements;
using MoreMountains.Tools;


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

        private bool isRestart;
        public static bool getFreeHelmet;

        private void Start()
        {
            Advertisement.AddListener(this);
            InitializeAdvertisement();
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
                Advertisement.Show(bannerAd);
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
            switch (showResult)
            {
                case ShowResult.Failed:
                    if(isRestart)
                        AdManager.Instance.GetComponent<LevelSelector>().RestartLevel();
                    else
                        AdManager.Instance.GetComponent<LevelSelector>().GoToLevel();
                    break;
                case ShowResult.Skipped:
                    if (isRestart)
                        AdManager.Instance.GetComponent<LevelSelector>().RestartLevel();
                    else
                        AdManager.Instance.GetComponent<LevelSelector>().GoToLevel();
                    break;
                case ShowResult.Finished:
                    if (placementId == rewardedVideoAd)
                    {
                        Debug.Log("give helmet");
                        getFreeHelmet = true;
                        Advertisement.RemoveListener(this);
                    }
                    if (isRestart)
                        AdManager.Instance.GetComponent<LevelSelector>().RestartLevel();
                    else
                        AdManager.Instance.GetComponent<LevelSelector>().GoToLevel();
                    break;
            }
            //throw new System.NotImplementedException();
        }

        public void SetRestart(bool restart)
        {
            isRestart = restart;
        }
    }
}
