using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instruction : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject instructions;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Instructions() {
        instructions.SetActive(!instructions.activeSelf);
    }
}
