using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

      //public Animator anim;
      public Rigidbody2D rb;
      public float jumpForce = 5f;
      public Transform feet;
      public LayerMask groundLayer;
      public LayerMask enemyLayer;
      public LayerMask silverwareLayer;
      public bool canJump = false;
      public int jumpTimes = 0;
      public bool isAlive = true;
      //public AudioSource JumpSFX;

      void Start(){
            //anim = gameObject.GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
      }

     void Update() {
            if ((IsGrounded()) || (jumpTimes <= 1)){
                  canJump = true;
            }  else if (jumpTimes > 1){
                  canJump = false;
            }

           if ((Input.GetButtonDown("Jump")) && (canJump) && (isAlive == true)) {
                  Jump();
            }
      }

      public void Jump() {
            jumpTimes += 1;
            rb.velocity = Vector2.up * jumpForce;
            // anim.SetTrigger("Jump");
            // JumpSFX.Play();

            //Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
            //rb.velocity = movement;
      }

      public bool IsGrounded() {
            Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 2f, groundLayer);
            Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, 2f, enemyLayer);
            Collider2D silverwareCheck = Physics2D.OverlapCircle(feet.position, 2f, silverwareLayer);
            if ((groundCheck != null) || (enemyCheck != null) || (silverwareCheck != null)) {
                  //Debug.Log("I am trouching ground!");
                  jumpTimes = 0;
                  return true;
            }
            return false;
      }
}