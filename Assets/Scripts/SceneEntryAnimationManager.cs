using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEntryAnimationManager : MonoBehaviour
{
    public Animator animator;
    public GameObject canvas;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(PlayEntryAnimation());
    }

    IEnumerator PlayEntryAnimation()
    {
        canvas.SetActive(true);
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        canvas.SetActive(false);
    }
}
