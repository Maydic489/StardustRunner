using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//for GUI
//for refresh GUI in general

public class MainMenu : MonoBehaviour
{
    //main menu
    [Header("Main Menu")]
    public TextMeshProUGUI startButton;
    public TextMeshProUGUI tutorialButton;
    public TextMeshProUGUI settingButton;
    public TextMeshProUGUI creditsButton;

    //setting
    [Header("Setting")]
    public TextMeshProUGUI st_TextTitle;
    public TextMeshProUGUI st_SFX;
    public TextMeshProUGUI st_Music;
    public TextMeshProUGUI st_Eng;
    public TextMeshProUGUI st_Thai;
    public TextMeshProUGUI st_Skoi;
    public TextMeshProUGUI st_Boy;
    public TextMeshProUGUI st_Girl;
    public GameObject engButt;
    public GameObject thaiButt;
    public GameObject skoiButt;
    public GameObject boyButt;
    public GameObject girlButt;

    //Credits
    [Header("Credits")]
    public TextMeshProUGUI cr_TextTitle;

    //Tutorial
    [Header("Tutorial")]
    public Text tu_Turn1;
    public Text tu_Turn2;
    public Text tu_Turn3;
    public Text tu_Up;
    public Text tu_Break;
    public Text tu_Down;
    public Text tu_Fuel;
    public Text tu_Helmet;
    public Text tu_3Helmet;
    public Text tu_End;

    [Header("PauseMenu")]
    public TextMeshProUGUI pm_Pause;
    public TextMeshProUGUI pm_Resume;
    public TextMeshProUGUI pm_Restart;
    public TextMeshProUGUI pm_Setting;
    public TextMeshProUGUI pm_MainMenu;
    public TextMeshProUGUI pm_TextTitle;
    public TextMeshProUGUI pm_SFX;
    public TextMeshProUGUI pm_Music;
    public TextMeshProUGUI pm_Eng;
    public TextMeshProUGUI pm_Thai;
    public TextMeshProUGUI pm_Skoi;

    [Header("GameOver")]
    public TextMeshProUGUI go_GameOver;
    public TextMeshProUGUI go_HighScore;
    public TextMeshProUGUI go_NewScore;
    public TextMeshProUGUI go_Share;
    public TextMeshProUGUI go_Restart;
    public TextMeshProUGUI go_MainMenu;

    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        switch (scene.name)
        {
            case "MainMenuScene":
                RefreshMenu();
                break;
            case "Tutorial":
                RefreshTutorial();
                RefreshPauseMenu();
                RefreshGameOver();
                break;
            case "MainScene":
                RefreshMain();
                RefreshPauseMenu();
                RefreshGameOver();
                break;
        }
    }

    public void RefreshMenu()
    {
        Localization.GetLanguage();
        startButton.text = Localization.startButton;
        tutorialButton.text = Localization.tutorialButton;
        settingButton.text = Localization.settingButton;
        creditsButton.text = Localization.creditsButton;

        st_TextTitle.text = Localization.st_TextTitle;
        st_SFX.text = Localization.st_SFX;
        st_Music.text = Localization.st_Music;
        st_Eng.text = Localization.st_Eng;
        st_Thai.text = Localization.st_Thai;
        st_Skoi.text = Localization.st_Skoi;
        st_Boy.text = Localization.st_Boy;
        st_Girl.text = Localization.st_Girl;

        cr_TextTitle.text = Localization.cr_TextTitle;

        Debug.Log(MoreMountains.InfiniteRunnerEngine.SettingManager.Instance.thisSetting.language);
        switch(MoreMountains.InfiniteRunnerEngine.SettingManager.Instance.thisSetting.language)
        {
            case "english":
                engButt.GetComponent<MarkedButton>().TriggerMove();
                break;
            case "thai":
                thaiButt.GetComponent<MarkedButton>().TriggerMove();
                break;
            case "skoi":
                skoiButt.GetComponent<MarkedButton>().TriggerMove();
                break;
        }
        switch (MoreMountains.InfiniteRunnerEngine.SettingManager.Instance.thisSetting.gender)
        {
            case "boy":
                boyButt.GetComponent<MarkedButton>().TriggerMove();
                break;
            case "girl":
                girlButt.GetComponent<MarkedButton>().TriggerMove();
                break;
        }
    }

    public void RefreshTutorial()
    {
        Localization.GetLanguage();
        tu_Turn1.text = Localization.tu_Turn1;
        tu_Turn2.text = Localization.tu_Turn2;
        tu_Turn3.text = Localization.tu_Turn3;
        tu_Up.text = Localization.tu_Up;
        tu_Break.text = Localization.tu_Break;
        tu_Down.text = Localization.tu_Down;
        tu_Fuel.text = Localization.tu_Fuel;
        tu_Helmet.text = Localization.tu_Helmet;
        tu_3Helmet.text = Localization.tu_3Helmet;
        tu_End.text = Localization.tu_End;
    }

    public void RefreshMain()
    {
        Debug.Log("Refresh Main");
    }

    public void RefreshPauseMenu()
    {
        Debug.Log("refresh pauseMenu");
        Localization.GetLanguage();
        pm_Pause.text = Localization.pm_Pause;
        pm_Resume.text = Localization.pm_Resume;
        pm_Restart.text = Localization.pm_Restart;
        pm_Setting.text = Localization.pm_Setting;
        pm_MainMenu.text = Localization.pm_MainMenu;
        pm_TextTitle.text = Localization.pm_TextTitle;
        pm_SFX.text = Localization.pm_SFX;
        pm_Music.text = Localization.pm_Music;
        pm_Eng.text = Localization.pm_Eng;
        pm_Thai.text = Localization.pm_Thai;
        pm_Skoi.text = Localization.pm_Skoi;

        switch (MoreMountains.InfiniteRunnerEngine.SettingManager.Instance.thisSetting.language)
        {
            case "english":
                engButt.GetComponent<MarkedButton>().TriggerMove();
                break;
            case "thai":
                thaiButt.GetComponent<MarkedButton>().TriggerMove();
                break;
            case "skoi":
                skoiButt.GetComponent<MarkedButton>().TriggerMove();
                break;
        }
    }

    public void RefreshGameOver()
    {
        Localization.GetLanguage();
        go_GameOver.text = Localization.go_GameOver;
        go_HighScore.text = Localization.go_HighScore;
        go_NewScore.text = Localization.go_NewScore;
        go_Share.text = Localization.go_Share;
        go_Restart.text = Localization.go_Restart;
        go_MainMenu.text = Localization.go_MainMenu;
    }
}
