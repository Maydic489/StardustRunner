using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Localization
{
    static string thisLanguage;

    public static string startButton = "Play";
    public static string tutorialButton = "Tutorial";
    public static string settingButton = "Setting";
    public static string creditsButton = "Credits";

    public static void GetLanguage()
    {
        thisLanguage = MoreMountains.InfiniteRunnerEngine.SettingManager.Instance.thisLanguage.language;
        switch (thisLanguage)
        {
            case "english":
                SetEnglish();
                break;
            case "thai":
                SetThai();
                break;
            case "skoi":
                SetSkoi();
                break;
        }
    }

    public static void SetEnglish()
    {
        startButton = "Play";
        tutorialButton = "Tutorial";
        settingButton = "Setting";
        creditsButton = "Credits";
    }
    public static void SetThai()
    {
        startButton = "เล่นเลย";
        tutorialButton = "เล่นไงอะ?";
        settingButton = "ตั้งค่า";
        creditsButton = "เครดิต";
    }
    public static void SetSkoi()
    {
        startButton = "เฬ่นเร่ย";
        tutorialButton = "เฬ่ณงั๊ยอ่?";
        settingButton = "ฏั๊งค่๊";
        creditsButton = "เครฎิฏ";
    }
}

