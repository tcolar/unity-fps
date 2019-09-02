using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WooshSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] wooshSounds;

    // Start is called before the first frame update
    void PlayWooshSound()
    {
        audioSource.clip = wooshSounds[Random.Range(0, wooshSounds.Length)];
        audioSource.Play();
    }

}
