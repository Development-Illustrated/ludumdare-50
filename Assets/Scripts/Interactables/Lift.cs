using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{

    LiftManager liftManager;
    [SerializeField] public int liftIndex;
    [SerializeField] public List<Transform> exitPositions;

    // Start is called before the first frame update
    void Start()
    {
        liftManager = transform.parent.GetComponent<LiftManager>();
        liftIndex = transform.GetSiblingIndex();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Lift has been triggered by " + other.gameObject.name);
        liftManager.EnterLift(liftIndex, other.gameObject);
    }

    public Vector3 GetExitPosition()
    {
        return exitPositions[Random.Range(0, exitPositions.Count)].position;
    }
}
