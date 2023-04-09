using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseSpawner : MonoBehaviour
{
    public Transform[] spawnpoints;
    [SerializeField] private LayerMask holeMask;
    public GameObject mouse;
    private int length;
    private bool spawn;
    private int[] amntSpawned;
    public float maxTime;
    // Start is called before the first frame update
    void Start()
    {
        length = spawnpoints.Length;
        spawn = true;
        Physics2D.IgnoreLayerCollision(8, 8, true);
        amntSpawned = new int [length];
        for (int i = 0; i < length; i++) {
            amntSpawned[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn) {
            int spawnIndex = Random.Range(0, length);
            spawn = false;
            Debug.Log("choosing spawn index "+ spawnIndex);
            Collider2D block = Physics2D.OverlapCircle(spawnpoints[spawnIndex].position, 0.2f, holeMask);
            if (block == null) {
                Debug.Log("spawning");
                amntSpawned[spawnIndex]++;
                Instantiate(mouse, spawnpoints[spawnIndex].position, Quaternion.identity);
                float random = (float) Random.Range(0.4f, maxTime);
                StartCoroutine(spawnMouse(random));
                if (maxTime > 2f) {
                    maxTime = maxTime - 1f;
                }
            }
        }
    }

    IEnumerator spawnMouse(float time) {
        yield return new WaitForSeconds(time);
        spawn = true;
    }
}
