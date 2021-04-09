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
                    GetComponent<LevelSelector>().RestartLevel();
                    break;
                case ShowResult.Skipped:
                    GetComponent<LevelSelector>().RestartLevel();
                    break;
                case ShowResult.Finished:
                    if (placementId == rewardedVideoAd)
                    {
                        Debug.Log("give 10K");
                        GameManager.Instance.AddPoints(10000f);
                        Advertisement.RemoveListener(this);
                    }

                    AdManager.Instance.GetComponent<LevelSelector>().RestartLevel();
                    break;
            }
            //throw new System.NotImplementedException();
        }
    }
}
