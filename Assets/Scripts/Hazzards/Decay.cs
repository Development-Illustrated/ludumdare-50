using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decay : MonoBehaviour
{
    [SerializeField] public int decayAfterXUses;
    [SerializeField] public float decayAfterXSeconds;
    private float elapsedTime;
    private int currentUses;
    private bool decayUsingTime;
    private bool decayUsingUses;
    private bool isDecayed;
    private Hazzard hazardScript;

    void Start()
    {
        currentUses = 0;
        elapsedTime = 0f;
        decayUsingTime = decayAfterXSeconds > 0;
        decayUsingUses = decayAfterXUses > 0;
        hazardScript = this.gameObject.GetComponent<Hazzard>();
    }

    void Update()
    {
        if (
            !isDecayed && (
                (decayUsingUses && currentUses == decayAfterXUses) ||
                (
                    decayUsingTime &&
                        elapsedTime + decayAfterXSeconds < Time.time
                )
            )
        ) {
            DecayHazard();
        }
    }

    public void OnUse()
    {
        // if hazzard script not enabled
        if (!hazardScript.enabled)
            currentUses += 1;
    }

    void DecayHazard()
    {
        Debug.Log("Hazard has Decayed");
        isDecayed = true;
        elapsedTime = Time.time;
        hazardScript.enabled = true;
    }

    void FixHazard()
    {
        Debug.Log("Hazard has been fixed");
        currentUses = 0;
        isDecayed = false;
        elapsedTime = Time.time;
        hazardScript.enabled = false;
    }
}
