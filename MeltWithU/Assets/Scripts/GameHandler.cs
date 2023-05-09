using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameHandler : MonoBehaviour {

      private GameObject player;
      public static float playerHealth = 100;
      public float StartPlayerHealth = 100;
      public Image healthBar;
      public GameObject dyingMessage;

      public static int gotTokens = 0;
      public GameObject tokensText;

      public bool isDefending = false;

      public static bool stairCaseUnlocked = false;
      //this is a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true;

      private string sceneName;
      public static bool GameisPaused = false;
      public GameObject pauseMenuUI;
      public AudioMixer mixer;
      public static float volumeLevel = 1.0f;
      private Slider sliderVolumeCtrl;


//       public GameObject healthText;
//       public Color healthyColor = new Color(0.3f, 0.8f, 0.3f);
//       public Color unhealthyColor = new Color(0.8f, 0.3f, 0.3f);

      void Awake (){
              SetLevel (volumeLevel);
              GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
              if (sliderTemp != null){
                      sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
                      sliderVolumeCtrl.value = volumeLevel;
              }
      }

      void Start(){
            pauseMenuUI.SetActive(false);
            GameisPaused = false;
            player = GameObject.FindWithTag("Player");
            sceneName = SceneManager.GetActiveScene().name;
            //if (sceneName=="MainMenu"){ //uncomment these two lines when the MainMenu exists
                  playerHealth = StartPlayerHealth;
            //}
            // updateStatsDisplay();
            if (PlayerPrefs.HasKey("PlayerDied")) {
                  healthBar.fillAmount = 1;
                  PlayerPrefs.SetFloat("PlayerHealth", 1f);
                  PlayerPrefs.DeleteKey("PlayerDied");
            }
            else if (PlayerPrefs.HasKey("PlayerHealth")) {
                  Debug.Log("player health is " + PlayerPrefs.GetFloat("PlayerHealth"));
                  healthBar.fillAmount = PlayerPrefs.GetFloat("PlayerHealth");
                  playerHealth = PlayerPrefs.GetFloat("PlayerHealth") * 100;
            }
      }

      void Update(){
            if (Input.GetKeyDown(KeyCode.Escape)){
                    if (GameisPaused){
                            Resume();
                    }
                    else{
                            Pause();
                    }
            }
      }

      void Pause(){
              pauseMenuUI.SetActive(true);
              Time.timeScale = 0f;
              GameisPaused = true;
      }

      public void Resume(){
              pauseMenuUI.SetActive(false);
              Time.timeScale = 1f;
              GameisPaused = false;
      }

      public void SetLevel (float sliderValue){
              mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
              volumeLevel = sliderValue;
      }

      public void playerGetTokens(int newTokens){
            gotTokens += newTokens;
            updateStatsDisplay();
      }

      public void playerGetHit(int damage){
           if (isDefending == false){
                  playerHealth -= damage;
                  if (playerHealth >=0){
                        updateStatsDisplay();
                  }
                  if (damage > 0){
                        //player.GetComponent<PlayerHurt>().playerHit();       //play GetHit animation
                  }
            }

           if (playerHealth > StartPlayerHealth){
                  playerHealth = StartPlayerHealth;
                  updateStatsDisplay();
            }

           if (playerHealth <= 0){
                  playerHealth = 0;
                  updateStatsDisplay();
                  playerDies();
            }
      }

      public void SetColor(Color newColor){
            healthBar.GetComponent<Image>().color = newColor;
      }

      public void updateStatsDisplay(){
           
            healthBar.fillAmount = playerHealth / StartPlayerHealth;
            PlayerPrefs.SetFloat("PlayerHealth", healthBar.fillAmount);
            // healthBar.fillAmount = playerHealth / StartPlayerHealth;
//             //turn red at low health:
//             if (playerHealth < 0.3f){
//                 SetColor(unhealthyColor);
//             } else {
//                 SetColor(healthyColor);
//             }
            //Text healthTextTemp = healthText.GetComponent<Text>();
            //healthTextTemp.text = "HEALTH: " + playerHealth;

            // Text tokensTextTemp = tokensText.GetComponent<Text>();
            // tokensTextTemp.text = "GOLD: " + gotTokens;
      }

      public void playerDies(){
            // player.GetComponent<PlayerHurt>().playerDead();       //play Death animation
            StartCoroutine(DeathPause());
      }

      IEnumerator DeathPause(){
            player.GetComponent<PlayerMove>().isAlive = false;
            player.GetComponent<PlayerJump>().isAlive = false;
            yield return new WaitForSeconds(1.0f);
            player.SetActive(false);
            dyingMessage.SetActive(true);
            PlayerPrefs.SetInt("PlayerDied", 1);
      }

      public void StartGame() {
            SceneManager.LoadScene("Level1");
      }

      public void RestartGame() {
            Time.timeScale = 1f;
            Debug.Log("before loading scene: " + playerHealth);
            SceneManager.LoadScene(sceneName);
                // Please also reset all static variables here, for new games!
            // playerHealth = StartPlayerHealth;
      }

      public void ReturntoKitchen() {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Lobby");
                // Please also reset all static variables here, for new games!
            //playerHealth = StartPlayerHealth;
      }

      public void QuitGame() {
                PlayerPrefs.DeleteAll();
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
      }

      public void Credits() {
            SceneManager.LoadScene("Credits");
      }
}