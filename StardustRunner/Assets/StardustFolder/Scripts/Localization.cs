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

    //Pause Menu
    public static string pm_Pause;
    public static string pm_Resume;
    public static string pm_Restart;
    public static string pm_Setting;
    public static string pm_MainMenu;
    public static string pm_TextTitle;
    public static string pm_SFX;
    public static string pm_Music;
    public static string pm_Eng;
    public static string pm_Thai;
    public static string pm_Skoi;

    //Game Over
    public static string go_GameOver;
    public static string go_HighScore;
    public static string go_NewScore;
    public static string go_Share;
    public static string go_Restart;
    public static string go_MainMenu;

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

        //pause menu
        pm_Pause = "PAUSE";
        pm_Resume = "Resume";
        pm_Restart = "Restart";
        pm_Setting = "Setting";
        pm_MainMenu = "Main Menu";
        pm_TextTitle = st_TextTitle;
        pm_SFX = st_SFX;
        pm_Music = st_Music;
        pm_Eng = st_Eng;
        pm_Thai = st_Thai;
        pm_Skoi = st_Skoi;

        //Game Over
        go_GameOver = "GAME OVER";
        go_HighScore = "High Score";
        go_NewScore = "New Score";
        go_Share = "Share";
        go_Restart = pm_Restart;
        go_MainMenu = pm_MainMenu;
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

        //pause menu
        pm_Pause = "หยุดเกม";
        pm_Resume = "เล่นต่อ";
        pm_Restart = "เริ่มใหม่";
        pm_Setting = "ตั้งค่า";
        pm_MainMenu = "ไปหน้าแรก";
        pm_TextTitle = st_TextTitle;
        pm_SFX = st_SFX;
        pm_Music = st_Music;
        pm_Eng = st_Eng;
        pm_Thai = st_Thai;
        pm_Skoi = st_Skoi;

        //Game Over
        go_GameOver = "เกมโอเวอร์";
        go_HighScore = "คะแนนสูงสุด";
        go_NewScore = "คะแนนใหม่";
        go_Share = "แชร์";
        go_Restart = pm_Restart;
        go_MainMenu = pm_MainMenu;
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
        tu_Turn1 = "ปัฎซ๊๊ญหรืฮฆว๊\nเภื่อเฬิ๊๊ยวซ๊๊ยหรืฮฆว๊\n[ลองปัฎษิ]";
        tu_Turn2 = "ปัฎซ๊๊ญหรือฆว๊\nเพื่ฮเฬิ๊๊ญวซ๊๊ยหรือฆว๊";
        tu_Turn3 = "ปัฎซ๊๊ญหรือฆว๊\nเพื่ฮเฬิ๊๊ญวซ๊๊ยหรือฆว๊";
        tu_Up = "ปัฎฆึ๊ฯหรือเเฏ๊หน๊๊จฮ\nเพื่ฮญกล๊ฮ";
        tu_Break = "ษ๊ม่รถพุ่งชน\nษิ่งกิ๊ฎฆว๊งบ๊งชณิ๊ฎไฎ๊\n[เเฏ๊จอเภื่อเฬ่ณฏ่อ]";
        tu_Down = "ปัฎลงเพื่อษไลฎ์หลบ";
        tu_Fuel = "คว๊ฒเร็วจ่ลฎลง\nเฒื่ฮฯ๊ำมัลเหลือน๊อญ\nเเล๊เกฒจ่จบถ๊๊น๊ำมัลบ์หฒฎ\n[เเฏ๊จฮเภื่อเฬป์่นฏ่อ]";
        tu_Helmet = "หฒ๊กกับ์ลฯ็อคจ่\nช่วญป๊องกัลก๊รชฯ\nไฎ๊ 1 ฅั๊ฐ์งหสุ์\n[เเฏ๊จอเภื่อเฬ่นฏ่ฮ]";
        tu_3Helmet = "จ่ไฎ๊บุ๊๊ษคว๊มเร็ว\nเมื่อเกํบหฒ๊กซ๊อฯ\nกัล 3 ใบ\n[เเฏ๊จอเภื่อเฬซ์่ณฏ่อ]";
        tu_End = "เเค่ณิ๊เเหล๊\nปั๊บ์ยเเว๊ฯไฎ๊เร่รฬฬ!\n[เเฏ๊จอเภื่อเห์ฬ่ฯฏ่อ]";

        //pause menu
        pm_Pause = "หยุฎเกฒ";
        pm_Resume = "เฬ่นฏ่ฮ";
        pm_Restart = "เริ่มใหฒ่";
        pm_Setting = "ฏั๊งค่๊";
        pm_MainMenu = "ปั๊บ์ยหณ๊๊เเรก";
        pm_TextTitle = st_TextTitle;
        pm_SFX = st_SFX;
        pm_Music = st_Music;
        pm_Eng = st_Eng;
        pm_Thai = st_Thai;
        pm_Skoi = st_Skoi;

        //Game Over
        go_GameOver = "เกฒโฮเวฮร์";
        go_HighScore = "ขร๊เเนณษุ๊งษุฎ";
        go_NewScore = "ขร๊เเนณใหฒ่";
        go_Share = "แฎช์";
        go_Restart = pm_Restart;
        go_MainMenu = pm_MainMenu;
    }
}

