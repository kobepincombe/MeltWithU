using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScenes : MonoBehaviour
{
    [SerializeField] private LayerMask playermask;
    public string scene;
    public Transform enterOrExit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            Collider2D collider = Physics2D.OverlapCircle(enterOrExit.position, 0.4f, playermask);
            Debug.Log(collider);
            if (collider != null && collider.tag == "Player") {
                changeSceneTo(scene);
            }
        }
    }

    public void changeSceneTo(string sceneChange) {
        SceneManager.LoadScene(sceneChange);
    }
}
