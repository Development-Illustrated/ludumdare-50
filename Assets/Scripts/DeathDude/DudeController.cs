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
    [SerializeField] private float timeBetweenDecisions;
    [SerializeField] private float randomChanceChangeDirection;

    [Header("State")]
    private bool isGoingRight;
    private Vector3 movement;
    private float currentSpeed;
    private float nextDecisionTime;
    public bool isAlive;


    // Start is called before the first frame update
    void Start()
    {
        isGoingRight = utils.randomBoolean();
        currentSpeed = 0f;
        rb = GetComponent<Rigidbody2D>();
        nextDecisionTime = Time.time + timeBetweenDecisions;
        isAlive = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextDecisionTime)
        {
            nextDecisionTime = Time.time + timeBetweenDecisions;
            if (Random.Range(0f, 1f) > randomChanceChangeDirection)
            {
                bool newdirection = utils.randomBoolean();
                if (newdirection != isGoingRight)
                {
                    Debug.Log("Changing direction");
                }

                isGoingRight = newdirection;
            }

        }
    }

    void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
        }

        if (isGoingRight)
        {
            if (currentSpeed < topSpeed)
            {
                currentSpeed = currentSpeed + acceleration * Time.fixedDeltaTime;
            }
            else
            {
                currentSpeed = currentSpeed - deceleration * Time.fixedDeltaTime;
            }


        }
        else if (!isGoingRight)
        {
            if (currentSpeed > -topSpeed)
            {
                currentSpeed = currentSpeed - acceleration * Time.fixedDeltaTime;
            }
            else
            {
                currentSpeed = currentSpeed + deceleration * Time.fixedDeltaTime;
            }
        }

        movement = new Vector3(currentSpeed, -gravity, 0f);

        rb.MovePosition(transform.position + movement * Time.fixedDeltaTime);
    }



    public void Kill()
    {
        isAlive = false;
        Debug.Log("Oh no something killed me!");
    }

}
