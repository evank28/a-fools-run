using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public AudioClip hitpotion;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("potion")) {
            GetComponent<AudioSource>().clip = hitpotion;
            GetComponent<AudioSource>().Play();
        };
        Debug.Log("Hit the potion");
    }
}
