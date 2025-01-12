using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPoint : MonoBehaviour
{
    private PlayerHealth playerHealth;

    bool playerTouch = false;


    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && playerTouch)
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            playerHealth.health = 5;
            string sceneName = SceneManager.GetActiveScene().name;
            playerHealth.spawnScene = sceneName;
            PlayerPrefs.SetString("LastScene", sceneName);
            PlayerPrefs.Save();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerTouch = false;
        }
    }
}
