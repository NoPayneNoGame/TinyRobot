using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {
  public float health;
  public float damageTakenPerHit;

  void die() {
    Destroy(gameObject);
  }

  void Start() {

  }

  void OnCollisionEnter(Collision hit) {
    if (hit.gameObject.tag == "PlayerProjectile") {
      health -= damageTakenPerHit;
    }
  }

  void Update() {
    if (health <= 0) die();
  }
}
