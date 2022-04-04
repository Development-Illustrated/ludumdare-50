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
        if (!isDecayed && decayUsingTime && elapsedTime + decayAfterXSeconds < Time.time)
        {
            DecayHazard();
        }
    }

    public void OnUse()
    {
        currentUses += 1;
        if(decayUsingUses && currentUses >= decayAfterXUses)
        {
            DecayHazard();
        }
    }

    public void DecayHazard()
    {
        Debug.Log(this.name + " hazard has Decayed");
        isDecayed = true;
        elapsedTime = Time.time;
        hazardScript.BeHazardous();
    }

    public void ResetDecay()
    {
        currentUses = 0;
        isDecayed = false;
        elapsedTime = Time.time;
    }
}
