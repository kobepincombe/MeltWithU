using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuddyExit : MonoBehaviour
{
    public GameObject SKey;
    public string buddyName;

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == buddyName) {
            SKey.SetActive(true);
        }
    }

}
