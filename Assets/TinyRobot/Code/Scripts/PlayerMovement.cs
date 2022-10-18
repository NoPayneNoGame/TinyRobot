using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
  public float speed;

  void Update() {
    //float vertical = Input.GetAxisRaw("Vertical");
    float horizontal = Input.GetAxisRaw("Horizontal");

    gameObject.transform.position = new Vector2(transform.position.x + (horizontal * speed), transform.position.y);
  }
}
