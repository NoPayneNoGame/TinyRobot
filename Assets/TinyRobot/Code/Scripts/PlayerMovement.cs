using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float speed;
  public float jumpSpeed;
  public Rigidbody rb;
  private enum state
  {
    ready,
    jumping,
    doubleJumping,
  }

  private void jump()
  {
    rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
  }

  void Update()
  {
    //float vertical = Input.GetAxisRaw("Vertical");
    float horizontal = Input.GetAxisRaw("Horizontal");

    // FIXME: Capsule falls off the platform when you move close to the edge.
    gameObject.transform.position = new Vector2(transform.position.x + (horizontal * speed), transform.position.y);

    if (Input.GetKeyDown(KeyCode.Space))
    {
      jump();
    }
  }
}
