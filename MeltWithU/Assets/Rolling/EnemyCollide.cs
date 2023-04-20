using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollide : MonoBehaviour
{
    public GameObject deadMessage;
    public bool isDead = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "enemyRoll")
        {
            // Stop the player from moving
            isDead = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            // Set isAlive to false
            gameObject.GetComponent<PlayerRollJump>().isAlive = false;
            // Show the dead message
            deadMessage.SetActive(true);

        }
    }

    void Update() {
        if (isDead && Input.GetKeyDown(KeyCode.Space)) {
            RestartScene();
        }
    }

    public void RestartScene() {
        isDead = false;
        gameObject.GetComponent<PlayerRollJump>().isAlive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
