using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazzardPointer : MonoBehaviour
{

    [SerializeField] float minDistanceToDisplay;
    [SerializeField] Transform ghostBoy;
    
    List<Hazzard> allHazzards;
    SpriteRenderer r;
    Hazzard closest;

    private void Start() 
    {
        r = GetComponentInChildren<SpriteRenderer>();
        allHazzards = new List<Hazzard>();
        allHazzards.AddRange(FindObjectsOfType<Hazzard>());
    }

    private void Update() 
    {
        transform.position = ghostBoy.position;
        closest = GetClosestHazardousHazzard();
        if(closest)
        {
            r.enabled = true;
            transform.right = closest.transform.position - transform.position;
        }
        else
        {
            closest = null;
            r.enabled = false;
        }
        
    }

    Hazzard GetClosestHazardousHazzard()
    {
        Hazzard hMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Hazzard h in allHazzards)
        {
            if(h.isHazardous)
            {
                float dist = Vector3.Distance(h.transform.position, currentPos);
                if (dist < minDist && dist > minDistanceToDisplay)
                {
                    hMin = h;
                    minDist = dist;
                }
            }
            
        }
        return hMin;
    }
    
}
