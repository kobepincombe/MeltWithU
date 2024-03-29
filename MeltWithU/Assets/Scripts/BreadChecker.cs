using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreadChecker : MonoBehaviour
{
    public GameObject speechBubble;

    void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            checkBuddies();
        }
    }

    void checkBuddies() {

        if (GameObject.FindGameObjectsWithTag("buddies").Length == 5) {
            SceneManager.LoadScene("WinScene");
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
