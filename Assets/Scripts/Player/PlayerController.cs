using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  enum Direction
  {
    LEFT,
    RIGHT
  }
  private Vector2 moveVector;
  private Rigidbody2D rb;
  private Direction lastDirection;

  private SpriteRenderer spriteRenderer;

  [Header("Attributes")]
  [SerializeField] private float movementSpeed = 3f;
  void Awake()
  {
    rb = gameObject.GetComponent<Rigidbody2D>();
  }

  public void HandleMovement(InputAction.CallbackContext context)
  {
    moveVector = context.ReadValue<Vector2>();
  }

  void FixedUpdate()
  {
    FlipSprite();
    rb.AddForce(new Vector2(moveVector.x * movementSpeed, moveVector.y * movementSpeed), ForceMode2D.Impulse);
  }

  private void FlipSprite()
  {
    if (rb.velocity.x > 0)
    {
      lastDirection = Direction.RIGHT;
    }
    else if (rb.velocity.x < 0)
    {
      lastDirection = Direction.LEFT;
    }

    if (lastDirection == Direction.RIGHT)
    {
      transform.localScale = new Vector3(-1, 1, 0);
    }
    else if (lastDirection == Direction.LEFT)
    {
      transform.localScale = Vector3.one;
    }

  }
}
