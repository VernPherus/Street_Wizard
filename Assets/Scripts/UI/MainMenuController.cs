using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    public VisualElement ui;

    public Button playButton;
    public Button settingsButton;
    public Button quitButton;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        playButton = ui.Q<Button>("PlayButton");
        playButton.clicked += OnPlayClicked;

        settingsButton = ui.Q<Button>("SettingsButton");
        settingsButton.clicked += OnSettingsClicked;

        quitButton = ui.Q<Button>("QuitButton");
        quitButton.clicked += OnQuitClicked;
    }


    private void OnPlayClicked()
    {
        Debug.Log("PLAY!");
    }

    private void OnSettingsClicked()
    {
        Debug.Log("OPTIONS!");
    }

    private void OnQuitClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
    EditorApplication.isPlaying = false;
#endif
    }
}
