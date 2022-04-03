using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class LiftManager : MonoBehaviour
{
    [SerializeField] List<Lift> allLifts;

    public Lift GetAvailableLift(Lift lift)
    {
        foreach (Lift l in allLifts)
        {
            if (l.liftAvailable)
            {
                return l;
            }
        }
        return lift;
    }

    void Start()
    {
        // Populate known lifts
        allLifts = new List<Lift>();
        allLifts.AddRange(transform.GetComponentsInChildren<Lift>());
    }

}
