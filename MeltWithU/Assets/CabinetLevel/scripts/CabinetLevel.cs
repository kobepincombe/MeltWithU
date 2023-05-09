using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetLevel : MonoBehaviour
{
    public GameObject deadMessage;
    public GameObject instructions;
    public Transform bottomRight;
    public Transform topLeft;
    private Transform playerT;
    private CapsuleCollider2D playerCollider;
    public GameObject SKey;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fadeOut());
        playerT = GetComponent<Transform>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerT.position.x >= bottomRight.position.x || playerT.position.x <= topLeft.position.x || 
            playerT.position.y <= bottomRight.position.y || playerT.position.y >= topLeft.position.y)  {
                //Debug.Log("out of range");
                playerCollider.enabled = true;
                return;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "mouse") {
            Destroy(gameObject);
            if (instructions.activeSelf) {
                instructions.SetActive(false);
            }
            deadMessage.SetActive(true);
        }
        if (collision.gameObject.name == "Butter Variant") {
            SKey.SetActive(true);
        }
    }

    IEnumerator fadeOut() {
        yield return new WaitForSeconds(2);
        instructions.SetActive(false);
    }
}
