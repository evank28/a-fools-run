using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
  public GameObject spawnObject; // The object to spawn repeatedly
  public float timeToSpawn; // Time in seconds before the next spawn of spawnObject
  private Vector3 position; // position to spawn
  private GameObject player; // The active character right now
  private bool spawnStarted; // Have we already started spawning?

  IEnumerator spawn () {
      while(true) {
        spawnStarted = true;
        // Set the position of the spawn object as the position of the player
        position = player.transform.position;
        position += player.transform.forward * 3;
        position += player.transform.up * 7;


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

  public void setActivePlayer(GameObject activePlayer) {
    if (spawnStarted) print("Player is :" +player.ToString());
    player = activePlayer;
    print("Setting the player to active player: " + activePlayer.ToString());
    if (!spawnStarted) StartCoroutine(spawn());
  }
}
