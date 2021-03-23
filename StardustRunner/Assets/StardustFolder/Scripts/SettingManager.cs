using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using System.Collections.Generic;

namespace MoreMountains.InfiniteRunnerEngine
{
    //public class LanguageSetting
    //{
    //    public string language;
    //}

    public class SettingManager : MMPersistentSingleton<SettingManager>
    {
        public string language;

        private void Awake()
        {
            language = "english";
        }

        public void ChangeToEnglish()
        {
            language = "english";
        }
        public void ChangeToThai()
        {
            language = "thai";
        }
        public void ChangeToSkoi()
        {
            language = "skoi";
        }
    }
}
