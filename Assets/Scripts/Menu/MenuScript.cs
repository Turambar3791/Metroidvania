using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuScript : MonoBehaviour
{
    public PauseManager pauseManager;
    public SceneController sceneController;

    void Start()
    {
        if (PlayerPrefs.GetString("LastScene", "FirstGameScene") != "FirstGameScene") GameObject.Find("ContinueButton").GetComponent<Button>().interactable = true;
    }

    public void ContinueGame()
    {
        sceneController.sceneName = PlayerPrefs.GetString("LastScene", "FirstGameScene");
        sceneController.NextScene();
    }

    public void NewGame()
    {
        sceneController.sceneName = "FirstGameScene";
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
