using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSpawnerController : MonoBehaviour
{
  [Header("References")]
  [SerializeField] Interactable[] interactablePrefabs;

  void Start()
  {
    // Pick an interactable at random
    int idx = Random.Range(0, interactablePrefabs.Length);
    GameObject.Instantiate<Interactable>(interactablePrefabs[idx], transform);
  }

  // Update is called once per frame
  void Update()
  {

  }
}
