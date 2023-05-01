using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheeseBuddy : MonoBehaviour
{
    public float cheeseVelocity;
    public float minDistance;
    private Transform cheeseT;
    private Rigidbody2D cheeserb;
    private GameObject cheeseParent;
    private Animator cheeseAnimator;
    // Start is called before the first frame update
    void Start()
    {
        cheeseT = GetComponent<Transform>();
        cheeserb = GetComponent<Rigidbody2D>();
        cheeseAnimator = GetComponent<Animator>();
        cheeseParent = null;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D (Collision2D collision) {
        if (cheeseT.parent == null && collision.gameObject.tag == "Player") {
            cheeserb.gravityScale = 0;
            cheeserb.bodyType = RigidbodyType2D.Kinematic;
            cheeserb.GetComponent<BoxCollider2D>().enabled = false;
            cheeseParent = collision.gameObject.GetComponent<Transform>().GetChild(0).gameObject;
            cheeseAnimator.SetBool("saved", true);
            // Debug.Log("after colliding, cheese name is: " + gameObject.name);
            if (!PlayerPrefs.HasKey(gameObject.name)) {
                PlayerPrefs.SetString(gameObject.name, "saved");
            } 
        }
    }

    void FixedUpdate() {

        if (cheeseParent != null) {
            //Find direction
            Vector3 dir = (cheeseParent.transform.position - cheeserb.transform.position).normalized;
            //Check if we need to follow object then do so 
            if (Vector3.Distance(cheeseParent.transform.position, cheeserb.transform.position) > minDistance)
            {
                cheeserb.MovePosition(cheeserb.transform.position + dir  * cheeseVelocity * Time.fixedDeltaTime);
            } else {
               
                cheeserb.velocity = new Vector3(0, 0, 0);
            }
        }
    }
}
