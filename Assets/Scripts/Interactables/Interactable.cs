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
    void Start()
    {
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
        if (timeUntilStopInteraction > 0f && Time.time > timeUntilStopInteraction)
        {
            spriteRenderer.sprite = originalSprite;
            spriteRenderer.color = originalColor;
            timeUntilStopInteraction = 0f;
            available = true;
        }
    }

    public int Interact(GameObject obj = null)
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

    public void StopInteract()
    {
        Debug.Log("Stopped interacting with " + this.name);
        timeUntilStopInteraction = Time.time + waitAfterInteraction;
    }
}

