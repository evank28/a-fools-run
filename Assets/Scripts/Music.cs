 using UnityEngine;
 using System.Collections;

 [RequireComponent(typeof(AudioSource))]
 public class Music : MonoBehaviour
 {
     public AudioClip clipOne;
     public AudioClip clipTwo;
     public AudioClip clipThree;
     public AudioClip clipFour;
     
     void Start()
     {
//         GetComponent<AudioSource> ().loop = true;
         StartCoroutine(playConsecMusic());
     }

     IEnumerator playConsecMusic()
     {
         GetComponent<AudioSource>().clip = clipOne;
         GetComponent<AudioSource>().Play();
         yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length - 1);
         GetComponent<AudioSource>().clip = clipTwo;
         GetComponent<AudioSource>().Play();
         yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
         GetComponent<AudioSource>().clip = clipThree;
         GetComponent<AudioSource>().Play();
         yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
         GetComponent<AudioSource>().clip = clipFour;
         GetComponent<AudioSource>().Play();
         yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
         StartCoroutine(playConsecMusic());
     }
 }
