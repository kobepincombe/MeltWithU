using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandardPlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask playermask;
    public GameObject feet;
    public GameObject head;
    private Rigidbody2D rigidbodycomponent;
    private Transform headTransform;
    private bool jumpkeywaspressed;
    private float horizontalinput;
    private bool upinput = false;
    private bool downinput = false;
    private Transform feetTransform;
    private bool SkeyWasPressed;
    private Transform playerT;
    private CapsuleCollider2D playerCollider;
    private bool passThrough; //a boolean that records if the player is falling through a platform

    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public LayerMask silverwareLayer;
    public bool canJump = false;
    public int jumpTimes = 0;
    public static float runSpeed = 10f;
    public float jumpForce = 5f;
    private Vector3 hMove;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodycomponent = GetComponent<Rigidbody2D>();
        feetTransform = feet.GetComponent<Transform>();
        headTransform = head.GetComponent<Transform>();
        SkeyWasPressed = false;
        passThrough = false;
        playerT = GetComponent<Transform>();
        playerCollider = GetComponent<CapsuleCollider2D>();

        if (PlayerPrefs.HasKey("spawnPosX") && PlayerPrefs.HasKey("spawnPosY")) {
            playerT.position = new Vector3(PlayerPrefs.GetFloat("spawnPosX"),
                                            PlayerPrefs.GetFloat("spawnPosY"),
                                            0);
            StartCoroutine(deletingSpawnPoints());
        }
    }

    IEnumerator deletingSpawnPoints() {
        yield return new WaitForSeconds(1);
        PlayerPrefs.DeleteKey("spawnPosX");
        PlayerPrefs.DeleteKey("spawnPosY");
    }

    // Update is called once per frame
    void Update()
    {

        upinput = (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space));
        downinput = (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow));

        if ((IsGrounded()) || (jumpTimes < 1)){
              canJump = true;
        }  else if (jumpTimes >= 1){
              canJump = false;
        }

        if (upinput && canJump) {
            jumpkeywaspressed = true;
        }

        if (downinput) {
            SkeyWasPressed = true;
        }


        horizontalinput = Input.GetAxis("Horizontal");

        if (horizontalinput < 0) {
            playerT.eulerAngles = new Vector3 (0, 180, 0);
        } else if (horizontalinput > 0) {
            playerT.eulerAngles = new Vector3 (0, 0, 0);
        }
    }

    public bool IsGrounded() {
        Collider2D groundCheck = Physics2D.OverlapCircle(feetTransform.position, 1f, groundLayer);
        Collider2D enemyCheck = Physics2D.OverlapCircle(feetTransform.position, 1f, enemyLayer);
        Collider2D silverwareCheck = Physics2D.OverlapCircle(feetTransform.position, 1f, silverwareLayer);
        if ((groundCheck != null) || (enemyCheck != null) || (silverwareCheck != null)) {
            //Debug.Log("I am trouching ground!");
            jumpTimes = 0;
            return true;
        }
        return false;
    }

    void FixedUpdate() {

        //slow down on hills / stops sliding from velocity
        hMove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        if (hMove.x == 0){
            rigidbodycomponent.velocity = new Vector2(rigidbodycomponent.velocity.x / 1.1f, rigidbodycomponent.velocity.y) ;
        }

        rigidbodycomponent.velocity = new Vector2(horizontalinput * runSpeed, rigidbodycomponent.velocity.y);

        // if the player's collider is turned off and the player is falling from jumping, then turn player collider on
        // passthrogh : a boolean that records if the player is falling through a platform
        if ((!passThrough && rigidbodycomponent.velocity.y <= 0.1f) || (passThrough && rigidbodycomponent.velocity.y < -6.5f)
            && !playerCollider.enabled) {

            if (passThrough) {
                passThrough = false;
            }
            playerCollider.enabled = true;
        }

        // if the player's collider is turned on and the player jumps upwards and collides with a passThrough object
        Collider2D collision = Physics2D.OverlapCircle(headTransform.position, 0.5f, playermask);
        if (collision != null && collision.tag == "passThrough" && rigidbodycomponent.velocity.y > 0) {
            playerCollider.enabled = false;
        }

        // if the player presses the s key and the player is on a pass through collider
        Collider2D feetCollision = Physics2D.OverlapCircle(feetTransform.position, 0.5f, playermask);
        if (SkeyWasPressed && (feetCollision != null) && feetCollision.tag == "passThrough") {

            passThrough = true;
            playerCollider.enabled = false;
        }

        if (SkeyWasPressed) {
            SkeyWasPressed = false;
        }

        if (jumpkeywaspressed) {
             jumpkeywaspressed = false;
             rigidbodycomponent.velocity = Vector2.up * jumpForce;
             jumpTimes += 1;
        }

        //if character is in the air or passing through a platform then cant jump
        if (!playerCollider.enabled || Physics2D.OverlapCircle(feetTransform.position, 0.2f, playermask) == null) {
            //Debug.Log("in the air or passing through a platform");
            return;
        }
    }
}
