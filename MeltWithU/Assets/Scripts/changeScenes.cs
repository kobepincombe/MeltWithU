using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScenes : MonoBehaviour
{
    [SerializeField] private LayerMask playermask;
    public string scene;
    public Transform enterOrExit;
    public Vector2 spawnPosition;
    public bool useSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        if (Input.GetKeyDown(KeyCode.E)) {
            Collider2D collider = Physics2D.OverlapCircle(enterOrExit.position, 0.4f, playermask);
            Debug.Log(collider);
            if (collider != null && collider.tag == "Player") {
                changeSceneTo(scene);
            }
        }
    }

    public void changeSceneTo(string sceneChange) {
        if (useSpawnPoint) {
            PlayerPrefs.SetFloat("spawnPosX", spawnPosition.x);
            PlayerPrefs.SetFloat("spawnPosY", spawnPosition.y);
        }
        SceneManager.LoadScene(sceneChange);
    }
}
