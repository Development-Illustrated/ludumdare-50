using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazzard : MonoBehaviour
{

    [SerializeField] public bool killsPlayer;
    [SerializeField] public GameObject ogGraphic;
    [SerializeField] public GameObject changedGraphic;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(this.name + " triggerEnterHit");
        ogGraphic.SetActive(false);
        changedGraphic.SetActive(true);
        if (killsPlayer)
        {
            other.gameObject.SendMessage("Kill", SendMessageOptions.DontRequireReceiver);
        }

    }
}
