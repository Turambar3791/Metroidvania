using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsScript : MonoBehaviour
{
    public PauseManager pauseManager;

    public void OpenGameOptions()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void OpenAudioOptions()
    {
        SceneManager.LoadSceneAsync(4);
    }
    public void OpenVideoOptions()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void OpenKeyboardOptions()
    {
        SceneManager.LoadSceneAsync(5);
    }
    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void BackToGameMenu()
    {
        pauseManager.gameMenuPanel.SetActive(true);
        pauseManager.gameOptionsPanel.SetActive(false);
    }
}
