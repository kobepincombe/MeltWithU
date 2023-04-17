using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyCollide : MonoBehaviour
{
    public LayerMask enemyLayer;
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("hit");
        if (other.gameObject.layer == enemyLayer)
        {
            Debug.Log("dead");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
