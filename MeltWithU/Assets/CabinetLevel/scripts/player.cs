using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private LayerMask playermask;
    public GameObject feet;
    public GameObject head;
    public Transform bottomRight;
    public Transform topLeft;
    private Rigidbody2D rigidbodycomponent;
    private Transform headTransform;
    private bool jumpkeywaspressed;
    private float horizontalinput;
    private Transform feetTransform;
    private bool passThrough;
    private bool passThroughBottom;
    private bool SkeyWasPressed;
    private Transform playerT;
    private BoxCollider2D playerCollider;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodycomponent = GetComponent<Rigidbody2D>();
        feetTransform = feet.GetComponent<Transform>();
        headTransform = head.GetComponent<Transform>();
        passThrough = false;
        passThroughBottom = false;
        SkeyWasPressed = false;
        playerT = GetComponent<Transform>();
        playerCollider = GetComponent<BoxCollider2D>();
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

        if (playerT.position.x >= bottomRight.position.x || playerT.position.x <= topLeft.position.x || 
            playerT.position.y <= bottomRight.position.y || playerT.position.y >= topLeft.position.y)  {
                //Debug.Log("out of range");
                playerCollider.enabled = true;
                return;
        }

        // if the player's collider is turned off and the player is falling from jumping, then turn player collider on
        if (passThrough && rigidbodycomponent.velocity.y < 0) {
            passThrough = false;
            playerCollider.enabled = true;
        }
        
        // if the player's collider is turned on and the player jumps upwards and collides with a passThrough object
        Collider2D collision = Physics2D.OverlapCircle(headTransform.position, 0.1f, playermask);
        if (!passThrough && collision != null && collision.tag == "passThrough" && rigidbodycomponent.velocity.y > 0) {
            playerCollider.enabled = false;
            passThrough = true;
        } 

        // if the player presses the s key and the player is on a pass through collider
        Collider2D feetCollision = Physics2D.OverlapCircle(feetTransform.position, 0.1f, playermask);
        if (SkeyWasPressed && feetCollision != null && feetCollision.tag == "passThrough" && !passThroughBottom) {
            passThroughBottom = true;
            playerCollider.enabled = false;
        }

        if (SkeyWasPressed) {
            SkeyWasPressed = false;
        }
        
        // if the player is falling through platform and is about to collide on another platform
        if (passThroughBottom && feetCollision != null && rigidbodycomponent.velocity.y < -8) {
            Debug.Log(rigidbodycomponent.velocity.y);
            Debug.Log("pass through bottom collider enabled");
            passThroughBottom = false;
            playerCollider.enabled = true;
        } 

        //if character is in the air or passing through a platform then cant jump 
        if (passThroughBottom || passThrough || Physics2D.OverlapCircle(feetTransform.position, 0.1f, playermask) == null) {
            return;
        }

        // jump
        if (jumpkeywaspressed) {
             jumpkeywaspressed = false;
             rigidbodycomponent.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
        }

    }

}
