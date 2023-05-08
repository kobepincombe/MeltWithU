using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseSpawner : MonoBehaviour
{
    private Transform spawnPos;
    public GameObject closed;
    public int limit;
    private int count;
    public int speed;
    public float damage;
    [SerializeField] private LayerMask holeMask;
    public GameObject mouse;
    private bool spawn;
    public float maxTime;

    // Start is called before the first frame update
    void Start()
    {
        spawn = true;
        Physics2D.IgnoreLayerCollision(8, 8, true);
        spawnPos = GetComponent<Transform>();

        if (PlayerPrefs.HasKey(gameObject.name)) {
            closeMouseHole();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn) {
            spawn = false;
            if (count < limit) {
                Collider2D block = Physics2D.OverlapCircle(spawnPos.position, 0.1f, holeMask);
                float random = (float) Random.Range(0.4f, maxTime);
                if (block == null) {
                    spawnMouse();
                    StartCoroutine(spawnCycle(random));
                } else {
                    closeMouseHole();
                }
                //if the hole is blocked once, then the hole no longer spawns
            } else {
                closeMouseHole();
            }
        }
    }

    void closeMouseHole() {
        closed.SetActive(true);
        PlayerPrefs.SetInt(gameObject.name, 1);
        spawn = false;
    }

    IEnumerator spawnCycle(float time) {
        yield return new WaitForSeconds(time);
        spawn = true;
    }

    void spawnMouse() {
        count++;
        mouse mouseScript = mouse.GetComponent<mouse>();
        mouseScript.speedScale = speed;
        mouseScript.damage = damage;
        Instantiate(mouse, spawnPos.position, Quaternion.identity);
    }
}
