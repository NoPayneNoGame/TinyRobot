using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float speed;
  public float jumpSpeed;
  public float doubleJumpSpeed;
  public int doubleJumpLength;
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
  public int doubleJumpTimer = 0;

  private void jump()
  {
    switch (currentState)
    {
      case state.ready:
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        currentState = state.jumping;
        break;
      case state.jumping:
        if (!spacePressed)
        {
          currentState = state.doubleJumping;
          haltVertical();
          doubleJumpTimer = doubleJumpLength;
          doubleJump();
        }
        break;
    }
  }

  private void doubleJump()
  {
    // FIXME: Ramp is very inconsistent based on double jump length also I originally wanted it to dip a little at the beginning
    int ramp = (doubleJumpLength - doubleJumpTimer) / 20;
    rb.AddForce(Vector3.up * doubleJumpSpeed * ramp, ForceMode.Force);
  }

  private void teleport(Vector3 target)
  {
    gameObject.transform.position = target;
  }

  private void moveHorizontal(float horizontalAxis)
  {
    // FIXME: Player moves faster horizontally in the air
    Vector3 movementDirection = new Vector3((horizontalAxis * speed), 0, 0);
    rb.AddForce(movementDirection, ForceMode.Force);
  }

  private void haltHorizontal()
  {
    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
  }

  private void haltVertical()
  {
    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
  }

  private void haltUpwardMomentum()
  {
    if (rb.velocity.y > 0) haltVertical();
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
        currentState = state.jumping; // Set to jumping to allow double jump after "falling" off a foor
      }
    }
  }

  void Update()
  {
    float horizontal = Input.GetAxisRaw("Horizontal");

    if (horizontal != 0)
    {
      moveHorizontal(horizontal);
    }
    else
    {
      haltHorizontal();
    }

    if (Input.GetKeyDown(KeyCode.Space))
    {
      jump();
      spacePressed = true;
    }

    if (doubleJumpTimer > 0)
    {
      doubleJumpTimer--;
      doubleJump();
    }

    if (Input.GetKeyUp(KeyCode.Space))
    {
      spacePressed = false;
      if (currentState != state.falling)
      {
        // This allows for smaller than maximum jumps, not sure if we want this
        haltUpwardMomentum();
      }
      if (currentState == state.doubleJumping)
      {
        doubleJumpTimer = 0;
        currentState = state.falling;
      }
    }
  }
}
