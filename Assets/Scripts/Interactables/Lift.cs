using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{

    LiftManager liftManager;
    [SerializeField] public int liftIndex;
    
    [SerializeField] float liftCooldownTime;
    [SerializeField] float liftTravelCooldownTime;

    [SerializeField] Sprite closedDoors;
    [SerializeField] Sprite openDoors;
    [SerializeField] AudioClip liftClip;
    Transform spawnPoint;
    private PlayAudio playAudio;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D coll;
    private float liftAvailableTime;
    private float liftTravelTime;
    public bool liftAvailable;

    private GameObject currentOccupant;
    private Lift destinationLift;

    // Start is called before the first frame update
    void Start()
    {
        playAudio = GetComponent<PlayAudio>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        liftAvailableTime = Time.time;
        liftManager = transform.parent.GetComponent<LiftManager>();
        liftIndex = transform.GetSiblingIndex();
        spawnPoint = transform.Find("SpawnPoint");
    }

    private void Update()
    {

        if (Time.time > liftTravelTime && liftAvailable == false)
        {
            if (currentOccupant)
            {
                currentOccupant.transform.position = destinationLift.GetExitPosition();
                currentOccupant.SetActive(true);
                currentOccupant = null;
            }
            spriteRenderer.sprite = openDoors;

        }
        if (liftAvailable == false && Time.time > liftAvailableTime)
        {
            liftAvailable = true;
            coll.enabled = true;
        }
    }

    public float EnterLift(GameObject occupant)
    {
        currentOccupant = occupant;
        SetUnavailable();
        currentOccupant.SetActive(false);
        destinationLift = liftManager.GetAvailableLift(this);
        destinationLift.SetAsDestination();
        liftTravelTime = Time.time + liftTravelCooldownTime;
        playAudio.PlayOneShot(liftClip);
        return liftCooldownTime;
    }

    public void SetAsDestination()
    {
        SetUnavailable();
    }

    public void SetUnavailable()
    {
        liftAvailable = false;
        liftAvailableTime = Time.time + liftCooldownTime;
        liftTravelTime = Time.time + liftTravelCooldownTime;
        coll.enabled = false;
        spriteRenderer.sprite = closedDoors;
    }

    public Vector3 GetExitPosition()
    {
        return spawnPoint.position;
    }
}
