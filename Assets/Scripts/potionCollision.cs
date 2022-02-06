using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionCollision : MonoBehaviour
{
  public GameObject particleEffect;
  // void OnCollisionEnter(Collision collision) {
  //   if (collision.collider.CompareTag("Player")) Explode();
  //   Debug.Log("Hit the player");
  // }

  void _Explode() {
    GameObject effect = Instantiate(particleEffect,
                                    transform.position,
                                    Quaternion.identity);
    Destroy(gameObject);
  }

  public void Explode() {
    GameObject effect = Instantiate(particleEffect,
      transform.position,
      Quaternion.identity);
    Destroy(gameObject);
  }
}
