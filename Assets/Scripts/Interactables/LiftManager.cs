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
    allLifts = new List<Lift>();

    Lift[] lifts = transform.GetComponentsInChildren<Lift>();

    for (int i = 1; i < lifts.Length; i++)
    {
      allLifts.Add(lifts[i]);
    }

  }

}
