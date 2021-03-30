using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//for GUI
public class MainMenu : MonoBehaviour
{
    //main menu
    public TextMeshProUGUI startButton;
    public TextMeshProUGUI tutorialButton;
    public TextMeshProUGUI settingButton;
    public TextMeshProUGUI creditsButton;

    //setting
    public TextMeshProUGUI st_TextTitle;
    public TextMeshProUGUI st_SFX;
    public TextMeshProUGUI st_Music;
    public TextMeshProUGUI st_Eng;
    public TextMeshProUGUI st_Thai;
    public TextMeshProUGUI st_Skoi;
    public TextMeshProUGUI st_Boy;
    public TextMeshProUGUI st_Girl;

    //Credits
    public TextMeshProUGUI cr_TextTitle;

    //Tutorial
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

    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        
        switch(scene.name)
        {
            case "MainMenuScene":
                RefreshMenu();
                break;
            case "Tutorial":
                RefreshTutorial();
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
    }

    public void RefreshTutorial()
    {
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
}
