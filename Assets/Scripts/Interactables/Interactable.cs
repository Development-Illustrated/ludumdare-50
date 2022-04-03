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
    private Sprite originalSprite;
    private Color originalColor;

    private float timeUntilStartInteraction = 0f;
    private float timeUntilStopInteraction = 0f;

    private Decay decayScript;
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        originalSprite = spriteRenderer.sprite;
        decayScript = this.gameObject.GetComponent<Decay>();
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
        }
    }

    public int Interact()
    {
        Debug.Log("Started interacting with " + this.name);
        timeUntilStartInteraction = Time.time + waitBeforeInteraction;
        if (decayScript) {
            decayScript.OnUse();
        }
        return interactionTime;
    }

    public void StopInteract()
    {
        Debug.Log("Stopped interacting with " + this.name);
        timeUntilStopInteraction = Time.time + waitAfterInteraction;
    }
}

