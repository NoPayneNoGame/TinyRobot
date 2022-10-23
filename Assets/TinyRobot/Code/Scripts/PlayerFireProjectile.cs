using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireProjectile : MonoBehaviour {
  public GameObject projectile;
  public float speed;
  public GameObject launchPosition;

  private Transform player;

  void Start() {
    player = gameObject.transform.Find("PlayerModel");
  }

  void Update() {
    if (Input.GetButtonDown("Fire1")) {
      GameObject bullet = Instantiate(projectile, launchPosition.transform.position, launchPosition.transform.rotation);
      Physics.IgnoreCollision(bullet.GetComponent<Collider>(), player.GetComponent<Collider>());
      bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(speed, 0, 0));
      // This doesn't work

    }

    // TODO: Despawn projectiles
  }
}