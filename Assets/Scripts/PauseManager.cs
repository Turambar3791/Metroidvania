using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject gameMenuPanel;
    public GameObject gameOptionsPanel;

    private bool isPaused = false;
    public bool menuOpenCloseInput;
    private KeyCode defaultMenuInput;

    void Start()
    {
    }

    void Update()
    {
        menuOpenCloseInput = UserInput.instance.MenuOpenCloseInput;
        defaultMenuInput = KeyCode.Escape;
        if (Input.GetKeyDown(defaultMenuInput) || menuOpenCloseInput)
        {
            if (!gameOptionsPanel.activeSelf)
            {
                gameMenuPanel.SetActive(!isPaused);
                TogglePause();
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }
}
