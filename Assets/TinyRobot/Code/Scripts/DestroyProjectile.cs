using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour {
  public float timeToLive = 0.5f;
  void Start() {
    Destroy(gameObject, timeToLive);
  }

  void OnCollisionEnter() {
    Destroy(gameObject);
  }
}
