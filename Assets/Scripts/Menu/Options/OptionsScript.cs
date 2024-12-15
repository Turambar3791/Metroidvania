using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsScript : MonoBehaviour
{
    public PauseManager pauseManager;

    public void OpenGameOptions()
    {
        SceneManager.LoadSceneAsync("OptionsGame");
    }
    public void OpenAudioOptions()
    {
        SceneManager.LoadSceneAsync("OptionsAudio");
    }
    public void OpenVideoOptions()
    {
        SceneManager.LoadSceneAsync("OptionsVideo");
    }
    public void OpenKeyboardOptions()
    {
        SceneManager.LoadSceneAsync("OptionsKeyboard");
    }
    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void BackToGameMenu()
    {
        pauseManager.gameMenuPanel.SetActive(true);
        pauseManager.gameOptionsPanel.SetActive(false);
    }
}
