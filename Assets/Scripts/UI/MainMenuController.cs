using UnityEditor;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject PlayerHUD;
    [SerializeField]
    private GameObject PlayerCamera;
    [SerializeField]
    private GameObject MenuCamera;

    [SerializeField]

    private bool IsOnMainMenu = true;

    public void OnEscape()
    {
        Debug.Log("Pause Triggered");
    }

    private void Awake()
    {
        Player.SetActive(false);
        PlayerHUD.SetActive(false);
    }

    public void PlayGame()
    {
        Player.SetActive(true);
        PlayerCamera.SetActive(true);
        PlayerHUD.SetActive(true);

        MenuCamera.SetActive(false);
        IsOnMainMenu = false;
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }


    public void ReturnToMenu()
    {
        Player.SetActive(false);
        PlayerCamera.SetActive(false);
        PlayerHUD.SetActive(false);

        MenuCamera.SetActive(true);
        IsOnMainMenu = true;
    }

    //     public VisualElement ui;

    //     public Button playButton;
    //     public Button settingsButton;
    //     public Button quitButton;

    //     private void Awake()
    //     {
    //         ui = GetComponent<UIDocument>().rootVisualElement;
    //     }

    //     private void OnEnable()
    //     {
    //         playButton = ui.Q<Button>("PlayButton");
    //         playButton.clicked += OnPlayClicked;

    //         settingsButton = ui.Q<Button>("SettingsButton");
    //         settingsButton.clicked += OnSettingsClicked;

    //         quitButton = ui.Q<Button>("QuitButton");
    //         quitButton.clicked += OnQuitClicked;
    //     }


    //     private void OnPlayClicked()
    //     {
    //         Debug.Log("PLAY!");
    //     }

    //     private void OnSettingsClicked()
    //     {
    //         Debug.Log("OPTIONS!");
    //     }

    //     private void OnQuitClicked()
    //     {
    //         Application.Quit();
    // #if UNITY_EDITOR
    //     EditorApplication.isPlaying = false;
    // #endif
    //     }
}
