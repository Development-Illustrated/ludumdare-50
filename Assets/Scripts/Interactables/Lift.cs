using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{

    LiftManager liftManager;
    [SerializeField] public int liftIndex;
    [SerializeField] public Transform spawnPoint;
    [SerializeField] float liftCooldownTime;
    private BoxCollider2D coll;
    private float liftAvailableTime;
    public bool liftAvailable;

    private GameObject currentOccupant;
    private Lift destinationLift;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        liftAvailableTime = Time.time;
        liftManager = transform.parent.GetComponent<LiftManager>();
        liftIndex = transform.GetSiblingIndex();
        spawnPoint = transform.Find("SpawnPoint");
    }

    private void Update()
    {
        if (liftAvailable == false)
        {
            if (currentOccupant)
            {
                currentOccupant.transform.position = destinationLift.GetExitPosition();
                currentOccupant.SetActive(true);
                currentOccupant = null;
            }
            if (Time.time > liftAvailableTime)
            {
                liftAvailable = true;
                coll.enabled = true;
            }
        }
    }

    public void EnterLift(GameObject occupant)
    {
        currentOccupant = occupant;
        SetUnavailable();
        currentOccupant.SetActive(false);
        destinationLift = liftManager.GetAvailableLift(this);
        destinationLift.SetAsDestination();
    }

    public void SetAsDestination()
    {
        SetUnavailable();
    }

    public void SetUnavailable()
    {
        liftAvailable = false;
        liftAvailableTime = Time.time + liftCooldownTime;
        coll.enabled = false;
    }

    public Vector3 GetExitPosition()
    {
        return spawnPoint.position;
    }
}
