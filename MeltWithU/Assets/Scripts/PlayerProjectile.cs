using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour{

      public int damage = 50;
      public GameObject hitEffectAnim;
      public float SelfDestructTime = 4.0f;
      public float SelfDestructVFX = 0.5f;
      public SpriteRenderer projectileArt;
      private float SDTime;

      void Start(){
           projectileArt = GetComponentInChildren<SpriteRenderer>();
           SDTime = Time.time + SelfDestructTime;
           //selfDestruct();
      }

      void Update() {
           if (Time.time >= SDTime) {
                Destroy(gameObject);
           }
      }

      //if the bullet hits a collider, play the explosion animation, then destroy the effect and the bullet
      void OnTriggerEnter2D(Collider2D other){
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
                  //gameHandlerObj.playerGetHit(damage);
                  other.gameObject.GetComponent<EnemyMeleeDamage>().TakeDamage(damage);
            }
           if (other.gameObject.tag != "Player") {
                  GameObject animEffect = Instantiate (hitEffectAnim, transform.position, Quaternion.identity);
                  projectileArt.enabled = false;
                  //Destroy (animEffect, 0.5);
                  StartCoroutine(selfDestructHit(animEffect));
            }
      }

      IEnumerator selfDestructHit(GameObject VFX){
            yield return new WaitForSeconds(SelfDestructVFX);
            Destroy (VFX);
            Destroy (gameObject);
      }

      IEnumerator selfDestruct(){
            yield return new WaitForSeconds(SelfDestructTime);
            Destroy (gameObject);
      }
}