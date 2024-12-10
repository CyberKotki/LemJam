using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool GameIsPaused;
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    void Start()
    {
        // Make sure settings and pause menus are closed on startup
        CloseSettings();
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (settingsMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseSettings();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameIsPaused = !GameIsPaused;
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (GameIsPaused)
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }
        else
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }

        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    // use if (!PauseControl.GameIsPaused) {} to disable inputs


    // Method to load a new scene by id
    public void ChangeScene(int sceneID)
    {
        Debug.Log("change to scene ID#" + sceneID);
        SceneManager.LoadScene(sceneID);
    }

    public void OpenSettings()
    {
        Debug.Log("Settings");
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void ShowCredits()
    {
        Debug.Log("Credits");
    }
}