using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowPlayer : MonoBehaviour
{
    public Transform playerT;
    private Transform cameraT;
    // Start is called before the first frame update
    void Start()
    {
        cameraT = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerT != null) {
            cameraT.position = new Vector3 (playerT.position.x, playerT.position.y, playerT.position.z - 50);
        }
    }
}
