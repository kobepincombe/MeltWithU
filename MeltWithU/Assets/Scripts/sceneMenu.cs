using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneMenu : MonoBehaviour
{

    void Awake() {
        PlayerPrefs.DeleteAll();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void rollingScene() {
        SceneManager.LoadScene("RollingScene");
    }
    public void cabinetScene() {
        SceneManager.LoadScene("cabinet");
    }
    public void MouseHoleScene() {
        SceneManager.LoadScene("MouseHoleScene");
    }
     public void KitchenScene() {
        SceneManager.LoadScene("Lobby");
    }

    public void FridgeScene() {
        SceneManager.LoadScene("TestScene");
    }
    public void QuitGame() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
