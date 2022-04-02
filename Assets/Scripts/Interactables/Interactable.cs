using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    [SerializeField] private int interactionTime;
    [SerializeField] private Sprite alternateSprite;
    [SerializeField] private Color alternateColor;
    private Sprite originalSprite;
    private Color originalColor;
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        originalSprite = spriteRenderer.sprite;
    }

    public int Interact()
    {
        Debug.Log(this.name + " is being interacted with");
        if (alternateSprite != null)
        {
            spriteRenderer.sprite = alternateSprite;
        }
        else if (alternateColor != null)
        {
            spriteRenderer.color = alternateColor;
        }

        return interactionTime;
    }

    public void StopInteract()
    {
        spriteRenderer.sprite = originalSprite;
        spriteRenderer.color = originalColor;
    }
}

