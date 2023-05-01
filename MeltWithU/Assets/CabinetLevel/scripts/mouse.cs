using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    [SerializeField] private LayerMask playermask;
    public int speedScale;
    public float damage;
    private Transform mouseTransform;
    private Rigidbody2D mouserigidbody;
    public Transform head;
    public Transform butt;
    private bool gonnaDie;
    private Transform mouseArt;
    // Start is called before the first frame update
    void Start()
    {
        mouseTransform = GetComponent<Transform>();
        mouserigidbody = GetComponent<Rigidbody2D>();
        gonnaDie = false;
        mouserigidbody.velocity = new Vector3(-1 * speedScale, 0, 0);
        mouseArt = mouseTransform.GetChild(2).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalinput = mouserigidbody.velocity.normalized.x;
        if (horizontalinput < 0) {
            mouseArt.eulerAngles = new Vector3 (0, 180, 0);
        } else {
            mouseArt.eulerAngles = new Vector3 (0, 0, 0);
        }
        Collider2D headcollision = Physics2D.OverlapCircle(head.position, 0.2f, playermask);
        Collider2D buttcollision = Physics2D.OverlapCircle(butt.position, 0.2f, playermask);
        
        if (!gonnaDie && headcollision != null && buttcollision != null) {
            Debug.Log("setting dying to true");
            gonnaDie = true;
            mouserigidbody.velocity = new Vector3(0, 0, 0);
            StartCoroutine(goingtoDie());
        }

        if (gonnaDie && headcollision == null || buttcollision == null) {
            gonnaDie = false;
        }
        
        if (gonnaDie) {
            return;
        }
        
        Vector3 velocitydir = mouserigidbody.velocity.normalized * speedScale;
        //if the head collides, change direction
        if (headcollision != null) {
            velocitydir = new Vector3(speedScale, 0, 0);
        }  //if the butt collides, change direction
        if (buttcollision != null) {
            velocitydir = new Vector3(-1 * speedScale, 0, 0);
        }
        mouserigidbody.velocity = velocitydir;

    }

    IEnumerator goingtoDie() {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("gonna die");
        if (gonnaDie) {
            Destroy(gameObject);
        }
    }
}
