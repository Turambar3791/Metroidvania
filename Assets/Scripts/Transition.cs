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

    private int currentHealth;
    private string currentSpawnPointSceneName;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            currentHealth = collider.gameObject.GetComponent<PlayerHealth>().health;
            currentSpawnPointSceneName = collider.gameObject.GetComponent<PlayerHealth>().spawnScene;
            if (exitDirection == "top right" || exitDirection == "top left")
            {
                collider.gameObject.GetComponent<PlayerMovement>().topFlyCounter = 1f;
            }
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
            player.GetComponent<PlayerHealth>().health = currentHealth;
            player.GetComponent<PlayerHealth>().spawnScene = currentSpawnPointSceneName;
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
                else if (exitDirection == "top right")
                {
                    player.transform.position = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y + 2f, targetObject.transform.position.z);
                    player.GetComponent<PlayerMovement>().exitTopCounter = 0.2f;
                    player.GetComponent<PlayerMovement>().exitTopSideDirection = 1f;
                }
                else if (exitDirection == "top left")
                {
                    player.transform.position = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y + 2f, targetObject.transform.position.z);
                    player.GetComponent<PlayerMovement>().isFacingRight = false;
                    Vector3 localScale = player.transform.localScale;
                    localScale.x *= -1f;
                    player.transform.localScale = localScale;
                    player.GetComponent<PlayerMovement>().exitTopCounter = 0.2f;
                    player.GetComponent<PlayerMovement>().exitTopSideDirection = -1f;
                }
                else if (exitDirection == "bottom")
                {
                    player.transform.position = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y - 2f, targetObject.transform.position.z);
                }
            }
        }        
    }
}
