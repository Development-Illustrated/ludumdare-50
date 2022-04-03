using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazzard : MonoBehaviour
{

    [SerializeField] public bool killsPlayer;
    [SerializeField] public bool killSelfOnClear;
    [SerializeField] ParticleSystem effectPs;
    [SerializeField] ParticleSystem ongoingPs;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(this.name + " triggerEnterHit");
        if (effectPs != null)
        {
            effectPs.Play();
        }


        if (killsPlayer)
        {
            other.gameObject.SendMessage("Kill", SendMessageOptions.DontRequireReceiver);
        }

    }

    public void ClearHazzard()
    {
        Debug.Log("Clearing Hazzard: " + this.name);
        if (killSelfOnClear)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnEnable()
    {
        if (ongoingPs != null)
        {
            ongoingPs.Play();
        }
        try
        {
            CountManager.Instance.incrementCount(CountManager.CountType.Hazard);
        }
        catch (System.NullReferenceException)
        {

            // Woops
        }

    }

    private void OnDisable()
    {
        if (ongoingPs != null)
        {
            ongoingPs.Stop();
        }

        try
        {
            CountManager.Instance.decrementCount(CountManager.CountType.Hazard);
        }
        catch (System.NullReferenceException)
        {
            // woops
        }


    }
}
