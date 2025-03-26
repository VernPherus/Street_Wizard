using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine;

public class win : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private PlayerFPSController playerFPSController;
    [SerializeField] private GameObject winUI;

    private string scene;

    private void Awake()
    {
        winUI.SetActive(false);
        scene = SceneManager.GetActiveScene().name;
    }

    void OnTriggerEnter(Collider other)
    {
        ActivateWinScreen();
    }

    public void ActivateWinScreen()
    {
        winUI.SetActive(true);
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
