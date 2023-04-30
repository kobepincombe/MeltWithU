using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHit : MonoBehaviour
{
    public Image playerHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        
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
            if (playerHealthBar.fillAmount <= 0f) {
                Destroy(gameObject);
            }
        }
    }
}
