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
  private Hazzard currentHazzard;

  private float prevMaxSpeed;
  private float currentSpeed;

  [Header("Attributes")]
  [SerializeField] private float accelerationSpeed = 3f;
  [SerializeField] private float maximumSpeed;

  [Header("References")]
  [SerializeField] private Canvas yoMessage;
  void Awake()
  {
    rb = gameObject.GetComponent<Rigidbody2D>();
    animator = gameObject.GetComponentInChildren<Animator>();
    yoMessage.gameObject.SetActive(false);
  }

  void Start()
  {
    animator.SetBool("isIdle", true);
  }

  public void HandleMovement(InputAction.CallbackContext context)
  {
    moveVector = context.ReadValue<Vector2>();
  }

  public void HandleInteract(InputAction.CallbackContext context)
  {
    if (currentHazzard)
    {
      currentHazzard.SendMessage("ClearHazzard", null);
      yoMessage.gameObject.SetActive(false);
    }
  }

  public void OnTriggerEnter2D(Collider2D col)
  {
    if (col.gameObject.GetComponent<Hazzard>())
    {
      Hazzard yo = col.gameObject.GetComponent<Hazzard>();
      if (yo.enabled)
      {
        currentHazzard = col.gameObject.GetComponent<Hazzard>();
        yoMessage.gameObject.SetActive(true);
      }
    }
  }

  public void OnTriggerExit2D(Collider2D col)
  {
    currentHazzard = null;
    yoMessage.gameObject.SetActive(false);
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
    float AHHHH = Mathf.Abs(rb.velocity.x);
    prevMaxSpeed = AHHHH;

    if (AHHHH > 0 && AHHHH < 5)
    {
      animator.SetBool("isIdle", false);
      animator.SetBool("isAccelerating", true);
    }

    if (AHHHH > 5)
    {
      animator.SetBool("isAccelerating", false);
      animator.SetBool("isMaxSpeed", true);
    }

    if (AHHHH < 1)
    {
      animator.SetBool("isAccelerating", false);
      animator.SetBool("isMaxSpeed", false);
      animator.SetBool("isIdle", true);
    }
  }
}
