using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
  public GameObject character1;
  public GameObject character2;

  void OnCollisionEnter(Collision collision) {
    if (collision.collider.CompareTag("Potion")) {
      if (character2.active == true) {
        character1.transform.position = character2.transform.position;
        character1.transform.rotation = character2.transform.rotation;
        character2.SetActive(false);
        character1.SetActive(true);
      } else if (character1.active == true) {
        character2.transform.position = character1.transform.position;
        character2.transform.rotation = character1.transform.rotation;
        character1.SetActive(false);
        character2.SetActive(true);
      }
    }
  }
}
