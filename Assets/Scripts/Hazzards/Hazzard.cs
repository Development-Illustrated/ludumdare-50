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
    [SerializeField] AudioClip hazzardSoundFX;
    [SerializeField] AudioClip continuousHazzardSoundFX;
    Color ogColor;
    PlayAudio playAudio;
    SpriteRenderer spriteRenderer;
    private Decay decayScript;

    public bool isHazardous;

    void Start()
    {
        isHazardous = false;
        decayScript = this.gameObject.GetComponent<Decay>();
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        ogColor = spriteRenderer.color;
        playAudio = this.gameObject.GetComponent<PlayAudio>();
    }

    public void BeHazardous()
    {
        isHazardous = true;
        if (ongoingPs != null)
        {
            ongoingPs.Play();
        }
        playAudio.PlayContinous(continuousHazzardSoundFX);
        try
        {
            CountManager.Instance.incrementCount(CountManager.CountType.Hazard);
        }
        catch (System.NullReferenceException)
        {

            // Woops
        }

    }

    public void PlayHazardFX()
    {
        playAudio.PlayOneShot(hazzardSoundFX);
        // Play one of particle system
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
        playAudio.StopPlaying();

        if (killSelfOnClear)
        {
            Destroy(this.gameObject);
        }
        else if (decayScript)
        {
            decayScript.FixHazard();
        }
        isHazardous = false;
            
    
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
