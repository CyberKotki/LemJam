using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject settingsMenu;

    void Start()
    {
        // Make sure settings and pause menus are closed on startup
        CloseSettings();
    }

    void Update()
    {
        if (settingsMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseSettings();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }


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
        Application.Quit();
    }

    public void ShowCredits()
    {
        Debug.Log("Credits");
    }
}