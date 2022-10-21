using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {
  public Rigidbody rb;
  public float deathPlaneY;

  void die() {
    // FIXME: Player just respawns with no consequences for now
    respawn();
  }

  void respawn() {
    transform.position = new Vector3(0, 0, 1.5f);
  }

  void Start() {
    // Player health?
    // Starting position
  }

  void Update() {
    if (rb.position.y < deathPlaneY) die();
  }
}
