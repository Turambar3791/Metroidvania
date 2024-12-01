using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public PauseManager pauseManager;

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(6);
    }
    public void OpenOptions()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void OpenGameOptions()
    {
        pauseManager.gameMenuPanel.SetActive(false);
        pauseManager.gameOptionsPanel.SetActive(true);
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
        SceneManager.LoadSceneAsync(0);
    }
}
