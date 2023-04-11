using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseSpawner : MonoBehaviour
{
    public List<Transform> spawnpoints;
    [SerializeField] private LayerMask holeMask;
    public GameObject mouse;
    private bool spawn;
    public float maxTime;
    // Start is called before the first frame update
    void Start()
    {
        spawn = true;
        Physics2D.IgnoreLayerCollision(8, 8, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn) {
            int spawnIndex = Random.Range(0, spawnpoints.Count);
            spawn = false;
            Debug.Log("choosing spawn index "+ spawnIndex + " current count is: " + spawnpoints.Count);
            if (spawnpoints.Count > 0) {
                Collider2D block = Physics2D.OverlapCircle(spawnpoints[spawnIndex].position, 0.1f, holeMask);
                float random = (float) Random.Range(0.4f, maxTime);
                if (block == null) {
                    Debug.Log("spawning");
                    Instantiate(mouse, spawnpoints[spawnIndex].position, Quaternion.identity);
                } else  {
                    spawnpoints.RemoveAt(spawnIndex);
                    if (maxTime > 2f) {
                        maxTime = maxTime - 1f;
                    }
                }
                StartCoroutine(spawnCycle(random));
            }
        }
    }

    IEnumerator spawnCycle(float time) {
        yield return new WaitForSeconds(time);
        spawn = true;
    }
}
