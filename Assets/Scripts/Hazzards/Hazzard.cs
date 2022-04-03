using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazzard : MonoBehaviour
{

    [SerializeField] public bool killsPlayer;
    [SerializeField] ParticleSystem effectPs;
    [SerializeField] ParticleSystem ongoingPs;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(this.name + " triggerEnterHit");
        effectPs.Play();

        if (killsPlayer)
        {
            other.gameObject.SendMessage("Kill", SendMessageOptions.DontRequireReceiver);
        }

    }

    public void ClearHazzard()
    {
        Debug.Log("Clearing Hazzard: " + this.name);
    }

    private void OnEnable()
    {
        ongoingPs.Play();
    }

    private void OnDisable()
    {
        ongoingPs.Stop();
    }
}
