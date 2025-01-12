using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    private GameObject Life1 = default;
    private GameObject Life2 = default;
    private GameObject Life3 = default;
    private GameObject Life4 = default;
    private GameObject Life5 = default;
    private GameObject LifeEmpty = default;
    private Sprite spriteRendererFull;
    private Sprite spriteRendererEmpty;

    // Start is called before the first frame update
    void Start()
    {
        Life1 = GameObject.Find("Life1").gameObject;
        Life2 = GameObject.Find("Life2").gameObject;
        Life3 = GameObject.Find("Life3").gameObject;
        Life4 = GameObject.Find("Life4").gameObject;
        Life5 = GameObject.Find("Life5").gameObject;
        LifeEmpty = GameObject.Find("LifeEmpty").gameObject;
        spriteRendererFull = Life1.GetComponent<SpriteRenderer>().sprite;
        spriteRendererEmpty = LifeEmpty.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.health == 5) {
            Life1.GetComponent<SpriteRenderer>().sprite = spriteRendererFull;
            Life2.GetComponent<SpriteRenderer>().sprite = spriteRendererFull;
            Life3.GetComponent<SpriteRenderer>().sprite = spriteRendererFull;
            Life4.GetComponent<SpriteRenderer>().sprite = spriteRendererFull;
            Life5.GetComponent<SpriteRenderer>().sprite = spriteRendererFull;
        } else if (playerHealth.health == 4) {
            Life1.GetComponent<SpriteRenderer>().sprite = spriteRendererFull;
            Life2.GetComponent<SpriteRenderer>().sprite = spriteRendererFull;
            Life3.GetComponent<SpriteRenderer>().sprite = spriteRendererFull;
            Life4.GetComponent<SpriteRenderer>().sprite = spriteRendererFull;
            Life5.GetComponent<SpriteRenderer>().sprite = spriteRendererEmpty;
        } else if (playerHealth.health == 3) {
            Life1.GetComponent<SpriteRenderer>().sprite = spriteRendererFull;
            Life2.GetComponent<SpriteRenderer>().sprite = spriteRendererFull;
            Life3.GetComponent<SpriteRenderer>().sprite = spriteRendererFull;
            Life4.GetComponent<SpriteRenderer>().sprite = spriteRendererEmpty;
            Life5.GetComponent<SpriteRenderer>().sprite = spriteRendererEmpty;
        } else if (playerHealth.health == 2) {
            Life1.GetComponent<SpriteRenderer>().sprite = spriteRendererFull;
            Life2.GetComponent<SpriteRenderer>().sprite = spriteRendererFull;
            Life3.GetComponent<SpriteRenderer>().sprite = spriteRendererEmpty;
            Life4.GetComponent<SpriteRenderer>().sprite = spriteRendererEmpty;
            Life5.GetComponent<SpriteRenderer>().sprite = spriteRendererEmpty;
        } else if (playerHealth.health == 1) {
            Life1.GetComponent<SpriteRenderer>().sprite = spriteRendererFull;
            Life2.GetComponent<SpriteRenderer>().sprite = spriteRendererEmpty;
            Life3.GetComponent<SpriteRenderer>().sprite = spriteRendererEmpty;
            Life4.GetComponent<SpriteRenderer>().sprite = spriteRendererEmpty;
            Life5.GetComponent<SpriteRenderer>().sprite = spriteRendererEmpty;
        } else if (playerHealth.health == 0) {
            Life1.GetComponent<SpriteRenderer>().sprite = spriteRendererEmpty;
            Life2.GetComponent<SpriteRenderer>().sprite = spriteRendererEmpty;
            Life3.GetComponent<SpriteRenderer>().sprite = spriteRendererEmpty;
            Life4.GetComponent<SpriteRenderer>().sprite = spriteRendererEmpty;
            Life5.GetComponent<SpriteRenderer>().sprite = spriteRendererEmpty;
        }
    }
}
