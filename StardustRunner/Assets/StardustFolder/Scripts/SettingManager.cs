using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using System.Collections.Generic;

namespace MoreMountains.InfiniteRunnerEngine
{
    [Serializable]
    public class GeneralSetting
    {
        public string language;
        public string gender;
    }

    public class SettingManager : MMPersistentSingleton<SettingManager>
    {
        public GeneralSetting thisSetting;

        protected const string _saveFolderName = "CorgiEngine/";
        protected const string _saveFileName = "General.settings";

        public void Start()
        {
            LoadSettings();
        }

        public virtual void SaveSettings()
        {
            MMSaveLoadManager.Save(thisSetting, _saveFileName, _saveFolderName);
        }

        protected virtual void LoadSettings()
        {
            GeneralSetting saveLanguage = (GeneralSetting)MMSaveLoadManager.Load(typeof(GeneralSetting), _saveFileName, _saveFolderName);
            if (saveLanguage != null)
            {
                thisSetting = saveLanguage;
            }
            else
            {
                thisSetting.language = "english";
                thisSetting.gender = "boy";
            }
        }

        public void ChangeToEnglish()
        {
            thisSetting.language = "english";
        }
        public void ChangeToThai()
        {
            thisSetting.language = "thai";
        }
        public void ChangeToSkoi()
        {
            thisSetting.language = "skoi";
        }

        public void ChangeToBoy()
        {
            thisSetting.gender = "boy";
        }
        public void ChangeToGirl()
        {
            thisSetting.gender = "girl";
        }
    }
}
