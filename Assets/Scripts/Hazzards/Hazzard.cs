using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazzard : MonoBehaviour
{

    [SerializeField] public bool killsPlayer;
    [SerializeField] public bool killSelfOnClear;
    [SerializeField] ParticleSystem effectPs;
    [SerializeField] ParticleSystem ongoingPs;

    public void OnEnable()
    {
        CountManager.Instance.incrementCount(CountManager.CountType.Hazard);
    }

    public void OnDisable()
    {
        CountManager.Instance.decrementCount(CountManager.CountType.Hazard);
    }

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
    }

    private void OnDisable()
    {
        if (ongoingPs != null)
        {
            ongoingPs.Stop();
        }

    }
}
