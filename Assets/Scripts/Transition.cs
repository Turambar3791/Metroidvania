using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public string targetScene;
    public string targetObjectName;
    public string exitDirection;
    public SceneController sceneController;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            sceneController.sceneName = targetScene;
            sceneController.NextScene();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        GameObject targetObject = GameObject.Find(targetObjectName);
        if (targetObject != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                if (exitDirection == "left")
                {
                    player.transform.position = new Vector3(targetObject.transform.position.x - 2f, targetObject.transform.position.y - 1f, targetObject.transform.position.z);
                    player.GetComponent<PlayerMovement>().isFacingRight = false;
                    Vector3 localScale = player.transform.localScale;
                    localScale.x *= -1f;
                    player.transform.localScale = localScale;
                }
                else if (exitDirection == "right")
                {
                    player.transform.position = new Vector3(targetObject.transform.position.x + 1.5f, targetObject.transform.position.y - 1f, targetObject.transform.position.z);
                }
                else if (exitDirection == "top")
                {
                    player.transform.position = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z);
                }
                else if (exitDirection == "bottom")
                {
                    player.transform.position = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z);
                }
            }
        }        
    }
}
