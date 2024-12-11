using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject settingsMenu;
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;


    void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    void Update()
    {
        if (settingsMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseSettings();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    
    public void PauseGame()
    {
        GameIsPaused = !GameIsPaused;
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
    
    // Method to load a new scene by id
    public void ChangeScene(int sceneID)
    {
        Debug.Log("change to scene ID#" + sceneID);
        SceneManager.LoadScene(sceneID);
    }
}