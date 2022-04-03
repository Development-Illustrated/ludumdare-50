using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum Direction
    {
        LEFT,
        RIGHT
    }

    [Header("Attributes")]
    [SerializeField] private float accelerationSpeed = 3f;
    [SerializeField] private float maximumSpeed;

    [Header("References")]
    [SerializeField] private Canvas messageCanvas;

    private Vector2 moveVector;
    private Rigidbody2D rb;
    private Animator animator;
    private Direction lastDirection;
    private float prevMaxSpeed;
    private float currentSpeed;
    private Hazzard currentHazardObject;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    void Start()
    {
        animator.SetBool("isIdle", true);
        messageCanvas.gameObject.SetActive(false);
    }

    public void HandleMovement(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    public void HandleClearHazard(InputAction.CallbackContext context)
    {
        if (currentHazardObject)
        {
            currentHazardObject.SendMessage("ClearHazzard", null);
        }
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
        float currentSpeed = Mathf.Abs(rb.velocity.x);
        prevMaxSpeed = currentSpeed;

        if (currentSpeed > 0 && currentSpeed < 5)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isAccelerating", true);
        }

        if (currentSpeed > 5)
        {
            animator.SetBool("isAccelerating", false);
            animator.SetBool("isMaxSpeed", true);
        }

        if (currentSpeed < 1)
        {
            animator.SetBool("isAccelerating", false);
            animator.SetBool("isMaxSpeed", false);
            animator.SetBool("isIdle", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Hazzard>())
        {
            currentHazardObject = coll.gameObject.GetComponent<Hazzard>();

            messageCanvas.gameObject.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D coll)
    {
        currentHazardObject = null;
        messageCanvas.gameObject.SetActive(false);
    }
}
