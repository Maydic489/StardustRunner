using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class SingletonButton : MonoBehaviour
    {
        public void SaveSetting()
        {
            SettingManager.Instance.SaveSettings();
        }
        public void ChangeToEnglish()
        {
            SettingManager.Instance.ChangeToEnglish();
        }
        public void ChangeToThai()
        {
            SettingManager.Instance.ChangeToThai();
        }
        public void ChangeToSkoi()
        {
            SettingManager.Instance.ChangeToSkoi();
        }

        public void SaveSoundSettings()
        {
            SoundManager.Instance.SaveSoundSettings();
        }
    }
}
