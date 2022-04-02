using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite alternateSprite;
    [SerializeField] private Color alternateColor;
    private Sprite originalSprite;
    private Color originalColor;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        originalSprite = spriteRenderer.sprite;
    }

    public void Interact()
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
    }

    public void StopInteract()
    {
        spriteRenderer.sprite = originalSprite;
        spriteRenderer.color = originalColor;
    }
}

