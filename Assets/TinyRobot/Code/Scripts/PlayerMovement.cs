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
    falling,
  }
  private state currentState = state.ready;
  private bool spacePressed = false;

  private void jump()
  {
    switch (currentState)
    {
      case state.ready:
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        currentState = state.jumping;
        break;
      case state.jumping:
      case state.falling:
        if (!spacePressed)
        {
          currentState = state.doubleJumping;
          // FIXME: Add flutter and remove downward force
          rb.AddForce(Vector3.up * (jumpSpeed / 2), ForceMode.Impulse);
        }
        break;
    }
  }

  private void teleport(Vector3 target)
  {
    gameObject.transform.position = target;
  }

  private void move(float horizontalAxis)
  {
    // FIXME: Should probably use forces/velocity instead of just teleporting the player a little left/right.
    teleport(new Vector3(transform.position.x + (horizontalAxis * speed), transform.position.y, transform.position.z));
  }

  void OnCollisionEnter(Collision hit)
  {
    if (hit.gameObject.tag == "Floor")
    {
      currentState = state.ready;
    }
  }

  void OnCollisionExit(Collision hit)
  {
    if (hit.gameObject.tag == "Floor")
    {
      if (currentState == state.ready)
      {
        currentState = state.falling;
      }
    }
  }


  void Update()
  {
    float horizontal = Input.GetAxisRaw("Horizontal");

    move(horizontal);

    if (Input.GetKeyDown(KeyCode.Space))
    {
      jump();
      spacePressed = true;
    }

    if (Input.GetKeyUp(KeyCode.Space))
    {
      spacePressed = false;
    }
  }
}
