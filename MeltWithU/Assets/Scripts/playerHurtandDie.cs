using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHurtandDie : MonoBehaviour
{
    public Image playerHealthBar;
    public Image dyingMessage;
    // Start is called before the first frame update
    void Start()
    {
        dyingMessage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "mouse") {
            GameObject mouse = col.gameObject;
            float damage = mouse.GetComponent<mouse>().damage;
            playerHealthBar.fillAmount = playerHealthBar.fillAmount - damage;
            PlayerPrefs.SetFloat("PlayerHealth", playerHealthBar.fillAmount);
            if (playerHealthBar.fillAmount <= 0f) {
                Destroy(gameObject);
                dyingMessage.gameObject.SetActive(true);
                PlayerPrefs.SetInt("PlayerDied", 1);
            }
        }
    }
}
