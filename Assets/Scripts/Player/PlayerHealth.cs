using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public int maxHealth = 5;
    public int health;

    //immunity
    public float ImmunityCounter;
    public float ImmunityTotalTime = 1f;

    //death
    public float DeathCounter;
    public float DeathTotalTime = 2f;

    private bool enableDeathCode;

    public string spawnScene;

    [SerializeField] private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        enableDeathCode = true;
        if (health == 0) health = maxHealth;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // immunity
        if (ImmunityCounter > 0)
        {
            ImmunityCounter -= Time.deltaTime;
        }

        // death
        if (DeathCounter <= 0)
        {
            if (health <= 0 && enableDeathCode)
            {
                enableDeathCode = false;
                GameObject sceneController = GameObject.Find("GameManager");
                Debug.Log(spawnScene);
                if (spawnScene != "")
                {
                    sceneController.GetComponent<SceneController>().sceneName = spawnScene;
                }
                else
                {
                    sceneController.GetComponent<SceneController>().sceneName = "FirstGameScene";
                }
                sceneController.GetComponent<SceneController>().NextScene();
                SceneManager.sceneLoaded += OnSceneLoaded;
            } 
        }
        else
        {
            DeathCounter -= Time.deltaTime;
        }

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        if (spawnPoint != null)
        {
            player.transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, -2);
        }
        playerMovement.KnockbackCounter = 0;
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        GameObject.Find("PauseManager").GetComponent<PauseManager>().HitPause(0.05f);
        playerMovement.KnockbackCounter = playerMovement.KnockbackTotalTime;
        ImmunityCounter = ImmunityTotalTime;
        if (health <= 0)
        {
            if (DeathCounter <= 0) DeathCounter = DeathTotalTime;
        }
    }
}
