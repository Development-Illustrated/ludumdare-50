using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazzard : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision detected on " + this.name);
        other.gameObject.SendMessage("Kill", null, SendMessageOptions.DontRequireReceiver);
    }
}
