using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;

public class MouseHoleEnter : MonoBehaviour
{
      void OnCollisionEnter2D(Collision2D other){
         //other.name should equal the root of your Player object
         if (other.gameObject.tag == "Player") {
             //The scene number to load (in File->Build Settings)
             SceneManager.LoadScene ("MouseHoleScene");
         }
     }
}
