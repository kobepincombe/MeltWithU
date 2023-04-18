using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    //public Animator anim;
    public Rigidbody2D rb;
    public float bounceForce = 5f;
    public float launchAngle = 45f;
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
            float radian = launchAngle * Mathf.Deg2Rad;
            Vector2 forceDirection = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized;
            Vector2 force = forceDirection * bounceForce;

            // Apply the force to the rigidbody
            rb.AddForce(force, ForceMode2D.Impulse);
            bounceCan = false;
      }

    public bool canBounce() {
            Collider2D bounceCheck = Physics2D.OverlapCircle(feet.position, .501f, bounceLayer);
            if ((bounceCheck != null)) {
                Debug.Log("Bounce!");
                return true;
            }
            return false;
    }
}
