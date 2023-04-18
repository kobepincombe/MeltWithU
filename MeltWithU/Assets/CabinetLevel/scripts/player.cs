using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    [SerializeField] private LayerMask playermask;
    public GameObject deadMessage;
    public GameObject instructions;
    public GameObject feet;
    public GameObject head;
    public Transform bottomRight;
    public Transform topLeft;
    private Rigidbody2D rigidbodycomponent;
    private Transform headTransform;
    private bool jumpkeywaspressed;
    private float horizontalinput;
    private Transform feetTransform;
    private bool SkeyWasPressed;
    private Transform playerT;
    private BoxCollider2D playerCollider;
    private bool passThrough;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodycomponent = GetComponent<Rigidbody2D>();
        feetTransform = feet.GetComponent<Transform>();
        headTransform = head.GetComponent<Transform>();
        SkeyWasPressed = false;
        passThrough = false;
        playerT = GetComponent<Transform>();
        playerCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(fadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            jumpkeywaspressed = true;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            Debug.Log("s pressed");
            SkeyWasPressed = true;
        }

        horizontalinput = Input.GetAxis("Horizontal");
        
        if (horizontalinput < 0) {
            playerT.eulerAngles = new Vector3 (0, 180, 0);
        } else if (horizontalinput > 0) {
            playerT.eulerAngles = new Vector3 (0, 0, 0);
        }
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
        if ((!passThrough && rigidbodycomponent.velocity.y <= 0.1f) || (passThrough && rigidbodycomponent.velocity.y < -6.5f) 
            && !playerCollider.enabled) {
            if (passThrough) {
                passThrough = false;
            }
            playerCollider.enabled = true;
        }

        // if the player's collider is turned on and the player jumps upwards and collides with a passThrough object
        Collider2D collision = Physics2D.OverlapCircle(headTransform.position, 0.1f, playermask);
        if (collision != null && collision.tag == "passThrough" && rigidbodycomponent.velocity.y > 0) {
            playerCollider.enabled = false;
        } 

        // if the player presses the s key and the player is on a pass through collider
        Collider2D feetCollision = Physics2D.OverlapCircle(feetTransform.position, 0.2f, playermask);
        if (SkeyWasPressed && feetCollision != null && feetCollision.tag == "passThrough") {

            passThrough = true;
            playerCollider.enabled = false;
        }

        if (SkeyWasPressed) {
            SkeyWasPressed = false;
        } 

        //if character is in the air or passing through a platform then cant jump 
        if (!playerCollider.enabled || Physics2D.OverlapCircle(feetTransform.position, 0.2f, playermask) == null) {
            return;
        }

        // jump
        if (jumpkeywaspressed) {
             jumpkeywaspressed = false;
             rigidbodycomponent.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
        }

    }

    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "mouse") {
            Destroy(gameObject);
            deadMessage.SetActive(true);
        }
    }

    IEnumerator fadeOut() {
        yield return new WaitForSeconds(2.5f);
        instructions.SetActive(false);
    }

}
