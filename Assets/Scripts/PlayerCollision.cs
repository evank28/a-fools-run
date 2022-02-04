using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
  public GameObject sphere;
  public GameObject cube;

  void Start() {
    sphere.transform.position = transform.position;
    //Instantiate(sphere, transform.position, Quaternion.identity);
    sphere.SetActive(true);
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.F)) {
      if (sphere.active == true) {
        cube.transform.position = sphere.transform.position;
        sphere.SetActive(false);
        cube.SetActive(true);
      } else if (cube.active == true) {
        sphere.transform.position = cube.transform.position;
        cube.SetActive(false);
        sphere.SetActive(true);
      }
    }
  }
}
