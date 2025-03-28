using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private PlayerFPSController playerFPSController;

    private string scene;

    private void Awake()
    {
        gameObject.SetActive(false);
        scene = SceneManager.GetActiveScene().name;
    }

    public void ActivateDeathScreen()
    {
        gameObject.SetActive(true);
        playerGameObject.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        playerFPSController.DisableAllInput();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Reset()
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

}
