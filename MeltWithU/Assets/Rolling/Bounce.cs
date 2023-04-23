using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    //public Animator anim;
    public Rigidbody2D rb;
    public float bounceForce = 5f;
    public Transform feet;
    public LayerMask bounceLayer;
    public bool bounceCan = false;
    public bool isAlive = true;
    //public AudioSource JumpSFX;

    void Start(){
            //anim = gameObject.GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
      }

    void Update() {
        
        if ((canBounce())){
            bounceCan = true;
        }

        if (bounceCan) {
            bounce();
        }
      }

      public void bounce() {
        // rb.velocity = Vector2.up * bounceForce;
        // anim.SetTrigger("bounce");
        // BounceSFX.Play();
        Debug.Log("forceadded");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2(.5f, 3.0f).normalized * bounceForce;
        rb.AddForce(force, ForceMode2D.Impulse);
        Debug.Log("force:" + force);
        bounceCan = false;
        StartCoroutine(ResetBounce());
      }

    IEnumerator ResetBounce() {
        // Wait for 4 frames
        for (int i = 0; i < 4; i++) {
            yield return null;
        }
    }
    public bool canBounce() {
            Collider2D bounceCheck = Physics2D.OverlapCircle(feet.position, .5f, bounceLayer);
            if ((bounceCheck != null)) {
                Debug.Log("Bounce!");
                return true;
            }
            return false;
    }
}
