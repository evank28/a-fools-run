using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
  public GameObject spawnObject; // The object to spawn repeatedly
  public float timeToSpawn; // Time in seconds before the next spawn of spawnObject
  public float xmin, xmax, y, zmin, zmax; // position bounds
  private Vector3 position; // position to spawn

  void Start() {
    StartCoroutine(spawn());
  }

  IEnumerator spawn () {

      while(true) {
        // Set the position of the spawn object as the position of the player
        position += transform.position;
        position += transform.forward * 3;


        Debug.Log("Instantiating object " + spawnObject.name + " in location "
                  + position.x.ToString() + " "
                  + position.y.ToString() + " "
                  + position.z.ToString());

        GameObject potion  = Instantiate(spawnObject,
                                         position,
                                         Quaternion.identity);

        Debug.Log("Waiting for " + timeToSpawn.ToString() + " seconds.");
        /** Wait for some time before spawning again.
        In case of the potion, the time to spawn again
        should decrease as the player levels up */
        yield return new WaitForSeconds (timeToSpawn);
      }
  }
}
