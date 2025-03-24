using UnityEngine;
using UnityEditor;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    [SerializeField] private GameObject PlayerHUD;

    [SerializeField] private GameObject PauseMenu;

    [SerializeField] private GameObject MainMenu;

    [SerializeField] private PlayerFPSController playerFPSController;

    private void Awake()
    {
        PauseMenu.SetActive(false);
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerFPSController.EnableMouseLook();
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
