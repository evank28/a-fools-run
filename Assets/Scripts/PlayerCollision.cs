using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
  public GameObject character1;
  public GameObject character2;

  void Start()
  {
    // Set the active player to player 2 for now for transform of spawn to work
    transform.parent.gameObject.GetComponent<Spawn>().setActivePlayer(character2);
  }

  void OnCollisionEnter(Collision collision)
  {
    print($"collision occured with {collision.collider.name}");
    if (collision.collider.CompareTag("Potion")) {
      collision.collider.gameObject.GetComponent<potionCollision>().Explode();
      if (character2.activeSelf) {
        character1.transform.position = character2.transform.position;
        character1.transform.rotation = character2.transform.rotation;
        character2.SetActive(false);
        character1.SetActive(true);
        transform.parent.gameObject.GetComponent<Spawn>().setActivePlayer(character1);
      } else if (character1.activeSelf) {
        character2.transform.position = character1.transform.position;
        character2.transform.rotation = character1.transform.rotation;
        character1.SetActive(false);
        character2.SetActive(true);
        transform.parent.gameObject.GetComponent<Spawn>().setActivePlayer(character2);
      }
    }
  }
}
