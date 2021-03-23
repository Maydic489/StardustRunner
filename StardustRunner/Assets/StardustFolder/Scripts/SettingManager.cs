using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using System.Collections.Generic;

namespace MoreMountains.InfiniteRunnerEngine
{
    [Serializable]
    public class LanguageSetting
    {
        public string language;
    }

    public class SettingManager : MMPersistentSingleton<SettingManager>
    {
        public LanguageSetting thisLanguage;

        protected const string _saveFolderName = "CorgiEngine/";
        protected const string _saveFileName = "langauge.settings";

        public void Start()
        {
            LoadSettings();
        }

        public virtual void SaveSettings()
        {
            MMSaveLoadManager.Save(thisLanguage, _saveFileName, _saveFolderName);
        }

        protected virtual void LoadSettings()
        {
            LanguageSetting saveLanguage = (LanguageSetting)MMSaveLoadManager.Load(typeof(LanguageSetting), _saveFileName, _saveFolderName);
            if (saveLanguage != null)
            {
                thisLanguage = saveLanguage;
            }
        }

        public void ChangeToEnglish()
        {
            thisLanguage.language = "english";
        }
        public void ChangeToThai()
        {
            thisLanguage.language = "thai";
        }
        public void ChangeToSkoi()
        {
            thisLanguage.language = "skoi";
        }
    }
}
