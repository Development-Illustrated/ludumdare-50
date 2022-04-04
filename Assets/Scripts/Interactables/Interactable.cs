using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] private float waitBeforeInteraction;
    [SerializeField] private float waitAfterInteraction;
    [SerializeField] private int interactionTime;
    [SerializeField] private Sprite alternateSprite;
    [SerializeField] private Color alternateColor;

    [SerializeField]
    private AudioClip interactClip;

    private PlayAudio playAudio;
    private Sprite originalSprite;
    private Color originalColor;

    private float timeUntilStartInteraction = 0f;
    private float timeUntilStopInteraction = 0f;
    public bool available;

    private Decay decayScript;

    private Hazzard hazzard;
    void Start()
    {
        hazzard = GetComponent<Hazzard>();
        available = true;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        originalSprite = spriteRenderer.sprite;
        decayScript = GetComponent<Decay>();
        playAudio = GetComponent<PlayAudio>();
    }

    void Update()
    {
        if (timeUntilStartInteraction > 0f && Time.time > timeUntilStartInteraction)
        {
            if (alternateSprite != null)
            {
                spriteRenderer.sprite = alternateSprite;
            }
            else if (alternateColor != null)
            {
                spriteRenderer.color = alternateColor;
            }
            timeUntilStartInteraction = 0f;
        }
    }

    public int Interact()
    {
        Debug.Log("Started interacting with " + this.name);
        available = false;
        timeUntilStartInteraction = Time.time + waitBeforeInteraction;
        if (decayScript)
        {
            decayScript.OnUse();
        }
        if (playAudio)
        {
            playAudio.PlayOneShot(interactClip);
        }
        return interactionTime;
    }

    public void StopInteract(DudeController dudeInteracting)
    {
        Debug.Log("Stopped interacting with " + this.name);
        available = true;
        timeUntilStopInteraction = Time.time + waitAfterInteraction;
        spriteRenderer.sprite = originalSprite;
        spriteRenderer.color = originalColor;
        
        if(hazzard != null && hazzard.isHazardous && hazzard.killsPlayer && dudeInteracting.isAlive)
        {
            dudeInteracting.Kill();
            hazzard.PlayHazardFX();
        }
    }
}

