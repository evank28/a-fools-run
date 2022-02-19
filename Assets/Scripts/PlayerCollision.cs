using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
  public GameObject character1;
  public GameObject character2;
  public AudioClip hitpotion;
  public static bool hitFinishLine;

  void Start()
  {
    // Set the active player to player 2 for now for transform of spawn to work
    transform.parent.gameObject.GetComponent<Spawn>().setActivePlayer(character2);
    hitFinishLine = false;
  }

  void OnCollisionEnter(Collision collision)
  {
    print($"collision occured with {collision.collider.name}");
    if (collision.collider.CompareTag("Potion")) {
      GetComponent<AudioSource>().clip = hitpotion;
      GetComponent<AudioSource>().Play();

      collision.collider.gameObject.GetComponent<potionCollision>().Explode();
      if (character2.activeSelf) {
        print("Changing to character 1\n");
        character1.transform.position = character2.transform.position;
        character1.transform.rotation = character2.transform.rotation;
        character2.SetActive(false);
        character1.SetActive(true);
        transform.parent.gameObject.GetComponent<Spawn>().setActivePlayer(character1);
      } else if (character1.activeSelf) {
        print("Changing to character 2\n");
        character2.transform.position = character1.transform.position;
        character2.transform.rotation = character1.transform.rotation;
        character1.SetActive(false);
        character2.SetActive(true);
        transform.parent.gameObject.GetComponent<Spawn>().setActivePlayer(character2);
      }
    }
    if (collision.collider.CompareTag("FinishLine")) {
      hitFinishLine = true;
    }
  }
}
