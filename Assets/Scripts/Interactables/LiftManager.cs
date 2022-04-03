using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftManager : MonoBehaviour
{
    [SerializeField] List<Lift> availableLifts;
    public void EnterLift(int liftIndex, GameObject occupant)
    {
        occupant.SetActive(false);
        Lift exitLift = availableLifts[Random.Range(0, availableLifts.Count)];
        occupant.transform.position = exitLift.GetExitPosition();
        occupant.SetActive(true);
    }

    void Start()
    {
        // Populate known lifts
        availableLifts = new List<Lift>();
        availableLifts.AddRange(transform.GetComponentsInChildren<Lift>());
    }

}
