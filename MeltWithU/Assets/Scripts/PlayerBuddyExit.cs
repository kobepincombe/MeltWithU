using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuddyExit : MonoBehaviour
{
    public GameObject SKey;

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "buddies") {
            SKey.SetActive(true);
        }
    }

}
