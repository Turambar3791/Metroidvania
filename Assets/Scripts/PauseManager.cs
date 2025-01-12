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

    private float hitPauseCounter;
    public bool isHit;

    void Start()
    {
        hitPauseCounter = 0f;
        isHit = false;
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

        if (hitPauseCounter > 0)
        {
            hitPauseCounter -= Time.deltaTime;
            Debug.Log(hitPauseCounter);
        } else if (hitPauseCounter<=0 && isHit)
        {
            TogglePause();
            isHit = false;
        }
        
    }

    public void HitPause(float duration)
    {
        StartCoroutine(PauseCoroutine(duration));
    }

    private IEnumerator PauseCoroutine(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }
}
