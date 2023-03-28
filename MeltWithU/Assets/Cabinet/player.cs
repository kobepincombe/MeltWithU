using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private LayerMask playermask;
    public GameObject feet;
    public GameObject head;
    private Rigidbody2D rigidbodycomponent;
    private Transform headTransform;
    private bool jumpkeywaspressed;
    private float horizontalinput;
    private Transform feetTransform;
    private bool passThrough;
    private bool passThroughBottom;
    private bool SkeyWasPressed;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodycomponent = GetComponent<Rigidbody2D>();
        feetTransform = feet.GetComponent<Transform>();
        headTransform = head.GetComponent<Transform>();
        passThrough = false;
        passThroughBottom = false;
        SkeyWasPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("space pressed");
            jumpkeywaspressed = true;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            Debug.Log("s pressed");
            SkeyWasPressed = true;
        }
        horizontalinput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate() {

        rigidbodycomponent.velocity = new Vector2(horizontalinput * 5, rigidbodycomponent.velocity.y);

        if (passThrough && rigidbodycomponent.velocity.y < 0) {
            passThrough = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }

        Collider2D collision = Physics2D.OverlapCircle(headTransform.position, 0.1f, playermask);
        if (!passThrough && collision != null && collision.tag == "passThrough" && rigidbodycomponent.velocity.y > 0) {
            GetComponent<BoxCollider2D>().enabled = false;
            passThrough = true;
        } 


        Collider2D feetCollision = Physics2D.OverlapCircle(feetTransform.position, 0.1f, playermask);
        if (SkeyWasPressed && feetCollision != null && feetCollision.tag == "passThrough" && !passThroughBottom) {
            passThroughBottom = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }

        if (SkeyWasPressed) {
            SkeyWasPressed = false;
        }

        Collider2D passThroughCollision = Physics2D.OverlapCircle(feetTransform.position, 0.4f, playermask);
        if (passThroughBottom && passThroughCollision == null) {
            Debug.Log("pass through bottom collider enabled");
            passThroughBottom = false;
            GetComponent<BoxCollider2D>().enabled = true;
        } 

        //if character is in the air or passing through a platform then cant jump 
        if (passThroughBottom || passThrough || Physics2D.OverlapCircle(feetTransform.position, 0.1f, playermask) == null) {
            return;
        }

        if (jumpkeywaspressed) {
             jumpkeywaspressed = false;
             rigidbodycomponent.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
        }

    }

}
