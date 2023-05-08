using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadChecker : MonoBehaviour
{
    public GameObject speechBubble;

    void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            checkBuddies();
        }
    }

    void checkBuddies() {

        if (GameObject.FindGameObjectsWithTag("buddies").Length == 6) {
            Debug.Log("Final Cutscene");
        } else {
            StartCoroutine(SpeechActivator());
        }
    }

    IEnumerator SpeechActivator() {
        speechBubble.SetActive(true);
        yield return new WaitForSeconds(2f);
        speechBubble.SetActive(false);
    }

}
