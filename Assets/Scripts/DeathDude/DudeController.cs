using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeController : MonoBehaviour
{

    [Header("Component")]
    private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float topSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float gravity;

    [Header("State")]
    public bool isGoingLeft;
    private Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        isGoingLeft = utils.randomBoolean();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (isGoingLeft)
        {
            velocity = new Vector3(rb.velocity.x - acceleration, rb.velocity.y, 0f);
        }
        else
        {
            velocity = new Vector3(rb.velocity.x + acceleration, rb.velocity.y, 0f);
        }

        rb.MovePosition(transform.position + currentVelocity * Time.deltaTime);
    }
}


