using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseSpawner : MonoBehaviour
{
    [System.Serializable] public struct spawner {
        public Transform spawnPos;
        public int limit;
        private int count;
        public int speed;

        public spawner(Transform spawnPoint, int limit, int speed) {
            Debug.Log("creating struct");
            this.spawnPos = spawnPoint;
            this.limit = limit;
            this.count = 0;
            this.speed = speed;
        }

        public bool isValid() {
            return this.limit > this.count;
        }

        public void increaseCount() {
            this.count++;
        }

    }

    public List<spawner> spawnpoints;
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
            // Debug.Log("choosing spawn index "+ spawnIndex + " current count is: " + spawnpoints.Count);
            if (spawnpoints.Count > 0 && spawnpoints[spawnIndex].isValid()) {
                spawner spawn = spawnpoints[spawnIndex];
                Collider2D block = Physics2D.OverlapCircle(spawn.spawnPos.position, 0.1f, holeMask);
                float random = (float) Random.Range(0.4f, maxTime);
                if (block == null) {
                    spawnMouse(spawn, spawnIndex);
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

    void spawnMouse(spawner spawn, int spawnIndex) {
        spawn.increaseCount();
        mouse.GetComponent<mouse>().speedScale = spawn.speed;
        Instantiate(mouse, spawn.spawnPos.position, Quaternion.identity);
        spawnpoints[spawnIndex] = spawn;
    }
}
