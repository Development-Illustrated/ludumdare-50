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
    Animator anim;

    [Header("State")]
    private bool isGoingRight;
    private Interactable currentInteractable;
    private float resumeTime;
    private Vector3 movement;
    private float currentSpeed;
    private float nextDecisionTime;
    public bool isAlive;




    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        try
        {
            CountManager.Instance.incrementCount(CountManager.CountType.Population);
        }
        catch (System.NullReferenceException)
        {
            // Woops
        }


        rb = GetComponent<Rigidbody2D>();
        isAlive = true;
    }

    private void OnEnable()
    {
        isGoingRight = utils.randomBoolean();
        currentSpeed = 0f;
        nextDecisionTime = Time.time + timeBetweenDecisions;
    }

    void Update()
    {

        if (currentInteractable != null)
        {
            if (Time.time > resumeTime)
            {
                currentInteractable.StopInteract();
                currentInteractable = null;
            }
        }

        if (Time.time > nextDecisionTime && currentInteractable == null)
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

        if (currentInteractable != null)
        {
            currentSpeed = 0;
        }
        movement = new Vector3(currentSpeed, -gravity, 0f);
        rb.MovePosition(transform.position + movement * Time.fixedDeltaTime);
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        if (Mathf.Abs(currentSpeed) > 0)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        if (isGoingRight)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 0);
        }

    }



    public void Kill()
    {
        isAlive = false;
        CountManager.Instance.decrementCount(CountManager.CountType.Population);
        CountManager.Instance.incrementCount(CountManager.CountType.Death);
        Debug.Log("Oh no something killed me!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Interactable>())
        {
            currentInteractable = other.gameObject.GetComponent<Interactable>();
            int interactionTime = currentInteractable.Interact(this.gameObject);
            resumeTime = Time.time + interactionTime;
        }
        if (other.gameObject.GetComponent<Lift>())
        {
            other.gameObject.GetComponent<Lift>().EnterLift(this.gameObject);
        }
    }
}
