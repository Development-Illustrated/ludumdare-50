using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazzard : MonoBehaviour
{

    [SerializeField] public bool killsPlayer;
    [SerializeField] public bool killSelfOnClear;
    [SerializeField] ParticleSystem effectPs;
    [SerializeField] ParticleSystem ongoingPs;
    [SerializeField] Color interactionColor;
    Color ogColor;

    SpriteRenderer spriteRenderer;
    private Decay decayScript;

    void Start()
    {
        decayScript = this.gameObject.GetComponent<Decay>();
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        ogColor = spriteRenderer.color;
    }


    public void OutlineMe(bool doIt)
    {
        if (doIt)
        {
            spriteRenderer.color = interactionColor;
        }
        else
        {
            spriteRenderer.color = ogColor;
        }
    }

    public void ClearHazzard()
    {
        Debug.Log("Clearing Hazzard: " + this.name);
        if (killSelfOnClear)
        {
            Destroy(this.gameObject);
        }
        else if (decayScript)
        {
            decayScript.FixHazard();
        }
        ongoingPs.Stop();

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
