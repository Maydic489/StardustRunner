using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//for GUI
public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI startButton;
    public TextMeshProUGUI tutorialButton;
    public TextMeshProUGUI settingButton;
    public TextMeshProUGUI creditsButton;

    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        
        switch(scene.name)
        {
            case "MainMenuScene":
                RefreshMenu();
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
    }
}
