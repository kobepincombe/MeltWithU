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
    public AudioSource BounceSFX;

    // The direction to launch the object in
    public Vector3 launchDirection = new Vector3(1, 2);

    void Start()
    {
        //anim = gameObject.GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canBounce())
        {
            bounceCan = true;
        }

        if (bounceCan)
        {
            bounce();
        }
    }

    public void bounce()
    {
        BounceSFX.Play();
        if (rb != null)
        {
            Vector3 direction = launchDirection;
            if (rb.velocity.x < 0) // If player is moving in -x direction, flip the launch direction
            {
                direction.x *= -1;
            }
            Vector3 launchForce = direction.normalized * bounceForce;
            rb.AddForce(launchForce, ForceMode2D.Impulse);
        }
        bounceCan = false;
        StartCoroutine(ResetBounce());
    }

    IEnumerator ResetBounce()
    {
        yield return new WaitForSeconds(0.2f); // Wait for 0.2 seconds
    }

    public bool canBounce()
    {
        Collider2D bounceCheck = Physics2D.OverlapCircle(feet.position, 0.5f, bounceLayer);
        if (bounceCheck != null)
        {
            Debug.Log("Bounce!");
            return true;
        }
        return false;
    }
}
