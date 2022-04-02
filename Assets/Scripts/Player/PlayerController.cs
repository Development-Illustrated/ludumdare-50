using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  private Vector2 moveVector;
  private Rigidbody2D rb;

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

    rb.AddForce(new Vector2(moveVector.x * movementSpeed * Time.fixedDeltaTime, moveVector.y * movementSpeed * Time.fixedDeltaTime), ForceMode2D.Impulse);
  }
}
