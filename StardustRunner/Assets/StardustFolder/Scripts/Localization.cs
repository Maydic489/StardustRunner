using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Localization
{
    static string thisLanguage;

    //main menu
    public static string startButton = "Play";
    public static string tutorialButton = "Tutorial";
    public static string settingButton = "Setting";
    public static string creditsButton = "Credits";

    //setting
    public static string st_TextTitle;
    public static string st_SFX;
    public static string st_Music;
    public static string st_Eng;
    public static string st_Thai;
    public static string st_Skoi;
    public static string st_Boy;
    public static string st_Girl;

    //Credits
    public static string cr_TextTitle;

    public static void GetLanguage()
    {
        thisLanguage = MoreMountains.InfiniteRunnerEngine.SettingManager.Instance.thisSetting.language;
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
        //main menu
        startButton = "Play";
        tutorialButton = "Tutorial";
        settingButton = "Setting";
        creditsButton = "Credits";

        //setting
        st_TextTitle = "Setting";
        st_SFX = "Sound FX";
        st_Music = "Music";
        st_Eng = "English";
        st_Thai = "Thai";
        st_Skoi = "Skoi";
        st_Boy = "Boy";
        st_Girl = "Girl";

        //credits
        cr_TextTitle = "Credits";
    }
    public static void SetThai()
    {
        //main menu
        startButton = "เล่นเลย";
        tutorialButton = "เล่นไงอะ?";
        settingButton = "ตั้งค่า";
        creditsButton = "เครดิต";

        //setting
        st_TextTitle = "ตั้งค่า";
        st_SFX = "เสียงเอฟเฟค";
        st_Music = "เสียงเพลง";
        st_Eng = "อังกฤษ";
        st_Thai = "ไทย";
        st_Skoi = "สก๊อย";
        st_Boy = "เด็กแว้น";
        st_Girl = "สาวสก๊อย";

        //credits
        cr_TextTitle = "เครดิต";
    }
    public static void SetSkoi()
    {
        //main menu
        startButton = "เฬ่นเร่ย";
        tutorialButton = "เฬ่ณงั๊ยอ่?";
        settingButton = "ฏั๊งค่๊";
        creditsButton = "เครฎิฏ";

        //setting
        st_TextTitle = "ฏั๊งค่๊";
        st_SFX = "เษิ๊ญงเฮฟเฟค";
        st_Music = "เษิ๊ญงเภลง";
        st_Eng = "อังกหรือษ";
        st_Thai = "ไธญ";
        st_Skoi = "สก๊อย";
        st_Boy = "เฎ็กเเว๊ณ";
        st_Girl = "ษ๊วสก๊อย";

        //credits
        cr_TextTitle = "เครฎิฏ";
    }
}

