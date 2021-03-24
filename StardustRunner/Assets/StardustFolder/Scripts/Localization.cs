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

    //tutorial
    public static string tu_Turn1;
    public static string tu_Turn2;
    public static string tu_Turn3;
    public static string tu_Up;
    public static string tu_Break;
    public static string tu_Down;
    public static string tu_Fuel;
    public static string tu_Helmet;
    public static string tu_3Helmet;
    public static string tu_End;

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

        //tutorial
        tu_Turn1 = "Swipe left or right\nto turn left or right.\n[Try it]";
        tu_Turn2 = "Swipe left or right\nto turn left or right.";
        tu_Turn3 = "Swipe left or right\nto turn left or right.";
        tu_Up = "Swipe up\nor tap the screen\nto do a wheelie.";
        tu_Break = "It can breakthrough\nsome type of obstacle.\n[Tap to continue]";
        tu_Down = "Swipe down to slide.";
        tu_Fuel = "You will slow down\nwhen your gas is low\nand the game will end\nwhen you run out of gas.\n[tap to continue]";
        tu_Helmet = "A helmet will\nprevent crashing\nfor one time each.\n[tap to continue]";
        tu_3Helmet = "You'll get a boost\nif you can stack\nthree helmets.\n[tap to continue]";
        tu_End = "That's all!\nLET'S GO!\n[tap to continue]";
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

        //tutorial
        tu_Turn1 = "ปัดซ้ายหรือขวา\nเพื่อเลี้ยวซ้ายหรือขวา\n[ลองปัดสิ]";
        tu_Turn2 = "ปัดซ้ายหรือขวา\nเพื่อเลี้ยวซ้ายหรือขวา";
        tu_Turn3 = "ปัดซ้ายหรือขวา\nเพื่อเลี้ยวซ้ายหรือขวา";
        tu_Up = "ปัดขึ้นหรือแตะหน้าจอ\nเพื่อยกล้อ";
        tu_Break = "สามารถพุ่งชน\nสิ่งกีดขวางบางชนิดได้\n[แตะจอเพื่อเล่นต่อ]";
        tu_Down = "ปัดลงเพื่อสไลด์หลบ";
        tu_Fuel = "ความเร็วจะลดลง\nเมื่อน้ำมันเหลือน้อย\nและเกมจะจบถ้าน้ำมันหมด\n[แตะจอเพื่อเล่นต่อ]";
        tu_Helmet = "หมวกกันน็อคจะ\nช่วยป้องกันการชน\nได้ 1 ครั้ง\n[แตะจอเพื่อเล่นต่อ]";
        tu_3Helmet = "จะได้บู้สความเร็ว\nเมื่อเก็บหมวกซ้อน\nกัน 3 ใบ\n[แตะจอเพื่อเล่นต่อ]";
        tu_End = "แค่นี้แหละ\nไปแว้นได้แล้ว!\n[แตะจอเพื่อเล่นต่อ]";
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

        //tutorial
        tu_Turn1 = "";
        tu_Turn2 = "";
        tu_Up = "";
        tu_Break = "";
        tu_Down = "";
        tu_Fuel = "";
        tu_Helmet = "";
        tu_3Helmet = "";
        tu_End = "";
    }
}

