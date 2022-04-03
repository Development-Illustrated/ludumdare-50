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

  private Animator animator;
  private Direction lastDirection;

  private float prevMaxSpeed;
  private float currentSpeed;

  [Header("Attributes")]
  [SerializeField] private float accelerationSpeed = 3f;
  [SerializeField] private float maximumSpeed;
  void Awake()
  {
    rb = gameObject.GetComponent<Rigidbody2D>();
    animator = gameObject.GetComponentInChildren<Animator>();
  }

  void Start()
  {
    animator.SetBool("isIdle", true);
  }

  public void HandleMovement(InputAction.CallbackContext context)
  {
    moveVector = context.ReadValue<Vector2>();
  }

  void FixedUpdate()
  {
    FlipSprite();
    UpdateAnimator();
    rb.AddForce(new Vector2(moveVector.x * accelerationSpeed, moveVector.y * accelerationSpeed), ForceMode2D.Impulse);
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

  private void UpdateAnimator()
  {
    float fuck = Mathf.Abs(rb.velocity.x);
    prevMaxSpeed = fuck;

    if (fuck > 0 && fuck < 5)
    {
      animator.SetBool("isIdle", false);
      animator.SetBool("isAccelerating", true);
    }

    if (fuck > 5)
    {
      animator.SetBool("isAccelerating", false);
      animator.SetBool("isMaxSpeed", true);
    }

    if (fuck < 1)
    {
      animator.SetBool("isAccelerating", false);
      // animator.SetBool("isDecelerating", false);
      animator.SetBool("isMaxSpeed", false);
      animator.SetBool("isIdle", true);
    }
  }
}
