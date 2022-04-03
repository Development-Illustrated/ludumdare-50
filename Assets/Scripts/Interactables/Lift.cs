using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{

    LiftManager liftManager;
    [SerializeField] public int liftIndex;
    [SerializeField] public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        liftManager = transform.parent.GetComponent<LiftManager>();
        liftIndex = transform.GetSiblingIndex();
        spawnPoint = transform.Find("SpawnPoint");
    }

    public void EnterLift(GameObject obj)
    {
        liftManager.EnterLift(liftIndex, obj);
    }

    public Vector3 GetExitPosition()
    {
        return spawnPoint.position;
    }
}
