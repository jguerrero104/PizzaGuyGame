using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudioController : MonoBehaviour
{
    public AudioClip[] tracks;  // Array to hold your tracks
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayRandomTrack();
    }

    void PlayRandomTrack()
    {
        if (tracks.Length == 0) return;  // Check if there are any tracks to play

        // Select a random track
        int randomIndex = Random.Range(0, tracks.Length);
        audioSource.clip = tracks[randomIndex];
        audioSource.Play();
    }
}
