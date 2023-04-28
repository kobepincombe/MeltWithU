using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class friendsHandler : MonoBehaviour
{
    public Transform playerT;
    public List<GameObject> cheeseFriends;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnpoint;
        if (PlayerPrefs.HasKey("spawnPosX") && PlayerPrefs.HasKey("spawnPosY")) {
            spawnpoint = new Vector3(PlayerPrefs.GetFloat("spawnPosX"), PlayerPrefs.GetFloat("spawnPosY"), 0);
        } else {
            spawnpoint = playerT.position;
        }

        for (int i = 0; i < cheeseFriends.Capacity; i++) {
            //if the player has collected the friend
            if (PlayerPrefs.HasKey(cheeseFriends[i].name)) {
                Debug.Log(cheeseFriends[i].name);
                //if the friend is already in the current scene, move friend to player's position
                if (cheeseFriends[i].scene.IsValid()) {
                    cheeseFriends[i].transform.position = spawnpoint;
                } else {
                    // if the friend is from a different scene, instantiate the friend next to the player
                    Instantiate(cheeseFriends[i], spawnpoint, Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
