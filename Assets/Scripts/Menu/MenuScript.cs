using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public PauseManager pauseManager;
    public SceneController sceneController;

    public void PlayGame()
    {
        sceneController.NextScene();
    }
    public void OpenOptions()
    {
        SceneManager.LoadSceneAsync("Options");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        pauseManager.gameMenuPanel.SetActive(false);
        pauseManager.TogglePause();
    }

    public void ReturnToMenu()
    {
        pauseManager.TogglePause();
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
